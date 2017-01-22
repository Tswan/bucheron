using System;
using System.Collections;

using UnityEngine;

public class GGJ_Moose : GGJ_EnemyController
{
    public float SecondsBetweenAttacks = 1.0f;
    public float ChargeMovementMultiplier = 5.0f;
    public float ChargeWaitTime = 1.0f;
    public float MaxChargeTime = 3.0f;

    private enum AIState
    {
        Idle,
        PlayerSeen,
        Charging,
    }

    private AIState _state;
    private float _chargeTimer;
    private Vector3 _chargeDirection;
    private DateTime _timeSinceLastPlayerDamage;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        _state = AIState.Idle;
        _chargeTimer = 0.0f;
        _timeSinceLastPlayerDamage = DateTime.MinValue;
        myAnim.Play("mooseIdle");
        myAnim.SetBool("isIdle", true);
        myAnim.SetBool("isAttacking", false);
        myAnim.SetBool("isWalking", false);
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        myAnim.SetBool("isIdle", false);
        myAnim.SetBool("isAttacking", false);
        myAnim.SetBool("isWalking", false);

        base.FixedUpdate();

        switch (_state)
        {
            case AIState.Idle:
                myAnim.Play("mooseIdle");
                myAnim.SetBool("isIdle", true);
                if (MoveToPlayerController != null)
                {
                    _state = AIState.PlayerSeen;
                    _chargeTimer = 0.0f;
                }
                break;

            case AIState.PlayerSeen:
                _chargeTimer += Time.fixedDeltaTime;
                if (_chargeTimer > MaxChargeTime)
                {
                    _chargeDirection = GetMovementDirection();
                    _state = AIState.Charging;
                    _chargeTimer = 0.0f;
                }
                myAnim.Play("mooseIdle");
                myAnim.SetBool("isIdle", true);
                break;

            case AIState.Charging:
                _chargeTimer += Time.fixedDeltaTime;
                if (_chargeTimer > ChargeWaitTime)
                {
                    if (_chargeTimer > ChargeWaitTime + MaxChargeTime)
                    {
                        myAnim.Play("mooseIdle");
                        myAnim.SetBool("isIdle", true);
                        _state = AIState.Idle;
                    }
                    else
                    {
                        myAnim.Play("mooseAttack");
                        myAnim.SetBool("isAttacking", true);
                    }
                }
                else
                {
                    myAnim.Play("mooseIdle");
                    myAnim.SetBool("isIdle", true);
                }
                break;
        }
    }

    protected override Vector3 GetMovementDirection()
    {
        switch (_state)
        {
            case AIState.PlayerSeen:
                return base.GetMovementDirection();

            case AIState.Charging:
                return _chargeDirection;

            default:
            case AIState.Idle:
                return base.GetMovementDirection();
        }
    }

    protected override float GetMovementSpeed()
    {
        switch (_state)
        {
            case AIState.PlayerSeen:
                return 0.0f;

            case AIState.Charging:
                return _chargeTimer > ChargeWaitTime ? base.GetMovementSpeed() * ChargeMovementMultiplier : 0.0f;

            default:
            case AIState.Idle:
                return base.GetMovementSpeed();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        // Check that we are charging
        if (_state == AIState.Charging && _chargeTimer > ChargeWaitTime)
        {
            // Only damage a player if we're charging
            var playerController = other.gameObject.GetComponent<GGJ_PlayerController>();
            if (playerController != null)
            {
                // Check the last time we damaged the player
                if ((DateTime.UtcNow - _timeSinceLastPlayerDamage).TotalSeconds > SecondsBetweenAttacks)
                {
                    // Reset the damage time
                    _timeSinceLastPlayerDamage = DateTime.UtcNow;

                    // Damage the player
                    playerController.Stats.TakeDamage(gameObject, Stats.Attack);
                }

                // Knock the player back
                playerController.RigidBody.AddForce(Vector3.Normalize(RigidBody.velocity) * 7.5f, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // If we hit anything that isn't the player, stop charging immediately
        var playerController = other.gameObject.GetComponent<GGJ_PlayerController>();
        if (playerController == null && _state == AIState.Charging && _chargeTimer > ChargeWaitTime)
        {
            _state = AIState.Idle;
        }
    }

    public void idle()
    {
        return;
        myAnim.Play("mooseIdle");
        myAnim.SetBool("isIdle", true);
        myAnim.SetBool("isAttacking", false);
        myAnim.SetBool("isWalking", false);
    }

    public void attack()
    {
        return;
        print("MOOOOSE");
        Vector3 targetPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3.MoveTowards(transform.position, new Vector3(targetPos.x, transform.position.y, targetPos.z), 20);
    }

    public void walk()
    {
        return;
        myAnim.SetBool("isWalking", true);
    }
}
