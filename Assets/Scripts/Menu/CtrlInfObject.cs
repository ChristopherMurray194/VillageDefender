using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This class stores information about a feature in the game which is bound to user input.
/// i.e.W moves the character forward, Left Mouse, is attack.
/// </summary>
public class CtrlInfObject : MonoBehaviour
{
    /// <summary> The image demonstrating the purpose of the control </summary>
    public Sprite image;
    /// <summary> The string for the label attributed to the control </summary>
    public string controlTitle;
}
