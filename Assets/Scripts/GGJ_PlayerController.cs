using System;
using System.Collections;

using UnityEngine;

public class GGJ_PlayerController : MonoBehaviour, IDamagable
{
	public Camera MainCamera;

    public int Currency;
    private DateTime _startTime;

	private Stats myStats;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _startTime = DateTime.UtcNow;
		myStats = GetComponent<Stats> ();
    }

    private void Update()
	{
    }

	public void OnDamage(int damage)
    {
		// DEBUG: Log the damage
		Debug.Log(string.Format ("Damaging player for {0} damage.", damage));

        // TODO: Play audio
        Debug.Log("TODO: Play audio for damaging player.");

        // Shake the camera for an amount of time dependant on the damage
        MainCamera.GetComponent<GGJ_CameraShake>().ShakeTime = damage * 0.1f;
    }

	public void OnKill()
	{
		// DEBUG: Log killing player
		Debug.Log("Player has been killed.");

        // TODO: Play audio
        Debug.Log("TODO: Player audio for player dying.");

        // TODO: Handle player death
        Debug.Log("TODO: Handle player death.");
    }
}
