using System;
using System.Collections;

using UnityEngine;

public class GGJ_Player : MonoBehaviour, IDamagable
{
	public Camera MainCamera;

    public int Currency;
    private DateTime _startTime;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _startTime = DateTime.UtcNow;
    }

    private void Update()
	{
		MainCamera.GetComponent<GGJ_CameraShake>().ShakeTime = 1.0f;
    }

	public void OnDamage(int damage)
    {
		// DEBUG: Log the damage
		Debug.Log(string.Format ("Damaging player for {0} damage.", damage));

        // TODO: Play audio

        // Shake the camera for an amount of time dependant on the damage
        MainCamera.GetComponent<GGJ_CameraShake>().ShakeTime = damage * 0.1f;
    }

	public void OnKill()
	{
		// DEBUG: Log killing player
		Debug.Log("Player has been killed.");

		// TODO: Handle player death
	}
}
