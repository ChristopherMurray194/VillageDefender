using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyCountManager : MonoBehaviour
{
    /// <summary> Number of enemies left for the player to kill. </summary>
    public static int enemyCount;
    public EnemyManager enemyManager;
    Text text;

    void Awake()
    {
        // Get the total number of enemies to be spawned 
        enemyCount = enemyManager.enemyCount;
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = "Enemies: " + enemyCount;
    }
}
