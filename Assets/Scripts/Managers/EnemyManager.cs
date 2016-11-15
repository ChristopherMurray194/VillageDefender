using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    /// <summary> Number of enemies to be spawned in the scene </summary>
    public int enemyCount = 5;
    public PlayerHealth playerHealth;
    /// <summary> Array of enemy GameObjects that can be spawned in this scene </summary>
    public GameObject[] enemies;
    /// <summary> Spawn time for the base enemy type </summary>
    public float baseSpawnTime = 3f;
    /// <summary>Spawn time for the runner enemy type </summary>
    public float runnerSpawnTime = 10f;
    /// <summary> Array of spawn points </summary>
    public Transform[] spawnPoints;

    /// <summary> Counts the number of enemies spawned </summary>
    int spawnCount;

    void Start()
    {
        InvokeRepeating("SpawnBaseEnemy", baseSpawnTime, baseSpawnTime);
        
        InvokeRepeating("SpawnRunner", runnerSpawnTime, runnerSpawnTime);
    }

    /// <summary>
    /// Initialises a Skeleton GameObject, the BaseEnemy type.
    /// </summary>
    void SpawnBaseEnemy()
    {
        // If the enemyCount has not been reached
        if (spawnCount != enemyCount)
        {
            // If the player is dead, there is no need to continue spawning
            if (playerHealth.CurrentHealth <= 0f)
            {
                return;
            }

            // Choose a random spawn point to spawn from
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(enemies[0], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            ++spawnCount;
        }
    }

    /// <summary>
    /// Initialises a SkeletonRunner GameObject, the runner enemy type.
    /// </summary>
    void SpawnRunner()
    {
        // If the enemyCount has not been reached
        if (spawnCount != enemyCount)
        {
            // If the player is dead, there is no need to continue spawning
            if (playerHealth.CurrentHealth <= 0f)
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

    public int SpawnCount { get { return spawnCount;  } }
}
