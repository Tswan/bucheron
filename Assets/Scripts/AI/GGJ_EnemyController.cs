using System;
using System.Collections.Generic;

using UnityEngine;

public class GGJ_EnemyController : GGJ_BaseController, IDamagable
{
    [HideInInspector]
    public float MaxViewDistance { get; set; }

    private Stats _stats;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _stats = GetComponent<Stats>();
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

    protected override Vector3 GetMovementDirection()
    {
        // Check whether the enemy can see the player by attempting to raycast to the player
        foreach (var ggjPlayerController in GameObject.FindObjectsOfType<GGJ_PlayerController>())
        {
            // Find direction between enemy and player
            var direction = Vector3.Normalize(gameObject.transform.position - ggjPlayerController.gameObject.transform.position);

            // Cast a ray and check the result
            var raycastHit = new RaycastHit();
            if (Physics.Raycast(gameObject.transform.position, direction, out raycastHit, MaxViewDistance))
            {
                var playerHit = raycastHit.rigidbody.gameObject.GetComponent<GGJ_PlayerController>();
                if (playerHit != null)
                {
                    // TODO: Move toward the player
                    Debug.Log("TODO: Move enemy toward the player.");
                    return direction;
                }
            }
        }

        // If all else fails return forwards
        return Vector3.forward;
    }
}