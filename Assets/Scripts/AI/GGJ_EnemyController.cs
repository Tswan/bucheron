using System;
using System.Collections.Generic;

using UnityEngine;

public class GGJ_EnemyController : GGJ_BaseController
{
    [HideInInspector]
    public GGJ_PlayerController MoveToPlayerController { get; set; }

    [HideInInspector]
    public GGJ_SwarmController SwarmController { get; set; }

    public GameObject CurrencyDrop;
    public float MaxViewDistance;
    public AudioClip DeathAudio;
    protected GGJ_PlayerController TargettedPlayerController { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
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
        other.gameObject.GetComponent<GGJ_PlayerController>().IncrementKill(1);

        // Remove this from the swarm controller
        SwarmController.Enemies.Remove(this);

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

        // Destory this controller, we are done here
        Destroy(this);
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
            var raycastResult = Physics.Raycast(enemyPosition, direction, out raycastHit);

            // If we're too close to another object, don't move
            var controller = raycastHit.collider.GetComponent<GGJ_BaseController>();
            if (raycastResult && controller is GGJ_PlayerController && raycastHit.distance < 2.5f)
            {
                // Move into the attack state
                TargettedPlayerController = controller as GGJ_PlayerController;
                return Vector3.zero;
            }
            else if (!raycastResult || controller == null || raycastHit.distance > 2.5f)
            {
                // Move toward the player
                return Vector3.Normalize(new Vector3(direction.x, 0.0f, direction.z));
            }
        }

        // Don't move
        return Vector3.zero;
    }
}