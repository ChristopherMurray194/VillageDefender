using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    /// <summary> Target's position for the camera to follow </summary>
    public Transform target;
    /// <summary> Lerp smoothness from camera pos to target pos </summary>
    public float smoothing = 5f;
    /// <summary> Distance the camera is from the player </summary>
    Vector3 offset;

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
