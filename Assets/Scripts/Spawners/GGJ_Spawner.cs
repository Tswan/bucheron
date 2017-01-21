using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

    private GGJ_Player _collidedPlayer;
    private float _timeElapsedSinceLastSpawn;
    private int _spawnedCount;

    public int MaxSpawnCount { get; set; }

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
            if (Mathf.Sin(_timeElapsedSinceLastSpawn) > 0.0f)
            {
                // Spawn enemy
                Instantiate(EnemyPrefab, gameObject.transform.localPosition, Quaternion.identity);
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
