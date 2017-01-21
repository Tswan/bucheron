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
            // Find direction between enemy and player
            var direction = Vector3.Normalize(gameObject.transform.position - ggjPlayerController.gameObject.transform.position);

            // Cast a ray and check the result
            var raycastHit = new RaycastHit();
            if (Physics.Raycast(gameObject.transform.position, direction, out raycastHit))
            {

            }
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