﻿using UnityEngine;
using System.Collections;

public class SpeedPickup : BasePickup
{
    // The new movement speed
    public float newSpeed = 10f;
    // The duration time (in seconds) for the effect of the pickup
    public int effectDuration = 5;
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            // Only change the player's speed if it has not already been changed
            if (playerMovement.Speed != newSpeed)
            {
                // Assign the new speed to the player's movement speed and pass the effect duration
                playerMovement.SetSpeed(newSpeed, effectDuration);

                NotifyManager();

                base.OnTriggerEnter(other);
            }
        }
    }
}
