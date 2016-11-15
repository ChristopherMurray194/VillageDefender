using UnityEngine;
using System.Collections;

public class RunnerMovement : EnemyMovement
{
    public float runSpeed = 4f;
    /// <summary> The distance from the player the runner will  begin running. </summary>
    public float runDistance = 8f;

    /// <summary> The ray cast from the enemy to detect when the player is close enough to run. </summary>
    Ray feeler;
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
            // TODO: These vector directions aren't quite right,
            //       to check uncomment Debug.DrawRay in the function.
            CreateFeeler(transform.forward + new Vector3(-.5f, 0f, 0f));
            CreateFeeler(transform.forward);
            CreateFeeler(transform.forward + new Vector3(.5f, 0f, 0f));
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
        float rayLength = runDistance;
        RaycastHit objectHit;
        feeler.origin = transform.position;
        feeler.direction = rayDirection;

        //Debug.DrawRay(feeler.origin + new Vector3(0f, 1f, 0f), feeler.direction * rayLength, Color.red, .1f);

        if(Physics.Raycast(feeler, out objectHit, rayLength, LayerMask.GetMask("Player")))
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
