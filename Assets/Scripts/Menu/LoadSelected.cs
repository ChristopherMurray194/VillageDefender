using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSelected : MonoBehaviour
{
    /// <summary>
    /// Loads a scene whose index in the project's scene manager, matches the parameter.
    /// </summary>
    /// <param name="sceneIndex"> The index of the scene to be loaded </param>
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
