using System;
using System.Collections.Generic;

using UnityEngine;

public class GGJ_EnemyController : GGJ_BaseController
{
    [HideInInspector]
    public GGJ_PlayerController MoveToPlayerController { get; set; }

    public GameObject CurrencyDrop;
    public float MaxViewDistance;

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
        // TODO:
    }

    public override void OnKill(GameObject other)
    {
        // Drop currency
        var currencyObject = Instantiate(CurrencyDrop, gameObject.transform.position, Quaternion.identity) as GameObject;
        var direction = currencyObject.transform.position - other.transform.position;
        currencyObject.GetComponent<Rigidbody>().AddExplosionForce(1000.0f, other.transform.position, 10.0f);

        // TODO: Player sound effect for currency drop
        Debug.Log("TODO: Play sound effect for currency drop.");

        // Remove this from the swarm controller
        GameObject.FindObjectOfType<GGJ_SwarmController>().Enemies.Remove(this);

        // Destory object
        Destroy(gameObject);
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