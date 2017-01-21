using UnityEngine;
using System.Collections;

public class stats : MonoBehaviour {

	public int healthMax;
	public int healthCurr;
	public int attack;
	public int defense;

	public int ammo;

	// Use this for initialization
	void Start () {
		healthCurr = healthMax;
	}

}
