using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    Quaternion initRot;
    public float ScaleX;

	void Start ()
    {
        // Get the initial rotation of the health bar
        initRot = transform.rotation;
	}

    void Update()
    {
        // Lock the rotation in place
        // Not sure if there is a more efficient way of doing this ?
        // But currently this works
        transform.rotation = initRot;
    }

    public void UpdateHealth(int damageValue, int startingHealth)
    {
        // Percentage to decrement i.e. if damageValue is 10 and statingHealth is 100
        // percentage needs to be .1 (10%)
        float percentage = (float)damageValue / (float)startingHealth;
        // The amount to decrement the health bar by
        float decrementValue = ScaleX * percentage;
        // Change the scale of the health bar in the X axis only
        transform.localScale -= new Vector3(decrementValue, 0f, 0f);
    }
}
