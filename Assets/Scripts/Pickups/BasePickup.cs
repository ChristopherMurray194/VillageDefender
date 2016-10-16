using UnityEngine;
using System.Collections;

public abstract class BasePickup : MonoBehaviour
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

    /*
     * Ensure all derived classes (pickup types) notifies their respective manager,
     * to free up their spawn point and decrement the number of pickups (of that type) in the scene.
     */
    protected abstract void NotifyManager();

    protected virtual void OnTriggerEnter(Collider other)
    {   
        // Remove the current pickup from the scene
        Destroy(gameObject);
    }
}
