using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyCountManager : MonoBehaviour
{
    public static int enemyCount;
    public EnemyManager enemyManager;
    Text text;

    void Awake()
    {
        enemyCount = enemyManager.enemyCount;
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = "Enemies: " + enemyCount;
    }
}
