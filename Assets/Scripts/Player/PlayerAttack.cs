using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 25;
    public float throwRange = 10f;

    Animator anim;
    GameObject enemy;
    EnemyHealth enemyHealth;
    bool bEnemyInRange;

    void Awake()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            bEnemyInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
            bEnemyInRange = false;
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
            enemyHealth.TakeDamage(attackDamage);
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
