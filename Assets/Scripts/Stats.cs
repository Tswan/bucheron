using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour
{
    public float MoveSpeed;
    public float JumpSpeed;

    public int HealthMax;
    public int Attack;
    public int Defense;

    public int Ammo;

    public int _healthCurrent;
    private bool _invincible;

    // Use this for initialization
    void Start()
    {
        _healthCurrent = HealthMax;
        _invincible = false;
    }

    public void TakeDamage(GameObject other, int damage)
    {
        // Retrieve the controller
        var controller = gameObject.GetComponent<GGJ_BaseController>();

        // Check the controller still exists, otherwise this enemy may be dead already
        if (controller != null)
        {
            // Decrement health
            _healthCurrent -= damage;

            // Call the on dmage event for the controller
            controller.OnDamage(other, damage);

            // Check whether we should kill the parent object
            if (_healthCurrent <= 0)
            {
                controller.OnKill(other);
            }
        }
    }
}
