using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {

    }
    
    void Update()
    {
    }
}
