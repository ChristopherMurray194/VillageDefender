using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour
{
    /// <summary>
    /// Quits the game.
    /// </summary>
    public void Quit()
    {
        // If the game is running in the editor
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else // Otherwise when running in a build
                Application.Quit();
        #endif
    }
}
