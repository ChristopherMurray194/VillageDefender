using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * This class stores information about a feature in the game which is bound to user input.
 * i.e. W moves the character forward, Left Mouse, is attack.
 */
public class CtrlInfObject : MonoBehaviour
{
    //  The image demonstrating the purpose of the control
    public Sprite image;
    // The string for the label attributed to the control
    public string controlTitle;
}
