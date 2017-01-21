using UnityEngine;
using System.Collections;

<<<<<<< HEAD
public class Controls : MonoBehaviour 
{
	public float moveSpeed;
	public float jumpSpeed;

	private Vector3 vel;
	private Stats myStats;

	Rigidbody rb;
	SpriteRenderer sr;
	Animator anim;

	public GameObject puck;

	// Use this for initialization
	void Start () 
	{
		sr = GetComponent<SpriteRenderer> ();
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		myStats = GetComponent<Stats> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		anim.SetBool ("isRunning", false);
		anim.SetBool ("isSwinging", false);
		anim.SetBool ("isShooting", false);
		if (gameObject.GetComponent<Rigidbody> ().velocity.y <= 0.01f && gameObject.GetComponent<Rigidbody> ().velocity.y >= -0.01f) {
			//checkMovement ();
			checkVelocity ();
		}

		if (Input.GetAxis ("Y") > 0 && anim.GetBool("isJumping") == false) {
			meleeAttack ();
		}
		if (Input.GetAxis ("B") > 0) {
			if (gameObject.GetComponent<Rigidbody> ().velocity.y <= 0.01f && gameObject.GetComponent<Rigidbody> ().velocity.y >= -0.01f && anim.GetBool("isSwinging") == false) {
				jump ();
			}
		}
		if (Input.GetAxis ("A") > 0) {

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

	private void meleeAttack()
	{
		anim.SetBool ("isSwinging", true);
	}

	private void rangedAttack()
	{
		anim.SetBool ("isShooting", true);
	}

	private void checkVelocity()
	{

		float x = 0;
		float z = 0;

		if (Input.GetAxis ("Horizontal") > 0.1f) 
		{
			transform.rotation = new Quaternion(0, 180, 0, 0);
			anim.SetBool ("isRunning", true);
			//rb.velocity = Vector3.right * moveSpeed;
			x = moveSpeed;
			//transform.rotation = new Quaternion(0, 180, 0, 0);
		} 
		else if (Input.GetAxis ("Horizontal") < -0.1f) 
		{
			transform.rotation = new Quaternion(0, 0, 0, 0);
			anim.SetBool ("isRunning", true);
			x = -moveSpeed;
			//rb.velocity = Vector3.left * moveSpeed;
			//transform.rotation = new Quaternion(0, 0, 0, 0);
		}

		if (Input.GetAxis ("Vertical") < -0.1f) 
		{


			anim.SetBool ("isRunning", true);
			z = moveSpeed;
			//rb.velocity = Vector3.forward * moveSpeed;
		} 

		else if (Input.GetAxis ("Vertical") > 0.1f) 
		{

			anim.SetBool ("isRunning", true);
			z = -moveSpeed;
			//rb.velocity = Vector3.back * moveSpeed;
		}


		rb.velocity = new Vector3 (x, rb.velocity.y, z * 0.75f);

	}

	private void checkMovement()
	{

		if (gameObject.GetComponent<Rigidbody> ().velocity.y <= 0.01f && gameObject.GetComponent<Rigidbody> ().velocity.y >= -0.01f) {

			Vector3 force = Vector3.zero;

			if (Input.GetAxis ("Horizontal") > 0.1f) {
				//x = moveSpeed * Time.fixedDeltaTime;
				//print ("RIGHT");
				//rb.AddForce (Vector3.right * moveSpeed);

				transform.rotation = new Quaternion(0, 180, 0, 0);
				//anim.Play ("run");
				force += Vector3.right;

			} else if (Input.GetAxis ("Horizontal") < -0.1f) {
				//print ("LEFT");
				//x = -moveSpeed * Time.fixedDeltaTime;
				//rb.AddForce (Vector3.left * moveSpeed);
				//anim.Play ("run");
				transform.rotation = new Quaternion(0, 0, 0, 0);
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

	public void chill()
	{
		anim.Play ("chill");
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Enemy") 
		{
			col.gameObject.GetComponent<Stats> ().TakeDamage (myStats.Attack);
		}
	}

	public void shootPuck()
	{
		if (myStats.Ammo > 0) 
		{
			myStats.Ammo -= 1;
		}
	}

=======
public class Controls : MonoBehaviour
{
    private Stats _stats;
    private Rigidbody _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animation;

    // Use this for initialization
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody>();
        _animation = GetComponent<Animator>();
        _stats = GetComponent<Stats>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        _animation.SetBool("isRunning", false);
        _animation.SetBool("isSwinging", false);
        if (gameObject.GetComponent<Rigidbody>().velocity.y <= 0.01f && gameObject.GetComponent<Rigidbody>().velocity.y >= -0.01f)
        {
            //checkMovement ();
            CheckVelocity();
        }

        if (Input.GetAxis("Y") > 0 && _animation.GetBool("isJumping") == false)
        {
            MeleeAttack();
        }
        if (Input.GetAxis("B") > 0)
        {
            if (gameObject.GetComponent<Rigidbody>().velocity.y <= 0.01f && gameObject.GetComponent<Rigidbody>().velocity.y >= -0.01f && _animation.GetBool("isSwinging") == false)
            {
                Jump();
            }
        }
        if (Input.GetAxis("A") > 0)
        {

        }
        if (Input.GetAxis("X") > 0)
        {
            RangedAttack();
        }
        if (Input.GetAxis("L") > 0)
        {

        }
        if (Input.GetAxis("R") > 0)
        {

        }
        if (Input.GetAxis("Start") > 0)
        {

        }
        if (Input.GetAxis("Select") > 0)
        {

        }

    }

    private void Jump()
    {
        /*
		Vector3 vel = rb.velocity;
		vel.y = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z);
		rb.AddForce (Vector3.Normalize (vel) * jumpSpeed, ForceMode.Impulse);
		*/

        _rigidBody.AddForce(Vector3.up * _stats.JumpSpeed);

        //rb.velocity = new Vector3 (rb.velocity.x, jumpSpeed, rb.velocity.z);
    }

    private void MeleeAttack()
    {
        _animation.SetBool("isSwinging", true);
    }

    private void RangedAttack()
    {

    }

    private void CheckVelocity()
    {

        float x = 0;
        float z = 0;

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            _animation.SetBool("isRunning", true);
            //rb.velocity = Vector3.right * moveSpeed;
            x = _stats.MoveSpeed;
            //transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            _animation.SetBool("isRunning", true);
            x = -_stats.MoveSpeed;
            //rb.velocity = Vector3.left * moveSpeed;
            //transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        if (Input.GetAxis("Vertical") < -0.1f)
        {


            _animation.SetBool("isRunning", true);
            z = _stats.MoveSpeed;
            //rb.velocity = Vector3.forward * moveSpeed;
        }

        else if (Input.GetAxis("Vertical") > 0.1f)
        {

            _animation.SetBool("isRunning", true);
            z = -_stats.MoveSpeed;
            //rb.velocity = Vector3.back * moveSpeed;
        }


        _rigidBody.velocity = new Vector3(x, _rigidBody.velocity.y, z * 0.75f);

    }

    private void CheckMovement()
    {

        if (gameObject.GetComponent<Rigidbody>().velocity.y <= 0.01f && gameObject.GetComponent<Rigidbody>().velocity.y >= -0.01f)
        {

            Vector3 force = Vector3.zero;

            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                //x = moveSpeed * Time.fixedDeltaTime;
                //print ("RIGHT");
                //rb.AddForce (Vector3.right * moveSpeed);

                transform.rotation = new Quaternion(0, 180, 0, 0);
                //anim.Play ("run");
                force += Vector3.right;

            }
            else if (Input.GetAxis("Horizontal") < -0.1f)
            {
                //print ("LEFT");
                //x = -moveSpeed * Time.fixedDeltaTime;
                //rb.AddForce (Vector3.left * moveSpeed);
                //anim.Play ("run");
                transform.rotation = new Quaternion(0, 0, 0, 0);
                force += Vector3.left;
            }

            if (Input.GetAxis("Vertical") < -0.1f)
            {
                //print ("UP");
                //z = moveSpeed * Time.fixedDeltaTime;
                //rb.AddForce (Vector3.forward * moveSpeed);



                force += Vector3.forward;

            }
            else if (Input.GetAxis("Vertical") > 0.1f)
            {
                //print ("DOWN");
                //z = -moveSpeed * Time.fixedDeltaTime;
                //rb.AddForce (-Vector3.forward * moveSpeed);
                force += Vector3.back;
            }

            _rigidBody.AddForce(Vector3.Normalize(force) * _stats.MoveSpeed);

        }

    }

    public void Chill()
    {
        _animation.Play("chill");
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Stats>().TakeDamage(_stats.Attack);
        }
    }
>>>>>>> origin/master
}
