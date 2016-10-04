﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;    // Speed of movement
    public float rotSpeed = 4f; // Speed of Slerp

    Vector3 movement;           // Store the movement direction
    Animator anim;              // Reference to animator component
    Rigidbody playerRigidBody;  // Reference to rigid body component
    int floorMask;              // Reference to the floor mask quad
    float camRayLength = 100f;  // Length of the ray cast from the camera

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor"); // Get the mask from the Floor layer
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get Inputs
        float h = Input.GetAxisRaw("Horizontal");   // Maps to A and D keys
        float v = Input.GetAxisRaw("Vertical");     // Maps to the W and S keys

        Move(h, v);
        Turning(h, v);
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        // Apply movement vector to the player
        playerRigidBody.MovePosition(transform.position + movement);
    }

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

                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                playerRigidBody.MoveRotation(newRotation);
            }
        }
        else
        {
            // The rotation in the direction determined by key events
            Vector3 slerpDirection = Vector3.Slerp(transform.forward, movement, rotSpeed * Time.deltaTime);
            Quaternion keyRotation = Quaternion.LookRotation(slerpDirection);
            playerRigidBody.MoveRotation(keyRotation);
        }
    }

    void Animating(float h, float v)
    {
        // running is true if there is input - h OR v are NOT 0
        bool running = h != 0f || v != 0f;
        // Set the 'IsRunning' boolean in the animator
        anim.SetBool("IsRunning", running);
    }
}
