using System;

using UnityEngine;
using UnityEngine.UI;

public class GGJ_PlayerController : GGJ_BaseController
{
    public enum PlayerState
    {
        Alive,
        Dead,
    };

    public Camera MainCamera;
    public int Currency;
    public GameObject Puck;
    public Slider hpSlider;
    public AudioClip OnHitAudio;
    public AudioClip OnSwingAudio;
    public AudioClip OnPuckAudio;
    public AudioClip RunningAudio;

    public Text Money;
    public Text Pucks;
    public Text WaveNumber;
    public Text WaveAnnouncement;

    [HideInInspector]
    public AudioSource OnHitAudioSource { get; private set; }
    [HideInInspector]
    public AudioSource OnSwingAudioSource { get; private set; }
    [HideInInspector]
    public AudioSource OnPuckAudioSource { get; private set; }
    [HideInInspector]
    public AudioSource GenericAudioSource { get; private set; }

    [HideInInspector]
    public PlayerState State { get; private set; }

    private DateTime _startTime;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _runningAudioSource;
    private int _killCount;
    private int _waveNumber;
    private Vector3 _startPosition;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    protected override void Start()
    {
        base.Start();

        _startPosition = gameObject.transform.position;
        ResetDefaults();

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        OnHitAudioSource = gameObject.AddComponent<AudioSource>();
        OnHitAudioSource.clip = OnHitAudio;
        OnHitAudioSource.loop = false;
        OnHitAudioSource.volume = 0.5f;

        OnSwingAudioSource = gameObject.AddComponent<AudioSource>();
        OnSwingAudioSource.clip = OnSwingAudio;
        OnSwingAudioSource.loop = false;
        OnSwingAudioSource.volume = 2.0f;

        OnPuckAudioSource = gameObject.AddComponent<AudioSource>();
        OnPuckAudioSource.clip = OnPuckAudio;
        OnPuckAudioSource.loop = false;
        OnPuckAudioSource.volume = 2.0f;

        GenericAudioSource = gameObject.AddComponent<AudioSource>();
        GenericAudioSource.loop = false;
        GenericAudioSource.volume = 1.0f;

        _runningAudioSource = gameObject.AddComponent<AudioSource>();
        _runningAudioSource.clip = RunningAudio;
        _runningAudioSource.loop = true;
        _runningAudioSource.volume = 0.25f;
    }

    private void ResetDefaults()
    {
        State = PlayerState.Alive;
        _waveNumber = 0;
        IncrementWave(1);

        hpSlider.maxValue = GetComponent<Stats>().HealthMax;
        hpSlider.value = GetComponent<Stats>().HealthMax;
        _startTime = DateTime.UtcNow;

        gameObject.transform.position = _startPosition;
    }

    private void Update()
    {
    }

    protected override void FixedUpdate()
    {
        _animator.SetBool("isDead", false);
        _animator.SetBool("isRunning", false);
        _animator.SetBool("isSwinging", false);
        _animator.SetBool("isShooting", false);
        _animator.SetBool("isWaving", false);
        _animator.SetBool("isBlocking", false);

        switch (State)
        {
            case PlayerState.Dead:
                _animator.SetBool("isDead", true);
                if (Input.GetAxis("Start") > 0)
                {
                    ResetDefaults();
                }
                break;

            case PlayerState.Alive:
                Money.text = Currency.ToString();
                Pucks.text = Stats.Ammo.ToString();

                base.FixedUpdate();

                // Check whether we're still running
                if (!_animator.GetBool("isRunning"))
                {
                    StopRunningAudio();
                }

                if (RigidBody.velocity.y <= 0.01f && RigidBody.velocity.y >= -0.01f)
                {
                    _animator.SetBool("isJumping", false);
                }

                if (Input.GetAxis("Y") > 0 && _animator.GetBool("isJumping") == false || Input.GetKey(KeyCode.M) && _animator.GetBool("isJumping") == false)
                {
                    meleeAttack();
                }
                if (Input.GetAxis("B") > 0 || Input.GetKey(KeyCode.Space))
                {
                    if (RigidBody.velocity.y <= 0.01f && RigidBody.velocity.y >= -0.01f && _animator.GetBool("isSwinging") == false)
                    {
                        jump();
                    }
                }
                if (Input.GetAxis("A") > 0)
                {
                    //block();
                }
                if (Input.GetAxis("X") > 0 || Input.GetKey(KeyCode.N))
                {
                    rangedAttack();
                }
                if (Input.GetAxis("L") > 0)
                {

                }
                if (Input.GetAxis("R") > 0)
                {
                    wave();
                }
                if (Input.GetAxis("Select") > 0)
                {

                }
                break;
        }
    }

