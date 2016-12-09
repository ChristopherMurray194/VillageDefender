using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    /// <summary> Amount of damage the player deals to enemies </summary>
    public int attackDamage = 25;

    Animator anim;
    RaycastHit objectHit;
    int damageableMask;
    EnemyHealth enemyHealth;
    /// <summary> Value to be assigned to the PlayerAC parameter with the same identifier </summary>
    bool bEnemyInRange;
    /// <summary> The transform representing the origin of the ray used to detect the enemy the player is facing </summary>
    Transform swordTransform;

    void Awake()
    {
        damageableMask = LayerMask.GetMask("Damageable");
        anim = GetComponent<Animator>();

        Transform footman = transform.Find("footman");
        Transform hips = footman.Find("Hips");
        swordTransform = hips.Find("Sword");
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
        GetFacing(transform.forward);
        // Also create a ray diagonally to the left, in case the enemy is to the left of the sword arm.
        // In which case they won't be detected by the ray.
        GetFacing(transform.forward + -transform.right);
    }

    /// <summary>
    /// Cast a ray from the sword's transform forward direction and get the health script of the enemy
    /// the player is facing.
    /// <param name="direction"> Direction of the created ray </param>
    /// </summary>
    void GetFacing(Vector3 direction)
    {
        Ray fwdRay = new Ray(swordTransform.position,   // The ray begins at the  sword transform's position
                            direction);                 // Ray is in the direction the player is facing

        // The length of the ray is the same as the radius of the player's spherecollider + its z offset.
        // Unnecessary to have it any longer as we do not care about objects outside of the player's range.
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        float rayRange = sphereCollider.radius + 0.5f; // Push the ray slightly out of the radius range to detect the enemy a little earlier.
        if (Physics.Raycast(fwdRay, out objectHit, rayRange, damageableMask))
        {
            enemyHealth = objectHit.collider.GetComponent<EnemyHealth>();
        }
    }

    /// <summary>
    /// Invoked when the player's attack animation event (with the same identifier) is reached in the animation playback.
    /// </summary>
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

    /// <summary>
    /// Cancels the attack animation playback if the player begins moving.
    /// </summary>
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
