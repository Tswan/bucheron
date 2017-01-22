using UnityEngine;
using System.Collections;

public class Beaver : GGJ_EnemyController
{
    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        myAnim.SetBool("isIdle", false);
        myAnim.SetBool("isAttacking", false);
        myAnim.SetBool("isWalking", false);

        switch (State)
        {
            case AIState.Idle:
                myAnim.Play("beaverIdle");
                myAnim.SetBool("isIdle", true);
                break;

            case AIState.Walking:
                myAnim.Play("beaverWalk");
                myAnim.SetBool("isWalking", true);
                break;

            case AIState.Attacking:
            case AIState.Attacked:
                myAnim.Play("beaverAttack");
                myAnim.SetBool("isAttacking", true);
                break;
        }
    }
}
