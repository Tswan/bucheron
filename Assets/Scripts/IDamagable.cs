using UnityEngine;
using System.Collections;

public interface IDamagable 
{
	void OnDamage (GameObject other, int damage);
	void OnKill(GameObject other);
}
