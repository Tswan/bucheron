using System;
using System.Collections;

using UnityEngine;

public class GGJ_Enemy : MonoBehaviour, IDamagable
{
	private void Awake()
	{
		DontDestroyOnLoad(this);
	}

	private void Start()
	{
	}

	private void Update()
	{

	}

	public void OnDamage(int damage)
	{
		// TODO:
	}

	public void OnKill()
	{
		// TODO:
	}
}