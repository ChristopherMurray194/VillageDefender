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

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            // Only restore health if damage has been taken
            if (playerHealth.CurrentHealth < playerHealth.StartingHealth)
            {
                playerHealth.RestoreHealth(restoreAmount);

                base.OnTriggerEnter(other);
            }
        }
    }
}
