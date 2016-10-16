using UnityEngine;
using System.Collections;

public class HealthPickup : BasePickup
{
    // Amount to restore player's health by
    public int restoreAmount = 10;

    protected override void Update()
    {
        base.Update();
    }

    protected override void NotifyManager()
    {
        GameObject healthPickupMgr = GameObject.Find("HealthPickupManager");
        PickupManager pMgr = healthPickupMgr.GetComponent<PickupManager>();
        // Free up the spawn point this pickup instance was located at,
        // so that a new health pickup can be spawned there.
        pMgr.FreeSpawn(spawnIndex);
        // Decrement number of pickups in the scene
        pMgr.PickupCount = pMgr.PickupCount - 1;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            // Only restore health if damage has been taken
            if (playerHealth.CurrentHealth < playerHealth.StartingHealth)
            {
                playerHealth.RestoreHealth(restoreAmount);

                NotifyManager();

                base.OnTriggerEnter(other);
            }
        }
    }
}
