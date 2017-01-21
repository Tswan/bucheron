using System;
using System.Collections;

using UnityEngine;

public class GGJ_Currency : MonoBehaviour
{
    private const float MAX_IGNORE_PLAYER_LIFE = 0.25f;

    public int CurrencyAmount;
    public AudioClip OnPickup;

    private Transform _transform;
    private float _lifeTimer;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        _lifeTimer = 0.0f;
        gameObject.AddComponent<GGJ_DeathFadeTimer>();

        // TODO: Play sound effect for currency drop
        Debug.Log("TODO: Play sound effect for currency drop.");
    }

    private void Update()
    {
        _lifeTimer += Time.deltaTime;
        _transform.Rotate(0.0f, 180.0f * Time.deltaTime, 0.0f);
    }

    private void OnCollisionStay(Collision other)
    {
        // Check whether we should be collidable yet
        if (_lifeTimer > MAX_IGNORE_PLAYER_LIFE)
        {
            // Check whether the other object is a player
            var player = other.gameObject.GetComponent<GGJ_PlayerController>();
            if (player != null)
            {
                // Add the currency amount to the player stats object
                player.Currency += CurrencyAmount;

                // Play audio
                player.GetComponent<AudioSource>().PlayOneShot(OnPickup, 1.0f);

                // DEBUG: Log that the player is ignoring this object
                Debug.Log(string.Format("Adding {0} to player ({1}), player now has {2}.", CurrencyAmount, player.tag, player.Currency));

                // Destory this object
                Destroy(gameObject);
            }
        }
    }
}