    public override void OnDamage(GameObject other, int damage)
    {
        // Check the state
        if (State != PlayerState.Dead)
        {
            startHit();
            if (!_animator.GetBool("isBlocking"))
            {
                damage = 0;
            }

            // DEBUG: Log the damage
            Debug.Log(string.Format("Damaging player for {0} damage.", damage));

            // TODO: Play audio
            Debug.Log("TODO: Play audio for damaging player.");

            // Decrement the HP slider
			hpSlider.value = Stats._healthCurrent;//damage;

            // Shake the camera for an amount of time dependant on the damage
            var cameraShake = MainCamera.GetComponent<GGJ_CameraShake>();
            if (cameraShake != null)
            {
                cameraShake.ShakeTime = damage * 0.1f;
            }
        }
    }

    public override void OnKill(GameObject other)
    {
        // Check the state 
        if (State != PlayerState.Dead)
        {
            // DEBUG: Log killing player
            Debug.Log("Player has been killed.");

            // TODO: Play audio
            Debug.Log("TODO: Player audio for player dying.");

            // Handle player death
            State = PlayerState.Dead;
            _animator.SetBool("isDead", true);
            WaveAnnouncement.text = string.Format("You Have Died, Press Start...");
            var fadeTextInOut = WaveAnnouncement.gameObject.AddComponent<GGJ_FadeTextInOut>();
            fadeTextInOut.FadeTime = 1.0f;
            fadeTextInOut.LifeTime = float.MaxValue;
        }
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
            OnPuckAudioSource.Play();
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
        if (_animator.GetBool("isDead") == false)
            _animator.SetBool("isHit", true);
    }

    private void endHit()
    {
        if (Stats._healthCurrent > 0)
            _animator.SetBool("isHit", false);
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

    private void PlayMeleeAudio()
    {
        OnSwingAudioSource.Play();
    }

    private void StartRunningAudio()
    {
        _runningAudioSource.Play();
    }

    private void StopRunningAudio()
    {
        _runningAudioSource.Stop();
    }

    public void IncrementKill(int amount)
    {
        // Increment the kill count
        _killCount += amount;

        // Increase the wave if necessary
        Debug.Log("TODO: Make wave check more intelligent, more fun: just better.");
        if (_killCount % 10 == 0)
        {
            // Increment the wave number
            IncrementWave(1);

            // Inform all the spawners they need to increase their waves
            foreach (var spawner in GameObject.FindObjectsOfType<GGJ_Spawner>())
            {
                spawner.WaveIncrease(_waveNumber);
            }
        }
    }

    private void IncrementWave(int amount)
    {
        _waveNumber += amount;
        WaveNumber.text = _waveNumber.ToString();

        WaveAnnouncement.text = string.Format("Wave #{0}", _waveNumber);
        var fadeTextInOut = WaveAnnouncement.gameObject.AddComponent<GGJ_FadeTextInOut>();
        fadeTextInOut.FadeTime = 1.0f;
        fadeTextInOut.LifeTime = 3.0f;
    }

    public void buyCoffee()
    {

        for (float x = Stats._healthCurrent / 10; x < 10; x++)
        {

            if (Currency > 0)
            {
                Currency -= 1;
                Stats._healthCurrent += 10;
                if (Stats._healthCurrent > Stats.HealthMax)
                {
                    Stats._healthCurrent = Stats.HealthMax;
                }
            }
        }

        for (int x = Stats.Ammo; x < 10; x++)
        {
            if (Currency > 0)
            {
                Currency -= 1;
                Stats.Ammo += 1;
            }
        }

    }

}
