using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;  // Reference to PlayerHealth script
    EnemyMovement enemyMovement;
    //EnemyHealth enemyHealth;
    bool bInRange;              // EnemyAC trigger
    float timer;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyMovement = GetComponent<EnemyMovement>();
        //enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Ensure collided object is the player
        if(other.gameObject == player)
            bInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            bInRange = false;
            // Reset the EnemyAC bool so it can transition back to walking
            anim.SetBool("bInRange", bInRange);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && bInRange/* && enemyHealth.currentHealth > 0*/)
        {
            Attack();
        }

        // Check is player is dead
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");  // Set the EnemyAC trigger
            // Player is dead so stop the enemy moving by disabling the script
            enemyMovement.enabled = false;
        }
    }

    void Attack()
    {
        // Reset the timer
        timer = 0f;

        // If the player stil has health
        if(playerHealth.currentHealth > 0)
        {
            // We are in range of the character so we know we can attack
            anim.SetBool("bInRange", bInRange);    // Allow transition to the attacking animation
        }
    }

    /**
     * This function will be called when the AttackFinished Event occurs on the Attack animation.
     */
    public void AttackFinished()
    {
        /* Apply damage ONLY when the enemy's sword hits the player.
        * If this is called in Attack then damage would be applied continously whilst in range of the player,
        * given that the enemy's attack is visually slow, there is a mismatch if the player is damaged multiple times
        * whilst the enemy attack animation is playing.
        */
        playerHealth.TakeDamage(attackDamage);
    }
}
