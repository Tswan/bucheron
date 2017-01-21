using UnityEngine;
using System.Collections;

public abstract class GGJ_BaseController : MonoBehaviour
{
    protected Stats Stats { get; private set; }

    public Rigidbody RigidBody { get; private set; }

    public Animator myAnim;

    protected virtual void Start()
    {
        myAnim = gameObject.GetComponent<Animator>();
        RigidBody = gameObject.GetComponent<Rigidbody>();
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

            if (direction.x < 0)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
            }
            else if (direction.x > 0)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
            }
        }
    }

    protected virtual float GetMovementSpeed()
    {
        return Stats.MoveSpeed;
    }

    protected abstract Vector3 GetMovementDirection();

    public abstract void OnDamage(GameObject other, int damage);

    public abstract void OnKill(GameObject other);
}
