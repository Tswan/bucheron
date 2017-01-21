using System;
using System.Collections.Generic;

using UnityEngine;

public class GGJ_EnemyController : GGJ_BaseController
{
    [HideInInspector]
    public GGJ_PlayerController MoveToPlayerController { get; set; }

    public GameObject CurrencyDrop;
    public float MaxViewDistance;
    public AudioClip DeathAudio;
  
    private int _collisionCount;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    protected override void Start()
    {
        base.Start();
        _collisionCount = 0;
    }

    public override void OnDamage(GameObject other, int damage)
    {
        // Call the sound effect of the enemy being hit
        var playerController = other.gameObject.GetComponent<GGJ_PlayerController>();
        playerController.GetComponent<AudioSource>().PlayOneShot(playerController.OnHitAudio, 0.5f);
    }

    public override void OnKill(GameObject other)
    {
        // Increment the player controller that killed this enemy
        other.gameObject.GetComponent<GGJ_PlayerController>().KillCount++;

        // Remove this from the swarm controller
        GameObject.FindObjectOfType<GGJ_SwarmController>().Enemies.Remove(this);

        // Flip the enemy upside down
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 180, transform.rotation.w);
        myAnim.enabled = false;

        // Reduce the weight of the rigid body so the explosion is very effective
        RigidBody.mass = 0.0001f;

        // Drop currency
        var currencyObject = Instantiate(CurrencyDrop, gameObject.transform.position, Quaternion.identity) as GameObject;
        var direction = currencyObject.transform.position - other.transform.position;
        currencyObject.GetComponent<Rigidbody>().AddExplosionForce(1000.0f, other.transform.position, 10.0f);

        // Play death audio
        gameObject.GetComponent<AudioSource>().PlayOneShot(DeathAudio, 0.5f);

        // Spawn a death fade timer
        gameObject.AddComponent<GGJ_DeathFadeTimer>();

        Destroy(this); ;

        // Destory this controller, we are done now.
        
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<GGJ_BaseController>() != null)
        {
            _collisionCount++;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        //_shouldMove = false;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.GetComponent<GGJ_BaseController>() != null)
        {
            _collisionCount--;
        }
    }

    protected override Vector3 GetMovementDirection()
    {
        // Check whether there's a player controller to move towards
        if (MoveToPlayerController != null)
        {
            // Find direction between enemy and player
            var enemyPosition = RigidBody.transform.position;
            var playerPosition = MoveToPlayerController.GetComponent<Rigidbody>().transform.position;
            var direction = playerPosition - enemyPosition;

            // Check the distance between the enemy and any object between them and the player (including the player)
            var raycastHit = new RaycastHit();
            var racastResult = Physics.Raycast(enemyPosition, direction, out raycastHit);
            
            // If we're too close to another object, don't move
            if (_collisionCount <= 0 || !racastResult || raycastHit.collider.GetComponent<GGJ_PlayerController>() != null)
            {
                // Move toward the player
                return Vector3.Normalize(new Vector3(direction.x, 0.0f, direction.z));
            }
        }

        // Don't move
        return Vector3.zero;
    }
}