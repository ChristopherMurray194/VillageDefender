using UnityEngine;
using System.Collections;

public class BasePickup : MonoBehaviour
{
    protected int spawnIndex = 5; // The index of the spawn this pickup is currently position at
    public int SpawnIndex
    {
        get
        {
            return spawnIndex;
        }
        set
        {
            spawnIndex = value;
        }
    }

    protected virtual void Update()
    {
        // Rotate the pickup 90 degrees in the Y axis
        transform.Rotate(new Vector3(0f, 90f, 0f) * Time.deltaTime, Space.World);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        GameObject healthPickupMgr = GameObject.Find("HealthPickupManager");
        PickupManager pMgr = healthPickupMgr.GetComponent<PickupManager>();
        // Spawn a new pickup
        pMgr.SpawnNew(spawnIndex);
        // Decrement number of pickups in the scene
        pMgr.PickupCount = pMgr.PickupCount - 1;
        
        // Remove the current pickup from the scene
        Destroy(gameObject);
    }
}
