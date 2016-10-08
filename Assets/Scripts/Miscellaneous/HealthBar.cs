using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    GameObject mainCamera;
    float scaleX; // This is the initial X value of the scale

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

    /*
     *  Function gets called by the enemy's TakeDamage function in the EnemyHealth script,
     *   when damage is taken.
     */
    public void UpdateHealth(int damageValue, int startingHealth)
    {
        // Percentage to decrement i.e. if damageValue is 10 and statingHealth is 100
        // percentage needs to be .1 (10%)
        float percentage = (float)damageValue / (float)startingHealth;
        // The amount to decrement the health bar by
        float decrementValue = scaleX * percentage;
        // Change the scale of the health bar in the X axis only
        transform.localScale -= new Vector3(decrementValue, 0f, 0f);
    }
}
