using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    /// <summary> Delay (in seconds) before the game restarts. </summary>
    public float restartDelay = 5f;

    Animator anim;
    float restartTimer;

    void Awake()
    {
        anim = GetComponent<Animator>();    
    }

    void Update()
    {
        if(playerHealth.IsDead)
        {
            anim.SetTrigger("GameOver");

            restartTimer += Time.deltaTime;

            if(restartTimer >= restartDelay)
                SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
