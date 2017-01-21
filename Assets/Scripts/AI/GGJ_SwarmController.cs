using System.Linq;
using System.Collections.Generic;

using UnityEngine;

public class GGJ_SwarmController : MonoBehaviour
{
    [HideInInspector]
    public List<GGJ_EnemyController> Enemies { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Enemies = new List<GGJ_EnemyController>();
    }

    private void Update()
    {
        // Check whether any of the enemy controllers can see any of the player
        GGJ_PlayerController moveToPlayerController = null;
        foreach (var enemyController in Enemies.Where(obj => obj.RigidBody != null))
        {
            // Check whether the enemy can see the player by attempting to raycast to the player
            foreach (var ggjPlayerController in GameObject.FindObjectsOfType<GGJ_PlayerController>())
            {
                // Find direction between enemy and player
                var enemyPosition = enemyController.RigidBody.transform.position;
                var playerPosition = ggjPlayerController.GetComponent<Rigidbody>().transform.position;
                var direction = playerPosition - enemyPosition;

                // Cast a ray and check the result
                var raycastHit = new RaycastHit();
                if (Physics.Raycast(enemyPosition, direction, out raycastHit, enemyController.MaxViewDistance))
                {
                    // If this is a player controller then set this as the target
                    var playerHit = raycastHit.collider.gameObject.GetComponent<GGJ_PlayerController>();
                    if (playerHit != null)
                    {
                        moveToPlayerController = playerHit;
                        break;
                    }
                }
            }

            // Check whether we have found a position to move towards
            if (moveToPlayerController != null)
            {
                // Set all the controllers to move to this location
                foreach (var controller in Enemies)
                {
                    controller.MoveToPlayerController = moveToPlayerController;
                }

                // End the loop immediately
                break;
            }
        }
    }
}
