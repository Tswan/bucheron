using System;
using System.Collections.Generic;

using UnityEngine;

public class GGJ_EnemyController : GGJ_BaseController, IDamagable
{
    public float MaxViewDistance;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    
    public void OnDamage(int damage)
    {
        // TODO:
    }

    public void OnKill()
    {
        // TODO:
        Destroy(gameObject);
    }

    protected override Vector3 GetMovementDirection()
    {
        // Check whether the enemy can see the player by attempting to raycast to the player
        var finalDirection = Vector3.zero;
        foreach (var ggjPlayerController in GameObject.FindObjectsOfType<GGJ_PlayerController>())
        {
            // Find direction between enemy and player
            var enemyPosition = RigidBody.transform.position;
            var playerPosition = ggjPlayerController.GetComponent<Rigidbody>().transform.position;
            var direction = playerPosition - enemyPosition;

            // Cast a ray and check the result
            var raycastHit = new RaycastHit();
            if (Physics.Raycast(enemyPosition, direction, out raycastHit, MaxViewDistance))
            {
                var playerHit = raycastHit.collider.gameObject.GetComponent<GGJ_PlayerController>();
                if (playerHit != null)
                {
                    // Check the distance
                    if (raycastHit.distance > 2.5f)
                    {
                        // Move toward the player
                        finalDirection = direction;
                    }
                    else
                    {
                        // Don't move
                        finalDirection = Vector3.zero;
                    }
                    break;
                }
            }
        }

        // Return the final direction
        if (finalDirection == Vector3.zero)
        {
            return Vector3.zero;
        }
        else
        {
            return Vector3.Normalize(new Vector3(finalDirection.x, 0.0f, finalDirection.z));
        }
    }
}