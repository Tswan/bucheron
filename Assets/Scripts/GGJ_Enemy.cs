using System;
using System.Collections;

using UnityEngine;

public class GGJ_Enemy : MonoBehaviour, IDamagable
{

	private Stats myStats;

	private void Awake()
	{
		DontDestroyOnLoad(this);
	}

	private void Start()
	{
		myStats = GetComponent<Stats> ();
	}

	private void Update()
	{

	}

	public void OnDamage(int damage)
	{
		// TODO:
		myStats.HealthCurrent -= damage;
	}

	public void OnKill()
	{
		// TODO:
		Destroy(gameObject);
	}

	private void onSee()
	{

	}
		
}