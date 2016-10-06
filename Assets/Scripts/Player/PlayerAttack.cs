﻿using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 25;
    public float throwRange = 10f;

    Animator anim;
    Ray fwdRay;
    RaycastHit objectHit;
    int damageableMask;
    EnemyHealth enemyHealth;
    bool bEnemyInRange;

    void Awake()
    {
        damageableMask = LayerMask.GetMask("Damageable");
        anim = GetComponent<Animator>();
    }

    // Called almost every frame
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // Ensures that if a enemy is within the collider, bEnemyRange remains true
            bEnemyInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            bEnemyInRange = false;
        }
    }

    void Update()
    {
        // If left mouse button pressed
        if (Input.GetButton("Fire1")) anim.SetBool("Attack", true);
        // If right mouse button pressed
        if (Input.GetButton("Fire2")) anim.SetBool("Block", true);
        if (Input.GetButtonUp("Fire2")) anim.SetBool("Block", false);

        SetAttackOnMovement();
        GetFacing();
    }

    /**
     * Cast a ray from the player's forward direction and get the health script of the enemy
     * the player is facing
     */
    void GetFacing()
    {
        fwdRay.origin = transform.position + new Vector3(0f,.5f, 0f);     // The ray begins at the player's position
        fwdRay.direction = transform.forward;   // Ray is in the direction the player is facing

        // The length of the ray is the same as the radius of the player's spherecollider + its z offset.
        // Unnecessary to have it any longer as we do not care about objects outside of the player's range.
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        float rayRange = sphereCollider.radius + 0.5f; // Push the ray slightly out of the radius range to detect the enemy a little earlier.
        if (Physics.Raycast(fwdRay, out objectHit, rayRange, damageableMask))
        {
            enemyHealth = objectHit.collider.GetComponent<EnemyHealth>();
        }
    }

    public void AttackFinished()
    {
        // Only apply damage to the enemy if the enemy is in range of the attack
        if (bEnemyInRange)
        {
            if (enemyHealth != null) enemyHealth.TakeDamage(attackDamage);
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
