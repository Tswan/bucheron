using UnityEngine;
using System.Collections;

public interface IDamagable 
{
	void OnDamage (int damage);
	void OnKill();
}
