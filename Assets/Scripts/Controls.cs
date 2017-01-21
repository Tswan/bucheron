using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour 
{
	public float moveSpeed;
	public float jumpSpeed;

	private Vector3 vel;

	Rigidbody rb;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (gameObject.GetComponent<Rigidbody> ().velocity.y <= 0.01f && gameObject.GetComponent<Rigidbody> ().velocity.y >= -0.01f) {
			checkMovement ();
		}

		//checkVelocity ();

		if (Input.GetAxis ("Y") > 0) {
			StartCoroutine ("meleeAttack");
		}
		if (Input.GetAxis ("B") > 0) {
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
		/*
		Vector3 vel = rb.velocity;
		vel.y = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z);
		rb.AddForce (Vector3.Normalize (vel) * jumpSpeed, ForceMode.Impulse);
		*/

		rb.AddForce (Vector3.up * jumpSpeed);

		//rb.velocity = new Vector3 (rb.velocity.x, jumpSpeed, rb.velocity.z);
	}

	private IEnumerator meleeAttack()
	{
		CapsuleCollider col = gameObject.GetComponentInChildren<CapsuleCollider> ();
		col.enabled = true;
		yield return new WaitForSeconds (0.4f);
		col.enabled = false;
	}

	private void rangedAttack()
	{

	}

	private void checkVelocity()
	{

		if (gameObject.GetComponent<Rigidbody> ().velocity.y <= 0.01f && gameObject.GetComponent<Rigidbody> ().velocity.y >= -0.01f) {

			vel = new Vector3 (0, rb.velocity.y, 0);

			if (Input.GetAxis ("Horizontal") > 0.1f) {
				vel += Vector3.right * moveSpeed;	
			} else if (Input.GetAxis ("Horizontal") < -0.1f) {
				vel += Vector3.left * moveSpeed;
			}

			if (Input.GetAxis ("Vertical") < -0.1f) {
				vel += Vector3.forward * moveSpeed;
			} else if (Input.GetAxis ("Vertical") > 0.1f) {
				vel += Vector3.back * moveSpeed;
			}

			rb.velocity = vel;

		} 
		else
		{
			rb.velocity = vel;
		}
	}

	private void checkMovement()
	{

		if (gameObject.GetComponent<Rigidbody> ().velocity.y <= 0.01f && gameObject.GetComponent<Rigidbody> ().velocity.y >= -0.01f) {

			Vector3 force = Vector3.zero;

			if (Input.GetAxis ("Horizontal") > 0.1f) {
				//x = moveSpeed * Time.fixedDeltaTime;
				//print ("RIGHT");
				//rb.AddForce (Vector3.right * moveSpeed);

				force += Vector3.right;

			} else if (Input.GetAxis ("Horizontal") < -0.1f) {
				//print ("LEFT");
				//x = -moveSpeed * Time.fixedDeltaTime;
				//rb.AddForce (Vector3.left * moveSpeed);
				force += Vector3.left;
			}

			if (Input.GetAxis ("Vertical") < -0.1f) {
				//print ("UP");
				//z = moveSpeed * Time.fixedDeltaTime;
				//rb.AddForce (Vector3.forward * moveSpeed);

				force += Vector3.forward;

			} else if (Input.GetAxis ("Vertical") > 0.1f) {
				//print ("DOWN");
				//z = -moveSpeed * Time.fixedDeltaTime;
				//rb.AddForce (-Vector3.forward * moveSpeed);
				force += Vector3.back;
			}

			rb.AddForce (Vector3.Normalize (force) * moveSpeed);

		}

	}

}
