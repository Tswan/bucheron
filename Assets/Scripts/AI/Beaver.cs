using UnityEngine;
using System.Collections;

public class Beaver : GGJ_EnemyController
{

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        base.FixedUpdate();
        if(RigidBody.velocity.x != 0 || RigidBody.velocity.z != 0)
        {
            walk();
        }
        else
        {
            idle();
        }
	}

    public void idle()
    {
        myAnim.Play("beaverIdle");
        myAnim.SetBool("isIdle", true);
        myAnim.SetBool("isAttacking", false);
        myAnim.SetBool("isWalking", false);
    }

    public void attack()
    {
        myAnim.SetBool("isAttacking", true);
    }

    public void walk()
    {
        myAnim.SetBool("isWalking", true);
    }

}
