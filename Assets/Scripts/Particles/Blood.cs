using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour
{
    // Determines whether to spurt or spray blood, default is to spurt
    bool bSpurt = true, bSpray;
    ParticleSystem bloodParticles;

    void  Awake()
    {
        bloodParticles = GetComponent<ParticleSystem>();
        bloodParticles.startSize = 0.3f;
        bloodParticles.startSpeed = 4f;

        // Ensure default is spurt
        Spurt();
    }

    void Update()
    {
        if (bSpray)
            Spray();
        if (bSpurt)
            Spurt();
    }
    
    void Spurt()
    {
        bSpray = false;
        bloodParticles.loop = false;
    }

    void Spray()
    {
        bSpurt = false;
        bloodParticles.loop = true;
    }

    public void SetSpurt(bool b)
    {
        bSpurt = b;
    }

    public void SetSpray(bool b)
    {
        bSpray = b;
    }
}
