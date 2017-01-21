using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
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
                // TODO: Spawn enemy

                // Increment the spawned count
                _spawnedCount++;

                // Reset spawn timer
                _timeElapsedSinceLastSpawn = 0.0f;

                // Check whether we should still be running after this
                if (_spawnedCount >= MaxSpawnCount)
                {
                    // Destory this as we're done with it
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
