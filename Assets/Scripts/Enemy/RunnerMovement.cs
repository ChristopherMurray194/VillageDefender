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
            // Create three feelers
            CreateFeeler(transform.forward + transform.right);
            CreateFeeler(transform.forward);
            CreateFeeler(transform.forward + -transform.right);
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
