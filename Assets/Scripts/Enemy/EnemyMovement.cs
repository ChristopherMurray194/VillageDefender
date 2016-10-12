using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 1f;
    
    protected Transform player;         // Position of the player
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    protected NavMeshAgent nav;         // Reference to the NavMeshAgent
    
    protected virtual void Awake()
    {
        // Find the player's position
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = movementSpeed;
    }

    protected virtual void Update()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.GetCurrentHealth() > 0)
        {
            if(nav.enabled)
                // Move to the player's position
                nav.SetDestination(player.position);
        }
        else
            nav.enabled = false;
    }
}
