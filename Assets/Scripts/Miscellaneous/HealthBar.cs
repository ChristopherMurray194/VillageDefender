using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    GameObject mainCamera;
    /// <summary> This is the initial X value of the scale compomnent </summary>
    float scaleX;

    void Awake()
    {
        mainCamera = GameObject.Find("Main Camera");
        // Store the initial X scale value
        scaleX = transform.localScale.x;
    }

    void Update()
    {
        Vector3 barToCamera = mainCamera.transform.position - transform.position;
        // Keep the bar rotation facing the camera
        transform.rotation = Quaternion.LookRotation(barToCamera) * Quaternion.Euler(90f, 0f, 0f);
    }

    /// <summary>
    /// Function gets called by the character's TakeDamage function in the character's health script---
    /// when damage is taken.
    /// </summary>
    /// <param name="damageValue"> Amount of damage to be dealt </param>
    /// <param name="startingHealth"> The value the health bar is at when full </param>
    public void UpdateHealth(int damageValue, int startingHealth)
    {
        // Percentage to decrement i.e. if damageValue is 10 and statingHealth is 100
        // percentage needs to be .1 (10%)
        float percentage = (float)damageValue / (float)startingHealth;
        // The amount to decrement the health bar by
        float decrementValue = scaleX * percentage;

        if (transform.localScale.x - decrementValue > 0)
            // Changes the scale of the health bar in the X axis only
            transform.localScale -= new Vector3(decrementValue, 0f, 0f);
        else
            transform.localScale = new Vector3(0f, 0f, 0f);
    }
}
