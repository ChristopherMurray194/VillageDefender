using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeedImageManager : MonoBehaviour {
    
    /// <summary> The time in seconds that the image is highlighted for </summary>
    int duration = 5;
    float startTime;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if(duration > 0)
            if((Time.realtimeSinceStartup - startTime) > duration)
                ResetImageAlpha();
    }

    /// <summary>
    /// Highlights the UI image associated with the Speed pickup. Gets passed
    /// the duration the effect lasts.
    /// </summary>
    /// <param name="duration"> The duration the speed pickup effect lasts (in seconds) </param>
    public void HighlightImage(int duration = 0)
    {
        this.duration = duration;
        // Start duration timer.
        startTime = Time.realtimeSinceStartup;
        // Highlight the speed pickup UI image
        anim.SetBool("bHighlight", true);
    }

    /// <summary>
    /// Resets the UI image to it's initial state.
    /// </summary>
    void ResetImageAlpha()
    {
        anim.SetBool("bHighlight", false);
        startTime = 0f;
    }
}
