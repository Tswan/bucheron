using UnityEngine;
using System.Collections;

public class GGJ_Snowman : GGJ_EnemyController {

    public GameObject snowball;
    public GameObject hand;
    private GameObject player;
	// Use this for initialization
	protected override void Start () {
        base.Start();
        throwSnowball();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    { 
        base.FixedUpdate();

        Vector2 angle = Vector3.AngleBetween(new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z), new Vector3(player.transform.position.x, 0, player.transform.position.z));

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
