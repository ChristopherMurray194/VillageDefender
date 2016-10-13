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

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            // Only restore health if damage has been taken
            if (playerHealth.GetCurrentHealth() < playerHealth.GetStartingHealth())
            {
                playerHealth.RestoreHealth(restoreAmount);
                
                GameObject healthPickupMgr = GameObject.Find("HealthPickupManager");
                PickupManager pMgr = healthPickupMgr.GetComponent<PickupManager>();
                // Spawn a new health pickup
                pMgr.SpawnNew();
                // Decrement number of pickups in the scene
                pMgr.setPickupCount(pMgr.GetPickupCount()-1);

                // Remove the current health pickup from the scene
                Destroy(gameObject);
            }
        }
    }
}
