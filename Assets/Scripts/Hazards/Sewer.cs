using UnityEngine;
using System.Collections;

public class Sewer : MonoBehaviour
{
    /// <summary> Damage dealt to the player </summary>
    public int playerDamage = 5;
    /// <summary> Damage dealt to the enemies </summary>
    public int enemyDamage = 10;
    /// <summary> Deal damage to the in range object every X seconds. </summary>
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
