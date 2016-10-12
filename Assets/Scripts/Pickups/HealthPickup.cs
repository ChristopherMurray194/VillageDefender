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
                // Remove the health pickup from the scene
                GameObject.Destroy(gameObject);
            }
        }
    }
}
