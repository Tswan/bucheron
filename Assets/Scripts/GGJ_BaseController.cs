using UnityEngine;
using System.Collections;

public abstract class GGJ_BaseController : MonoBehaviour
{
    protected Stats Stats { get; private set; }

    private Rigidbody _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        Stats = GetComponent<Stats>();
    }

    private void FixedUpdate()
    {
        if (true)
        {
            // Apply movement by modifying the velocity
            var direction = GetMovementDirection();
            _rigidBody.velocity = new Vector3(direction.x, _rigidBody.velocity.y, direction.z * 0.75f);
        }
        else
        {
            // Apply movement using proper physics
            _rigidBody.AddForce(GetMovementDirection() * GetMovementSpeed());
        }
    }

    protected virtual float GetMovementSpeed()
    {
        return Stats.MoveSpeed;
    }

    protected abstract Vector3 GetMovementDirection();
}
