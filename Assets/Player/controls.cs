using UnityEngine;
using System.Collections;

public class controls : MonoBehaviour {

	public float moveSpeed;
	public float jumpSpeed;
	public bool grounded;


	private float x;
	private float y;
	private float z;

	// Use this for initialization
	void Start () {
		grounded = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		checkMovement ();

		if (Input.GetAxis ("Y") > 0) {
			meleeAttack ();
		}
		if (Input.GetAxis ("B") > 0) {
			if (grounded) 
			{
				
			}
			if (gameObject.GetComponent<Rigidbody> ().velocity.y <= 0.01f && gameObject.GetComponent<Rigidbody> ().velocity.y >= -0.01f) {
				jump ();
			}
		}
		if (Input.GetAxis ("A") > 0) {
			print ("A");
		}
		if (Input.GetAxis ("X") > 0) {
			rangedAttack ();
		}
		if (Input.GetAxis ("L") > 0) {
			
		}
		if (Input.GetAxis ("R") > 0) {
			
		}
		if (Input.GetAxis ("Start") > 0) {
			
		}
		if (Input.GetAxis ("Select") > 0) {
			
		}

	}

	private void jump()
	{
		grounded = false;
		gameObject.GetComponent<Rigidbody> ().AddForce (transform.up * jumpSpeed);
	}

	private void meleeAttack()
	{

	}

	private void rangedAttack()
	{

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
