using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;

    void Awake()
    {
        GameObject HUD = GameObject.Find("HUDCanvas");
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = GetComponentInChildren<PlayerHealth>();
    }

    void Update()
    {
        GameOver();
    }

    void GameOver()
    {
        if (playerHealth.GetIsDead())
        {

        }
    }
}
