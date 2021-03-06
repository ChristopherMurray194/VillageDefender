﻿using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 1f;
    
    /// <summary> The transform of the player object </summary>
    protected Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    protected UnityEngine.AI.NavMeshAgent nav;         // Reference to the NavMeshAgent
    
    protected virtual void Awake()
    {
        // Find the player's position
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.speed = movementSpeed;
    }

    protected virtual void Update()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.CurrentHealth > 0)
        {
            if(nav.enabled)
                // Move to the player's position
                nav.SetDestination(player.position);
        }
        else
            nav.enabled = false;
    }
}
