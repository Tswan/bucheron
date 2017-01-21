using System;
using System.Collections;

using UnityEngine;

public class GGJ_Player : MonoBehaviour
{
    public int HitPoints { get; set; }
    public Camera MainCamera { get; set; }

    private int _currency;
    private DateTime _startTime;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _startTime = DateTime.UtcNow;
    }

    private void Update()
    {

    }

    public void HurtPlayer(int damage)
    {
        // TODO: Play audio

        // TODO: Shake camera

        // Decrement health
        HitPoints -= damage;
        Debug.Log(string.Format("Damaging player by {0} points.", damage));
        if (HitPoints >= 0)
        {
            // TODO: Kill player
            Debug.Log("TODO: Kill player.");
        }

        // Shake the camera for an amount of time dependant on the damage
        MainCamera.GetComponent<CameraShake>().ShakeTime = damage * 0.1f;
    }
}
