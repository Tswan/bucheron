using UnityEngine;
using System.Collections;

public class puck : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void shoot(float speed)
    {
        GetComponent<Rigidbody>().velocity = Vector3.right * speed;
        StartCoroutine("life");
    }

    private IEnumerator life()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }

}
