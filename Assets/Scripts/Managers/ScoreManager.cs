using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    Text text;

    void Awake()
    {
        // Reset the score if we die. As the score variable is static,
        // when the game is reset the value will not change unless explicitely changed.
        score = 0;
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = "Score: " + score;
    }
}
