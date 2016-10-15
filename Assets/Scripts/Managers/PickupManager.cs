using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour
{
    public GameObject pickup;           // Pickup object
    public int pickupCap = 1;           // Maximum number of pickups in the scene
    public float pickupSpawnTime = 10f; // Time between spawns
    public Transform[] spawnPoints;

    int pickupCount;        // Keep track of how many pickups are currently in the scene
    ArrayList inUseSpawns;  // List of spawns with a pickup spawned

    void Start()
    {
        // Spawn an initial pickup
        Invoke("SpawnPickup", pickupSpawnTime);
    }

    public void SpawnNew()
    {
        Invoke("SpawnPickup", pickupSpawnTime);
    }

    void SpawnPickup()
    {
        if (pickupCount < pickupCap)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(pickup, spawnPoints[spawnPointIndex].position, pickup.transform.rotation);
            ++pickupCount;
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
            pickupCount = PickupCount;
        }
    }
}