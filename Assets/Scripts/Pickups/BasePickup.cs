using UnityEngine;
using System.Collections;

public abstract class BasePickup : MonoBehaviour
{
    /// <summary> 
    /// The index of the spawn this pickup is currently positioned at.
    /// </summary>
    // Default of -1 in the event that the variable is not assigned a new value for whatever reason, 
    // the pickup manager can't remove a spawn index from the inUse array incorrectly.
    protected int spawnIndex = -1;
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

    void Awake()
    {
        // Rotate all pickups so that they are upright,
        // because all mushroom meshes are instantiated on their side.
        // If a different mesh is used for a future pickup type, altering the
        // rotation may be unecessary.
        transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
    }

    protected virtual void Update()
    {
        // Rotate the pickup 90 degrees in the Y axis
        transform.Rotate(new Vector3(0f, 90f, 0f) * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// Notify the manager to free up the spawn point this pickup has been spawned at 
    /// and decrement the number of pickups in the scene.
    /// </summary>
    protected void NotifyManager()
    {
        GameObject pickupMgr = GameObject.Find("PickupManager");
        PickupManager pMgr = pickupMgr.GetComponent<PickupManager>();
        // Free up the spawn point this pickup instance was located at,
        // so that a new health pickup can be spawned there.
        pMgr.FreeSpawn(spawnIndex);
        // Decrement number of pickups in the scene
        pMgr.PickupCount = pMgr.PickupCount - 1;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {   
        // Remove the current pickup from the scene
        Destroy(gameObject);
    }
}
