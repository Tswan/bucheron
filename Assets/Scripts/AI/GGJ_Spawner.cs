using UnityEngine;
using System.Collections;

public class GGJ_Spawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float MaxSpawnDistance = 100.0f;
    public float SpawnTime = 2.5f;

    private GGJ_SwarmController _swarmController;
    private GGJ_PlayerController _collidedPlayer;
    private float _timeElapsedSinceLastSpawn;
    private int _spawnedCount;

    public int MaxSpawnCount;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        // Set some defaults
        _collidedPlayer = null;
        _timeElapsedSinceLastSpawn = 0.0f;
        _spawnedCount = 0;

        // Instantiate the swarm controller
        _swarmController = gameObject.AddComponent<GGJ_SwarmController>();
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
                var enemyGameObject = Instantiate(EnemyPrefab, gameObject.transform.localPosition + localEnemyPosition, Quaternion.identity) as GameObject;
                Debug.Log(string.Format("Spawned enemy {0} of {1}.", _spawnedCount + 1, MaxSpawnCount));

                // Add a new controller
                var enemyController = enemyGameObject.AddComponent<GGJ_EnemyController>();
                enemyController.MaxViewDistance = float.MaxValue;
                _swarmController.Enemies.Add(enemyController);

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

    private void OnTriggerEnter(Collider other)
    {
        _collidedPlayer = other.gameObject.GetComponent<GGJ_PlayerController>() ?? _collidedPlayer;
    }
}
