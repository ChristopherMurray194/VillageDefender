using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour
{
    /// <summary> If true. When damage is taken, spurt animation will play. </summary>
    bool bSpurt = true;
    /// <summary> If true. When damage is taken, spray animation will play. </summary>
    bool bSpray = false;

    ParticleSystem bloodParticles;

    void  Awake()
    {
        bloodParticles = GetComponent<ParticleSystem>();
        bloodParticles.startSize = 0.3f;
        bloodParticles.startSpeed = 4f;

        // Ensure default is to spurt blood
        Spurt();
    }
    
    /// <summary>
    /// Invoke to play the spurt animation when damage is taken.
    /// </summary>
    public void Spurt()
    {
        bSpurt = true;
        bSpray = false;
        bloodParticles.loop = false;
    }

    /// <summary>
    /// Invoke to play the spray animation when damage is taken.
    /// </summary>
    public void Spray()
    {
        bSpray = true;
        bSpurt = false;
        bloodParticles.loop = true;
    }
}
