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
    float timer;
    Ray throwRay;
    RaycastHit throwHit;
    int damageableMask;
    GameObject throwPos;
    LineRenderer throwLine;

    void Awake()
    {
        damageableMask = LayerMask.GetMask("Damageable");
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        throwPos = GameObject.Find("ThrowPos");
        throwLine = throwPos.GetComponent<LineRenderer>();
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

        // If T key is pressed
        if (Input.GetKeyDown(KeyCode.T))
            AimThrowable();
        // If T key is released
        if(Input.GetKeyUp(KeyCode.T))
            throwLine.enabled = false;
    }

    void AimThrowable()
    {
        // Enable the throw line
        throwLine.enabled = true;
        Vector3 startPos = transform.position;
        startPos.y = 1f;
        throwLine.SetPosition(0, startPos);

        throwRay.origin = startPos;
        throwRay.direction = transform.forward;

        // If the ray hits something, get information about that object
        if(Physics.Raycast(throwRay, out throwHit, throwRange, damageableMask))
        {
            throwLine.SetPosition(1, throwHit.point);
        }
        else
        {
            throwLine.SetPosition(1, throwRay.origin + throwRay.direction * throwRange);
        }
    }

    void Attack()
    {
        // If player is not throwing a rock
        if (!throwLine.enabled)
        {
            anim.SetBool("Attack", true);
        }

        // If player is throwing a rock
        if (throwLine.enabled)
        {
            // ThrowRock() - disable throwLine in this function
        }
    }

    public void AttackFinished()
    {
        if (bEnemyInRange)
        {
            enemyHealth.TakeDamage(attackDamage, new Vector3(0f, 0.1f, 0f));
        }
        anim.SetBool("Attack", false);
    }

    void SetAttackOnMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0f || v != 0f)
            anim.SetBool("Attack", false);
    }
}
