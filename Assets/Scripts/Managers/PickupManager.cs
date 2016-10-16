using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour
{
    public GameObject pickup;           // Pickup object
    public int pickupCap = 1;           // Maximum number of pickups in the scene
    public float pickupSpawnTime = 10f; // Time between spawns
    public Transform[] spawnPoints;

    int pickupCount;                          // Keep track of how many pickups are currently in the scene
    ArrayList inUseSpawns = new ArrayList();  // List of spawns with a pickup spawned

    void Start()
    {
        // Spawn an initial pickup
        InvokeRepeating("SpawnPickup", pickupSpawnTime, pickupSpawnTime);
    }

    public void SpawnNew(int index)
    {
        if (inUseSpawns.Contains(index))
            // Remove the index from the list, as it is now free for use
            inUseSpawns.Remove(index);
    }

    void SpawnPickup()
    {
        // Prevent stack overflow. If all the spawn points are in use...
        if (inUseSpawns.Count == spawnPoints.Length)
            return;

        if (pickupCount < pickupCap)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            // Check the spawn is not currently in use
            if (!inUseSpawns.Contains(spawnPointIndex))
            {
                GameObject pickupInstance;
                pickupInstance = Instantiate(pickup, spawnPoints[spawnPointIndex].position, pickup.transform.rotation) as GameObject;
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