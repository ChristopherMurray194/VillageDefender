using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 25;
    public float throwRange = 10f;

    Animator anim;
    ArrayList enemies;  // The list of enemies in range of the player
    bool bEnemyInRange;

    void Awake()
    {
        enemies = new ArrayList();
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // Check the enemy isn't already a member of the array list.
            // Was having an issue where object would be added twice.
            if(!enemies.Contains(other.gameObject))
                // Add the enemy to the list of enemies
                enemies.Add(other.gameObject);
            
            bEnemyInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // Remove the enemy from the enemies list
            enemies.Remove(other.gameObject);
            bEnemyInRange = false;
        }
    }

    void Update()
    {
        // If left mouse button pressed
        if (Input.GetButton("Fire1"))
        {
            Attack();
        }
        SetAttackOnMovement();
        
    }

    void Attack()
    {
        // Play the attack animation
        anim.SetBool("Attack", true);
    }

    public void AttackFinished()
    {
        // Only apply damage to the enemy if the enemy is in range of the attack
        if (bEnemyInRange)
        {
            //enemyHealth.TakeDamage(attackDamage);
        }
        // Stop the attack animation from looping
        anim.SetBool("Attack", false);
    }

    void SetAttackOnMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // If the player is moving
        if (h != 0f || v != 0f)
            // Stop the attack animation
            anim.SetBool("Attack", false);
    }
}
