﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;  // Reference to PlayerMovement script
    //PlayerAttaching playerAttacking;
    bool isDead;
    bool damaged;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        //playerAttacking = GetComponentInChildren<PlayerAttacking>(); // THIS WILL LIKELY NEED TO BE CHANGED AS PLAYER CAN HANDLE WHEN ATTACKING NOT THE SWORD
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

    public void TakeDamage(int amount)
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
}
