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
    private IDamagable _damagable;

    // Use this for initialization
    void Start()
    {
        _healthCurrent = HealthMax;
        _invincible = false;
        _damagable = gameObject.GetComponent<IDamagable>();
    }

    public void TakeDamage(GameObject other, int damage)
    {
        // Decrement health
        _healthCurrent -= damage;

        // Call the on dmage event for the controller
        _damagable.OnDamage(other, damage);

        // Check whether we should kill the parent object
        if (_healthCurrent <= 0)
        {
            _damagable.OnKill(other);
            Destroy(gameObject);
        }
    }
}
