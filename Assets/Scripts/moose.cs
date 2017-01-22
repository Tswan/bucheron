using UnityEngine;
using System.Collections;

public class moose : GGJ_EnemyController
{

	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {

        base.FixedUpdate();
        //float d = Vector3.Distance(gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

    }

    public void idle()
    {
        myAnim.Play("mooseIdle");
        myAnim.SetBool("isIdle", true);
        myAnim.SetBool("isAttacking", false);
        myAnim.SetBool("isWalking", false);
    }

    public void attack()
    {
        print("MOOOOSE");
        Vector3 targetPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3.MoveTowards(transform.position, new Vector3(targetPos.x, transform.position.y, targetPos.z), 20);
    }

    public void walk()
    {
        myAnim.SetBool("isWalking", true);
    }
}
