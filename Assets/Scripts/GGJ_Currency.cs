using System;
using System.Collections;

using UnityEngine;

public class GGJ_Currency : MonoBehaviour
{
    public int CurrencyAmount;

    private Transform _transform;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        _transform.Rotate(0.0f, 180.0f * Time.deltaTime, 0.0f);
    }

    private void OnTrigger(Collider other)
    {
        // Check whether the other object is a player
        var player = other.gameObject.GetComponent<GGJ_PlayerController>();
        if (player != null)
        {
            // Add the currency amount to the player stats object
            player.Currency += CurrencyAmount;

            // TODO: Play audio
            Debug.Log("TODO: Play audio for player picking up currency.");

            // DEBUG: Log that the player is ignoring this object
            Debug.Log(string.Format("Adding {0} to player ({1}), player now has {2}.", CurrencyAmount, player.tag, player.Currency));

            // Destory this object
            Destroy(gameObject);
        }
    }
}
