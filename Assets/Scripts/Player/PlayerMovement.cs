using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    /// <summary> initialSpeed of movement </summary>
    public float initialSpeed = 6f;
    /// <summary> Speed of Slerp to be applied to the player object's rotation </summary>
    public float rotSpeed = 4f;

    /// <summary> Timer variable for any alterations to speed </summary>
    float startTime;

    /// <summary> Player's current movement speed </summary>
    float speed;
    public float Speed
    {
        get { return speed; }
    }

    /// <summary> The duration any alteration to the player's movmement will last </summary>
    int effectDuration = 0;
    /// <summary> Store the movement direction </summary>
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidBody;
    int floorMask;
    /// <summary> Length of the ray cast from the camera </summary>
    float camRayLength = 100f;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor"); // Get the mask from the Floor layer
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // If the speed has changed
        if (speed != initialSpeed)
            // After the effect duration has elapsed
            if ((Time.realtimeSinceStartup - startTime) > effectDuration)
                ResetMovementSpeed();
    }

    void FixedUpdate()
    {
        // Get Inputs
        // Raw axis returns the value of the virtual axis identified by axisName
        // The value will be in the range -1...1. For keyboard input this will either be -1, 0, 1
        float h = Input.GetAxisRaw("Horizontal");   // Maps to A and D keys
        float v = Input.GetAxisRaw("Vertical");     // Maps to the W and S keys

        Move(h, v);
        Turning(h, v);
        Animating(h, v);
    }

    /// <summary>
    /// Handles the player's movement.
    /// </summary>
    /// <param name="h"> Horizontal virtual axis value. In the range of -1...1 </param>
    /// <param name="v"> Vertical virtual axis value. In the range of -1...1 </param>
    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        // Apply movement vector to the player
        playerRigidBody.MovePosition(transform.position + movement);
    }

    /// <summary>
    /// Turn the player. The input which drives the turning is determined by whether or not the player is currently moving.
    /// If they are not then the mouse position determines the direction the player rotates towards. Otherwise KeyEvents do.
    /// </summary>
    /// <param name="h"> Horizontal virtual axis value. In the range of -1...1 </param>
    /// <param name="v"> Vertical virtual axis value. In the range of -1...1 </param>
    void Turning(float h, float v)
    {
        // If there is no horizontal OR vertical axis input - i.e. not moving
        if (h == 0f && v == 0f)
        {
            // Rotation determined by the mouse position
            // Cast a ray from the main camera to the mouse position
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;
            // If the raycast hits something...(also get information about what was hit)
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                // Vector from player's position to the position of the mouse
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0f;
                Vector3 slerpDirection = Vector3.Slerp(transform.forward, playerToMouse, ((int)rotSpeed << 1) * Time.deltaTime);
                
                Quaternion newRotation = Quaternion.LookRotation(slerpDirection);
                playerRigidBody.MoveRotation(newRotation);
            }
        }
        else
        {
            // The rotation in the direction determined by key events, apply spherical interpolation
            Vector3 slerpDirection = Vector3.Slerp(transform.forward, movement, rotSpeed * Time.deltaTime);
            Quaternion newRotation = Quaternion.LookRotation(slerpDirection);
            playerRigidBody.MoveRotation(newRotation);
        }
    }

    /// <summary>
    /// Handles the animation to be played determined by how the player is moving.
    /// </summary>
    /// <param name="h"> Horizontal virtual axis value. In the range of -1...1 </param>
    /// <param name="v"> Vertical virtual axis value. In the range of -1...1 </param>
    void Animating(float h, float v)
    {
        // running is true if there is input - h OR v are NOT 0
        bool running = h != 0f || v != 0f;
        // Set the 'IsRunning' boolean in the animator
        anim.SetBool("IsRunning", running);
    }

    /// <summary> 
    /// Mutator function for speed 
    /// </summary>
    ///<param name="newSpeed"> the new player movement speed </param>
    ///<param name="duration"> the duration the new speed effect should last. Default is 0 </param>
    public void SetSpeed(float newSpeed, int duration = 0)
    {
        speed = newSpeed;

        // If a new duration value has been passed
        if (duration != 0)
        {
            // Set the duration the new speed lasts
            effectDuration = duration;
            // Start the effect timer by getting the current time
            startTime = Time.realtimeSinceStartup;
        }
    }

     /// <summary>
     /// If the movment speed gets altered, this function is called
     /// to reset it back to its initial value.
     /// </summary>
    void ResetMovementSpeed()
    {
        // Reset necessary variables
        speed = initialSpeed;
        startTime = 0f;
        effectDuration = 0;
    }
}
