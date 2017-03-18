using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    /// <summary> The speed the enemy object sinks when killed </summary>
    public float sinkSpeed = 1f;
    /// <summary> The score value this enemy type is worth </summary>
    public int scoreValue = 10;
    public AudioClip deathClip;

    Animator anim;
    AudioSource enemyAudio;
    HealthBar healthBar;
    ParticleSystem bloodParticles;
    Blood bloodScript;
    CapsuleCollider capsuleCollider;
    /// <summary> Value to assign to the EnemyAC parameter with the same identifier </summary>
    bool isDead;
    /// <summary> Value to assign to the EnemyAC parameter with the same identifier </summary>
    bool isSinking;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        healthBar = GetComponentInChildren<HealthBar>();
        bloodParticles = GetComponentInChildren<ParticleSystem>();
        bloodScript = bloodParticles.GetComponent<Blood>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    void Update()
    {
        if (isSinking)
            // Translate the enemy down
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Deals damage to the enemy.
    /// </summary>
    /// <param name="amount"> The amound of damage to deal. </param>
    public void TakeDamage(int amount)
    {
        if (isDead)
            return;

        enemyAudio.Play();
        currentHealth -= amount;
        // Change the health bar accordingly
        healthBar.UpdateHealth(amount, startingHealth);

        // Play the damage taken animation
        anim.SetBool("bDamageTaken", true);

        // Play the blood particle spurt effect
        bloodScript.Spurt();
        bloodParticles.Play();

        if (currentHealth <= 0)
            Death();
    }

    /// <summary>
    /// Handles what should happen when the enemy has no health remianing.
    /// </summary>
    void Death()
    {
        isDead = true;

        // Triggers are not physically collidable, so player can move through dead enemies
        capsuleCollider.isTrigger = true;

        anim.SetTrigger("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        // Play the blood particle spray effect
        bloodScript.Spray();
        bloodParticles.Play();
    }

    /// <summary>
    /// Start the sinking process of the enemy object.
    /// </summary>
    public void StartSinking()
    {
        // Disable the nav agent so that the enemy stops path finding
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        // Unity NavMesh ignores kinematic objects when updating
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        // Decrement the count of enemies in the level
        --EnemyCountManager.enemyCount;
        // Destroy the enemy gameobject
        Destroy(gameObject, 2f);
    }

    /// <summary>
    /// Resets the bDamageTaken EnemyAC parameter to false. So that it can be played again.
    /// </summary>
    public void ResetDamageTaken()
    {
        // Reset the bDamageTaken EnemyAC parameter at the end of the damage animation
        anim.SetBool("bDamageTaken", false);
    }
}
