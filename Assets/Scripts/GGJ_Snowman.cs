using System.Linq;
using UnityEngine;
using System.Collections;

public class GGJ_Snowman : GGJ_EnemyController
{

    public GameObject snowball;
    public GameObject hand;

    protected override Vector3 GetMovementDirection()
    {
        return Vector3.zero;
    }

    protected override float GetMovementSpeed()
    {
        return 0.0f;
    }

    private void Update()
    {
        // Check the snowman is aware of the player
        if (MoveToPlayerController != null)
        {
            if (MoveToPlayerController.State != GGJ_PlayerController.PlayerState.Dead)
            {
                throwSnowball();
            }
        }
        else
        {
            myAnim.Stop();
        }

        // Find the closest player
        var closestPlayer = FindObjectOfType<GGJ_PlayerController>();
        RigidBody.rotation = Quaternion.LookRotation(
            new Vector3(closestPlayer.gameObject.transform.position.x, 0.0f, closestPlayer.gameObject.transform.position.z)
                - new Vector3(gameObject.transform.position.x, 0.0f, gameObject.transform.position.z),
            Vector3.up);
    }

    private void throwSnowball()
    {
        myAnim.Play("throwSnowball");
    }

    private void makeSnowball()
    {
        if (MoveToPlayerController != null && MoveToPlayerController.State != GGJ_PlayerController.PlayerState.Dead)
        {
            GameObject newSnowball = Instantiate(snowball, hand.transform.position, Quaternion.identity) as GameObject;
        }
    }
}
