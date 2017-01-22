using System;
using System.Collections;

using UnityEngine;

public class GGJ_Spawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float SpawnTime = 2.5f;
    public int MaxSpawnCount;
    public float HealthMultiplierIncrease = 1.0f;
    public float DamageMultiplerIncrease = 0.25f;

    private GGJ_SwarmController _swarmController;
    private float _timeElapsedSinceLastSpawn;
    private BoxCollider _boxCollider;

    private float _damageMultipler;
    private float _healthMultipler;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        // Set some defaults
        _timeElapsedSinceLastSpawn = 0.0f;
        _damageMultipler = 1.0f;
        _healthMultipler = 1.0f;

        // Instantiate the swarm controller
        _swarmController = gameObject.AddComponent<GGJ_SwarmController>();

        // Retrieve the box collider for calculations later
        _boxCollider = gameObject.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        // Check whether we should spawn an enemy
        if (_timeElapsedSinceLastSpawn > SpawnTime)
        {
            // Check whether we should still be running after this
            var spawnCount = _swarmController.Enemies.Count;
            if (spawnCount < MaxSpawnCount)
            {
                // Spawn a new enemy
                SpawnEnemy();
                Debug.Log(string.Format("Spawned enemy {0} of {1}.", spawnCount + 1, MaxSpawnCount));

                // Reset spawn timer
                _timeElapsedSinceLastSpawn = 0.0f;
            }
            else
            {
                // Do not spawn any more
                Debug.Log(string.Format("Max spawn count reached ({0}/{1}), holding off until something dies.", spawnCount, MaxSpawnCount));
            }
        }
        else
        {
            // Increment the time
            _timeElapsedSinceLastSpawn += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Do nothing now... we don't care for where the player is
    }

    public void WaveIncrease(int waveNumber)
    {
        // Increase the max number by wave number
        MaxSpawnCount += waveNumber;

        // Spawn up to the max number of enemies
        var maxCount = MaxSpawnCount - _swarmController.Enemies.Count;
        for (int i = 0; i < maxCount; i++)
        {
            SpawnEnemy();
        }

        // Check if special wave, immediately spawn lots of enemies
        if (waveNumber % 5 == 0)
        {
            // Immediately spawn the max enemies AGAIN
            for (int i = 0; i < maxCount; i++)
            {
                SpawnEnemy();
            }
        }
        // Check if super special wave, time to make enemies tougher
        if (waveNumber % 3 == 0)
        {
            _healthMultipler += HealthMultiplierIncrease;
        }
        // Check if even wave, time to make enemies harder
        if (waveNumber % 2 == 0)
        {
            _damageMultipler += DamageMultiplerIncrease;
        }
    }

    private void SpawnEnemy()
    {
        // Calculate enemy spawn local position
        var localEnemyPosition = UnityEngine.Random.onUnitSphere;
        var size = _boxCollider.size * 0.5f;
        localEnemyPosition.x *= UnityEngine.Random.Range(-size.x, size.x);
        localEnemyPosition.y = gameObject.transform.localPosition.y;
        localEnemyPosition.z *= UnityEngine.Random.Range(-size.z, size.z);

        // Spawn new enemy
        var enemyGameObject = Instantiate(EnemyPrefab, gameObject.transform.localPosition + localEnemyPosition, Quaternion.identity) as GameObject;

        // Add a new controller
        var enemyController = enemyGameObject.GetComponent<GGJ_EnemyController>();
        _swarmController.Enemies.Add(enemyController);

        // Modify the enemies stats according to the multipliers
        var enemyStats = enemyController.GetComponent<Stats>();
        enemyStats.Attack = (int)Math.Round(enemyStats.Attack * _damageMultipler);
        enemyStats.HealthMax = (int)Math.Round(enemyStats.HealthMax * _healthMultipler);
        enemyStats._healthCurrent = enemyStats.HealthMax;
    }
}
