using UnityEngine;
using System.Collections;

public class Sewer : MonoBehaviour
{
    // Damage dealt to the player
    public int playerDamage = 5;
    // Damage dealt to the enemies
    public int enemyDamage = 10;
    // Damage dealt increment (in seconds)
    public float damageTime = 3f;

    float timer;

    void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime;

        // Apply damage in value of 'damageTime' increments
        if (timer >= damageTime)
        {
            // Is the object the player?
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(playerDamage);
                timer = 0;
            }
            // Is the object the enemy?
            if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(enemyDamage);
                timer = 0;
            }
        }
    }
}
