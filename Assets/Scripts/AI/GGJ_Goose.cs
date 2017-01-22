using UnityEngine;
using System.Collections;

public class GGJ_Goose : GGJ_EnemyController
{
    private const float MAX_ATTACK_TIME = 2.0f / 3.0f;

    private enum AIState
    {
        Idle,
        Walking,
        Attacking,
        Attacked,
    };

    private float _attackTimer;
    private AIState _state;

    protected override void Start()
    {
        base.Start();
        _state = AIState.Idle;
        _attackTimer = 0.0f;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        _attackTimer += Time.deltaTime;

        base.FixedUpdate();

        myAnim.SetBool("isIdle", false);
        myAnim.SetBool("isAttacking", false);
        myAnim.SetBool("isWalking", false);
        
        switch (_state)
        {
            case AIState.Idle:
            case AIState.Walking:
                var direction = GetMovementDirection();
                if (TargettedPlayerController == null)
                {
                    if (direction == Vector3.zero)
                    {
                        myAnim.Play("beaverIdle");
                        myAnim.SetBool("isIdle", true);
                    }
                    else
                    {
                        myAnim.Play("beaverWalk");
                        myAnim.SetBool("isWalking", true);
                    }
                }
                else
                {
                    _attackTimer = 0.0f;
                    _state = AIState.Attacking;
                }
                break;

            case AIState.Attacking:
                myAnim.Play("beaverAttack");
                myAnim.SetBool("isAttacking", true);
                if (_attackTimer > MAX_ATTACK_TIME * 0.5f)
                {
                    if (CheckPlayerDistance(TargettedPlayerController))
                    {
                        TargettedPlayerController.Stats.TakeDamage(gameObject, Stats.Attack);
                    }
                    TargettedPlayerController = null;
                    _state = AIState.Attacked;
                }
                break;

            case AIState.Attacked:
                myAnim.Play("beaverAttack");
                myAnim.SetBool("isAttacking", true);
                if (_attackTimer > MAX_ATTACK_TIME)
                {
                    _state = AIState.Idle;
                }
                break;
        }
    }

    protected override Vector3 GetMovementDirection()
    {
        // Check the state
        switch (_state)
        {
            case AIState.Attacked:
            case AIState.Attacking:
                return Vector3.zero;

            default:
            case AIState.Idle:
            case AIState.Walking:
                return base.GetMovementDirection();
        }
    }

    private bool CheckPlayerDistance(GGJ_PlayerController playerController)
    {
        // Find direction between enemy and player
        var enemyPosition = RigidBody.transform.position;
        var playerPosition = playerController.GetComponent<Rigidbody>().transform.position;
        var direction = playerPosition - enemyPosition;

        // Check the distance between the enemy and any object between them and the player (including the player)
        var raycastHit = new RaycastHit();
        var raycastResult = Physics.Raycast(enemyPosition, direction, out raycastHit);

        // If we're too close to another object, don't move
        var controller = raycastHit.collider.GetComponent<GGJ_BaseController>();
        return raycastResult && controller is GGJ_PlayerController && raycastHit.distance < 2.5f;
    }
}
