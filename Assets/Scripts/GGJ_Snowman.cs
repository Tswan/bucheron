using UnityEngine;
using System.Collections;

public class GGJ_Snowman : GGJ_EnemyController {

    public GameObject snowball;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        throwSnowball();

    }

    // Update is called once per frame
    protected override void FixedUpdate()
    { 
        base.FixedUpdate();
	}

    private void throwSnowball()
    {
        GameObject newSnowball = Instantiate(snowball, gameObject.transform.position, Quaternion.identity) as GameObject;
    }

}
