using System;
using System.Collections.Generic;

using UnityEngine;

public class GGJ_SwarmController : MonoBehaviour
{
    [HideInInspector]
    public List<GGJ_EnemyController> Enemies { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Enemies = new List<GGJ_EnemyController>();
    }

    private void Update()
    {
    }
}
