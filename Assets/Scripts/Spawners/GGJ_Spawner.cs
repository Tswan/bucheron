﻿using UnityEngine;
using System.Collections;

public class GGJ_Spawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float MaxSpawnDistance = 100.0f;
    public float SpawnTime = 2.5f;

    private GGJ_Player _collidedPlayer;
    private float _timeElapsedSinceLastSpawn;
    private int _spawnedCount;

    public int MaxSpawnCount;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        _collidedPlayer = null;
        _timeElapsedSinceLastSpawn = 0.0f;
        _spawnedCount = 0;
    }
    
    void Update()
    {
        // Check whether we're spawning yet
        if (_collidedPlayer != null)
        {
            // Check whether we should spawn an enemy
            if (_timeElapsedSinceLastSpawn > SpawnTime)
            {
                // Calculate enemy spawn local position
                var localEnemyPosition = Random.onUnitSphere;
                localEnemyPosition.y = gameObject.transform.localPosition.y;
                localEnemyPosition *= Random.Range(0.0f, MaxSpawnDistance);

                // Spawn new enemy
                Instantiate(EnemyPrefab, gameObject.transform.localPosition + localEnemyPosition, Quaternion.identity);
                Debug.Log(string.Format("Spawned enemy {0} of {1}.", _spawnedCount + 1, MaxSpawnCount));

                // Increment the spawned count
                _spawnedCount++;

                // Reset spawn timer
                _timeElapsedSinceLastSpawn = 0.0f;

                // Check whether we should still be running after this
                if (_spawnedCount >= MaxSpawnCount)
                {
                    // Destory this as we're done with it
                    Debug.Log(string.Format("Max spawn count reached ({0}/{1}), destorying spawner.", _spawnedCount, MaxSpawnCount));
                    Destroy(this);
                }
            }
            else
            {
                // Increment the time
                _timeElapsedSinceLastSpawn += Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        _collidedPlayer = other.gameObject.GetComponent<GGJ_Player>();
    }
}
