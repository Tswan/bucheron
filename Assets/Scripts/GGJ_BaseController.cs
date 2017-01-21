using UnityEngine;
using System.Collections;

public abstract class GGJ_BaseController : MonoBehaviour
{
    protected Stats Stats { get; private set; }

    protected Rigidbody RigidBody { get; private set; }

    protected virtual void Start()
    {
        RigidBody = GetComponent<Rigidbody>();
        Stats = GetComponent<Stats>();
    }

    protected virtual void FixedUpdate()
    {
        // Check there is movement to be applied
        var direction = GetMovementDirection();
        if (direction != Vector3.zero)
        {
            if (true)
            {
                // Apply movement by modifying the velocity
                var movementSpeed = GetMovementSpeed();
                RigidBody.velocity = new Vector3(direction.x * movementSpeed,
                    RigidBody.velocity.y, direction.z * 0.75f * movementSpeed);
            }
            else
            {
                // Apply movement using proper physics
                RigidBody.AddForce(GetMovementDirection() * GetMovementSpeed());
            }
        }
    }

    protected virtual float GetMovementSpeed()
    {
        return Stats.MoveSpeed;
    }

    protected abstract Vector3 GetMovementDirection();
}
