using UnityEngine;
using System.Collections;

public class controls : MonoBehaviour {

	public float moveSpeed;

	private float x;
	private float y;
	private float z;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		checkMovement ();

	}

	private void checkMovement()
	{
		x = 0;
		y = 0;
		z = 0;

		if (Input.GetAxis ("Horizontal") > 0.1f) {
			x = moveSpeed * Time.fixedDeltaTime;
			//print ("RIGHT");
		}
		else if (Input.GetAxis ("Horizontal") < -0.1f) {
			//print ("LEFT");
			x = -moveSpeed * Time.fixedDeltaTime;
		}

		if (Input.GetAxis ("Vertical") < -0.1f) {
			//print ("UP");
			z = moveSpeed * Time.fixedDeltaTime;
		}

		else if (Input.GetAxis ("Vertical") > 0.1f) {
			//print ("DOWN");
			z = -moveSpeed * Time.fixedDeltaTime;
		}

		transform.position += new Vector3 (x, 0, z);
	}

}
