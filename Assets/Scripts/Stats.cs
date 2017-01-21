using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour
{
    public int HealthMax;
    public int HealthCurrent;
    public int Attack;
    public int Defense;

    public int Ammo;

    private bool _invincible;
    private IDamagable _damagable;

    // Use this for initialization
    void Start()
    {
        HealthCurrent = HealthMax;
        _invincible = false;
        _damagable = gameObject.GetComponent<IDamagable>();
    }

    public void TakeDamage(int amount)
    {
        HealthCurrent -= amount;
        _damagable.OnDamage(amount);

        if (HealthCurrent <= 0)
        {
            _damagable.OnKill();
            Destroy(gameObject);
        }
    }
}
