using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    /// <summary> Inital health value for the player </summary>
    public int startingHealth = 100;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    /// <summary> Time the damage taken image is flashed on the UI for. Will be multiplied by deltaTime </summary>
    public float flashSpeed = 5f;
    /// <summary> Colour of the damage taken UI image </summary>
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    /// <summary> Current health of the player </summary>
    int currentHealth;
    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;  // Reference to PlayerMovement script
    /// <summary> The value to be assigned to the playerAC parameter with the same identifier </summary>
    bool isDead;
    /// <summary> The value to be assigned to the playerAC parameter with the same identifier </summary>
    bool damaged;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHealth = startingHealth;
    }

    void Update()
    {
        // If taking damage
        if (damaged)
            // Flash the damage color
            damageImage.color = flashColour;
        else
            // Linearly interpolate back to clear
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

        damaged = false;
    }

    /// <summary>
    /// Deals damage to the player.
    /// </summary>
    /// <param name="amount"> Amount of damage to be dealt </param>
    public void TakeDamage(int amount)
    {
        // If the player is not blocking, they can take damage
        if (!anim.GetBool("Block"))
        {
            damaged = true;
            currentHealth -= amount;

            // Change the slider value accordingly
            healthSlider.value = currentHealth;
            // Play the hurt audio clip
            playerAudio.Play();

            // Check if the player is dead
            if (currentHealth <= 0 && !isDead)
                Death();
        }
    }

    /// <summary>
    /// Restores health to the player.
    /// </summary>
    /// <param name="amount"> Amount of health to restore </param>
    public void RestoreHealth(int amount)
    {
        // Ensure when health is restored it is not greater than,
        // starting health.
        if (currentHealth + amount <= startingHealth)
            currentHealth += amount;
        else // If it is, just give full health
            currentHealth = startingHealth;

        // Change the slider value accordingly
        healthSlider.value = currentHealth;

        // TODO: Add some particle effect to make it clearer to the player
        //       that some health has been restored

        // TODO: Either play an audio clip here or just add it as a component to the
        //       above particle effect. Latter would be better.
    }

    /// <summary>
    /// Handles what should happen when the player has no health remaining.
    /// </summary>
    void Death()
    {
        isDead = true;

        //playerAttacking.DisableEffects();
        anim.SetTrigger("Die");
        // Change the audio clip
        playerAudio.clip = deathClip;
        playerAudio.Play();

        // Stop the player from moving
        playerMovement.enabled = false;
        //playerAttacking.enabled = false;
    }

    public bool IsDead { get { return isDead; } }
    public int StartingHealth { get { return startingHealth; } }
    public int CurrentHealth { get { return currentHealth; } }
}
