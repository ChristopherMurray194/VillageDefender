using UnityEngine;
using System.Collections;

public class BasePickup : MonoBehaviour
{
    void Update()
    {
        // Rotate the pickup 90 degrees in the Y axis
        transform.Rotate(new Vector3(0f, 90f, 0f) * Time.deltaTime, Space.World);
    }
}
