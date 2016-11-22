using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeedPickup : BasePickup
{
    /// <summary> The new movement speed </summary>
    public float newSpeed = 10f;
    /// <summary> The duration time (in seconds) for the effect of the pickup </summary>
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

                GameObject SpeedPwrUpUI = GameObject.Find("PupSpeedUI");
                SpeedImageManager speedImgMgr = SpeedPwrUpUI.GetComponent<SpeedImageManager>();
                speedImgMgr.HighlightImage(effectDuration);

                NotifyManager();

                base.OnTriggerEnter(other);
            }
        }
    }
}
