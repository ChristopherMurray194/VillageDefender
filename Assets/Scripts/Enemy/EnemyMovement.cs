using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 3f;  // Movement speed of skeleton
    Transform player;   // Position of the player
    //PlayerHealth playerHealth;
    //EnemyHealth enemyHealth;
    NavMeshAgent nav;   // Reference to the NavMeshAgent

    void Awake()
    {
        // Find the player's position
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
        // Set the initial movement speed
        nav.speed = movementSpeed;
    }

    void Update()
    {
        // if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        //{
            // Move to the player's position
            nav.SetDestination(player.position);
        //}
        //else
        //    nav.enabled = false;
    }
}
