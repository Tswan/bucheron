using UnityEngine;
using System.Collections;

public class GGJ_Snowman : GGJ_EnemyController
{

    public GameObject snowball;
    public GameObject hand;
    private GameObject player;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        throwSnowball();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // Rotate the snowman to face the player       
        RigidBody.rotation = Quaternion.LookRotation(player.transform.position - gameObject.transform.position, Vector3.up);
    }

    private void throwSnowball()
    {
        myAnim.Play("throwSnowball");
    }

    private void makeSnowball()
    {
        GameObject newSnowball = Instantiate(snowball, hand.transform.position, Quaternion.identity) as GameObject;
    }

}
