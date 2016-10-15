using UnityEngine;
using System.Collections;

public class BasePickup : MonoBehaviour
{
    int spawnIndex; // The index of the spawn this pickup is currently position at

    protected virtual void Update()
    {
        // Rotate the pickup 90 degrees in the Y axis
        transform.Rotate(new Vector3(0f, 90f, 0f) * Time.deltaTime, Space.World);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        // Remove the current pickup from the scene
        Destroy(gameObject);
    }

    public int SpawnIndex
    {
        get
        {
            return spawnIndex;
        }
        set
        {
            spawnIndex = SpawnIndex;
        }
    }
}
