﻿using UnityEngine;
using System.Collections;

public class attack : MonoBehaviour 
{
	void OnTriggerEnter(Collider col)
	{
		switch (col.gameObject.tag) 
		{
			case "Enemy":
				col.GetComponent<Stats> ().TakeDamage (gameObject, gameObject.GetComponentInParent<Stats>().Attack);
				gameObject.GetComponent<CapsuleCollider> ().enabled = false;
				break;
		}
	}
}
