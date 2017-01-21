using System;
using System.Collections.Generic;

using UnityEngine;

public class GGJ_EnemyController : MonoBehaviour, IDamagable
{
    public int Movement;

    private Stats _stats;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _stats = GetComponent<Stats>();
    }

    private void Update()
    {
        // Check whether the enemy can see the player by attempting to raycast to the player
        foreach (var ggjPlayerController in GameObject.FindObjectsOfType<GGJ_PlayerController>())
        {
            var raycastHit = new RaycastHit();
            Physics.Raycast();
        }
    }

    public void OnDamage(int damage)
    {
        // TODO:
    }

    public void OnKill()
    {
        // TODO:
        Destroy(gameObject);
    }

}