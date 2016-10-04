using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // Follow target (Player)
    public float smoothing = 5f;    // Smoothness of follow movement

    Vector3 offset;                 // Distance from player

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;    // Target position for camera to move - player's position plus vector between the two
        // Lerp (smoothly interpolate) from current position to target position
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
