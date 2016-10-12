using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public int enemyCount = 5;          // Number of enemies to be spawned in this scene
    public PlayerHealth playerHealth;
    public GameObject[] enemies;
    public float baseSpawnTime = 3f;    // Spawn time for the base enemy type
    public float runnerSpawnTime = 10f;  // Spawn time for the runner enemy type
    public Transform[] spawnPoints;     // Array of spawn points

    int spawnCount; // Counts the number of enemies spawned

    void Start()
    {
        InvokeRepeating("SpawnBaseEnemy", baseSpawnTime, baseSpawnTime);
        
        InvokeRepeating("SpawnRunner", runnerSpawnTime, runnerSpawnTime);
    }

    void SpawnBaseEnemy()
    {
        // If the enemyCount has not been reached
        if (spawnCount != enemyCount)
        {
            // If the player is dead, there is no need to continue spawning
            if (playerHealth.GetCurrentHealth() <= 0f)
            {
                return;
            }

            // Choose a random spawn point to spawn from
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(enemies[0], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            ++spawnCount;
        }
    }

    void SpawnRunner()
    {
        // If the enemyCount has not been reached
        if (spawnCount != enemyCount)
        {
            // If the player is dead, there is no need to continue spawning
            if (playerHealth.GetCurrentHealth() <= 0f)
            {
                return;
            }

            // Choose a random spawn point to spawn from
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            if (enemies.Length > 0)
            {
                // I am assuming the runner enemy will be the second element of the array
                Instantiate(enemies[1], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                ++spawnCount;
            }
            else return;
        }
    }

    int GetEnemyCount() { return enemies.Length; }
}
