using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GGJ_PlayerController : GGJ_BaseController
{
	public Camera MainCamera;
    public int Currency;
    public GameObject Puck;
    public Slider hpSlider;
    public Text money;
    public Text pucks;

    [HideInInspector]
    public int KillCount { get; set; }

    private DateTime _startTime;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    protected override void Start()
    {
        base.Start();
        hpSlider.maxValue = GetComponent<Stats>().HealthMax;
        hpSlider.value = GetComponent<Stats>().HealthMax;
        _startTime = DateTime.UtcNow;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void Update()
	{
    }

    protected override void FixedUpdate()
    {
        _animator.SetBool("isRunning", false);
        _animator.SetBool("isSwinging", false);
        _animator.SetBool("isShooting", false);
        _animator.SetBool("isWaving", false);
        _animator.SetBool("isBlocking", false);
        _animator.SetBool("isHit", false);

        money.text = Currency.ToString();
        pucks.text = Stats.Ammo.ToString();

        base.FixedUpdate();

        if (RigidBody.velocity.y <= 0.01f && RigidBody.velocity.y >= -0.01f)
        {
            _animator.SetBool("isJumping", false);
        }

        if (Input.GetAxis("Y") > 0 && _animator.GetBool("isJumping") == false)
        {
            meleeAttack();
        }
        if (Input.GetAxis("B") > 0)
        {
            if (RigidBody.velocity.y <= 0.01f && RigidBody.velocity.y >= -0.01f && _animator.GetBool("isSwinging") == false)
            {
                jump();
            }
        }
        if (Input.GetAxis("A") > 0)
        {
            block();
        }
        if (Input.GetAxis("X") > 0)
        {
            rangedAttack();
        }
        if (Input.GetAxis("L") > 0)
        {
            OnDamage(gameObject, 1);
        }
        if (Input.GetAxis("R") > 0)
        {
            wave();
        }
        if (Input.GetAxis("Start") > 0)
        {

        }
        if (Input.GetAxis("Select") > 0)
        {

        }
    }

	public override void OnDamage(GameObject other, int damage)
    {

        startHit();
        if (_animator.GetBool("isBlocking") == true)
        { 
            damage = 0;
        }

		// DEBUG: Log the damage
		Debug.Log(string.Format ("Damaging player for {0} damage.", damage));

        // TODO: Play audio
        Debug.Log("TODO: Play audio for damaging player.");

        Stats._healthCurrent -= damage;
        hpSlider.value -= damage;

        // Shake the camera for an amount of time dependant on the damage
        //MainCamera.GetComponent<GGJ_CameraShake>().ShakeTime = damage * 0.1f;
    }

	public override void OnKill(GameObject other)
	{
		// DEBUG: Log killing player
		Debug.Log("Player has been killed.");

        // TODO: Play audio
        Debug.Log("TODO: Player audio for player dying.");

        // TODO: Handle player death
        Debug.Log("TODO: Handle player death.");
    }
    
    private void jump()
    {
        /*
		Vector3 vel = rb.velocity;
		vel.y = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z);
		rb.AddForce (Vector3.Normalize (vel) * jumpSpeed, ForceMode.Impulse);
		*/

        _animator.SetBool("isJumping", true);


        RigidBody.AddForce(Vector3.up * Stats.JumpSpeed);

        //rb.velocity = new Vector3 (rb.velocity.x, jumpSpeed, rb.velocity.z);
    }

    private void meleeAttack()
    {
        _animator.SetBool("isSwinging", true);
    }

    private void block()
    {
        _animator.SetBool("isBlocking", true);
    }

    private void wave()
    {
        _animator.SetBool("isWaving", true);
    }

    private void rangedAttack()
    {
        _animator.SetBool("isShooting", true);
    }

    public void chill()
    {
        _animator.Play("chill");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Stats>().TakeDamage(gameObject, Stats.Attack);
        }
    }

    public void shootPuck()
    {
        if (Stats.Ammo > 0)
        {
            Stats.Ammo--;
            var newPuck = Instantiate(Puck, transform.FindChild("puckEmitter").gameObject.transform.position, Quaternion.identity) as GameObject;
            if (transform.rotation.y > 0)
            {
                newPuck.GetComponent<puck>().shoot(50);
            }
            else
            {
                newPuck.GetComponent<puck>().shoot(-50);
            }
        }
    }

    private void startHit()
    {
        _animator.SetBool("isHit", true);
    }

    private void endHit()
    {


    }

    protected override Vector3 GetMovementDirection()
    { 
        if (RigidBody.velocity.y <= 0.01f && RigidBody.velocity.y >= -0.01f)
        {
            Vector3 diection = Vector3.zero;

            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                //x = moveSpeed * Time.fixedDeltaTime;
                //print ("RIGHT");
                //rb.AddForce (Vector3.right * moveSpeed);
                _animator.SetBool("isRunning", true);
                diection += Vector3.right;
            }
            else if (Input.GetAxis("Horizontal") < -0.1f)
            {
                //print ("LEFT");
                //x = -moveSpeed * Time.fixedDeltaTime;
                //rb.AddForce (Vector3.left * moveSpeed);
                _animator.SetBool("isRunning", true);
                diection += Vector3.left;
            }

            if (Input.GetAxis("Vertical") < -0.1f)
            {
                //print ("UP");
                //z = moveSpeed * Time.fixedDeltaTime;
                //rb.AddForce (Vector3.forward * moveSpeed);
                _animator.SetBool("isRunning", true);
                diection += Vector3.forward;
            }
            else if (Input.GetAxis("Vertical") > 0.1f)
            {
                //print ("DOWN");
                //z = -moveSpeed * Time.fixedDeltaTime;
                //rb.AddForce (-Vector3.forward * moveSpeed);
                _animator.SetBool("isRunning", true);
                diection += Vector3.back;
            }

            return Vector3.Normalize(diection);
        }
        else
        {
            return Vector3.zero;
        }
    }
}
