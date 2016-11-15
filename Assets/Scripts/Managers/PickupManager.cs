using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour
{
    /// <summary> Array of pickup GameObjects </summary>
    public GameObject[] pickups;
    /// <summary> Maximum number of pickups in the scene </summary>
    public int pickupCap = 2;
    /// <summary> Time  (in secodns) between spawns </summary>
    public float pickupSpawnTime = 10f;
    /// <summary> Array of spawnpoint GameObject transforms </summary>
    public Transform[] spawnPoints;

    /// <summary> Keeps track of how many pickups are currently in the scene </summary>
    int pickupCount;
    /// <summary> List of spawns with a pickup spawned </summary>
    ArrayList inUseSpawns = new ArrayList();

    void Start()
    {
        // Spawn an initial pickup
        InvokeRepeating("SpawnPickup", pickupSpawnTime, pickupSpawnTime);
    }

    /// <summary>
    /// Called when a spawn point is freed, by a pickup at that spawn point being destroyed.
    /// </summary>
    /// <param name="index"> The index of the spawn point that has been assigned to the destroyed pickup </param>
    public void FreeSpawn(int index)
    {
        if (inUseSpawns.Contains(index))
            // Remove the index from the list, as it is now free for use
            inUseSpawns.Remove(index);
    }

    /// <summary>
    /// Instantiate a pickup GameObject randomly chosen from the pickups array.
    /// </summary>
    void SpawnPickup()
    {
        // Prevent stack overflow. If all the spawn points are in use...
        if (inUseSpawns.Count == spawnPoints.Length)
            return;

        if (pickupCount < pickupCap)
        {
            // Select a random spawn point
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            // Select a random pickup to spawn
            int pickupIndex = Random.Range(0, pickups.Length);

            // Check the spawn is not currently in use
            if (!inUseSpawns.Contains(spawnPointIndex))
            {
                // Instantiate a pickup and return a copy of it
                GameObject pickupInstance = Instantiate(pickups[pickupIndex], spawnPoints[spawnPointIndex].position, pickups[pickupIndex].transform.rotation) as GameObject;
                // Add the index to the in-use list
                inUseSpawns.Add(spawnPointIndex);
                // Give the index to the pickup object
                pickupInstance.GetComponent<BasePickup>().SpawnIndex = spawnPointIndex;
                ++pickupCount;
            }
            else SpawnPickup();
        }
    }

    public int PickupCount
    {
        get
        {
            return pickupCount;
        }
        set
        {
            pickupCount = value;
        }
    }
}