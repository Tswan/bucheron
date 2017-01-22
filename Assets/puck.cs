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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            explode(col.gameObject);
        }
    }

    private void explode(GameObject target)
    {
        target.GetComponent<Rigidbody>().AddExplosionForce(500, transform.position, 500.0f, 10.0f);
        Destroy(gameObject);

    }

}
