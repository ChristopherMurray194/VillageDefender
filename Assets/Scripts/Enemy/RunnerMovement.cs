using UnityEngine;
using System.Collections;

public class RunnerMovement : EnemyMovement
{
    public float runSpeed = 4f;
    /// <summary> The distance from the player the runner will  begin running. </summary>
    public float runDistance = 8f;

    Animator anim;
    /// <summary> The value to assign to the RunnerAC parameter with the same identifier. </summary>
    bool bCastRay = true;
    const float THETA = 90f;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (bCastRay)
        {
            Vector3 fwd = transform.forward;
            // Left feeler
            Vector3 redFeeler = new Vector3((fwd.x * Mathf.Cos(THETA)) + (fwd.z * Mathf.Sin(THETA)),
                                    transform.forward.y,
                                    (fwd.x * -Mathf.Sin(THETA)) + (fwd.z * Mathf.Cos(THETA)));
            // Create three feelers
            CreateFeeler(redFeeler);
            Debug.DrawRay(transform.position + new Vector3(0f, 1f, 0f), redFeeler * runDistance, Color.red, .1f);
            Debug.Log(Vector3.Dot(transform.forward, redFeeler) * (180/Mathf.PI));

            CreateFeeler(transform.forward);
            Debug.DrawRay(transform.position + new Vector3(0f, 1f, 0f), transform.forward * runDistance, Color.blue, .1f);

            // Right feeler
            Vector3 greenFeeler = new Vector3((fwd.x * Mathf.Cos(-THETA)) + (fwd.z * Mathf.Sin(-THETA)),
                                    transform.forward.y,
                                    (fwd.x * -Mathf.Sin(-THETA)) + (fwd.z * Mathf.Cos(-THETA)));
            CreateFeeler(greenFeeler);
            Debug.DrawRay(transform.position + new Vector3(0f, 1f, 0f), greenFeeler * runDistance, Color.green, .1f);
        }
    }

    /// <summary>
    /// Creates a 'feeler' raycast from the runner enemy to detect when the player is close
    /// enough to run at them.Cannot use a sphere collider as there is a sphere collider component
    /// acting as a trigger placed on the enemy object.
    /// </summary>
    /// <param name="rayDirection"> The direction for the raycast. </param>
    void CreateFeeler(Vector3 rayDirection)
    {
        //The ray cast from the enemy to detect when the player is close enough to run.
        Ray feeler = new Ray(transform.position, Vector3.ClampMagnitude(rayDirection, runDistance));
        RaycastHit objectHit;

        //Debug.DrawRay(feeler.origin + new Vector3(0f, 1f, 0f), feeler.direction * runDistance, Color.red, .1f);

        // If the ray collides with the player
        if(Physics.Raycast(feeler, out objectHit, runDistance, LayerMask.GetMask("Player")))
        {
                // Start running animation
                anim.SetBool("bRun", true);
                // Change movement speed
                nav.speed = runSpeed;
                // No longer need to cast the ray(s)
                bCastRay = false;
        }
    }

    /// <summary>
    /// Function called at the beginning of the SKILL animation playback.
    /// </summary>
    void StopMovement()
    {
        nav.enabled = false;   
    }

    /// <summary>
    /// Function called at the end of the SKILL animation playback.
    /// </summary>
    void RestartMovement()
    {
        nav.enabled = true;
    }
}
