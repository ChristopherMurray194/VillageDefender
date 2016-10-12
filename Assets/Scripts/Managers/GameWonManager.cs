using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameWonManager : MonoBehaviour
{
    public float restartDelay = 5f;

    Animator anim;
    float restartTimer;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (EnemyCountManager.enemyCount <= 0)
        {
            anim.SetTrigger("GameWon");

            restartTimer += Time.deltaTime;

            if(restartTimer >= restartDelay)
                SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
