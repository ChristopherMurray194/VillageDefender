using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageSlideshow : MonoBehaviour
{
    public CtrlInfObject[] CtrlInf;

    /// <summary> This game object has the Control title Text component as a child </summary>
    GameObject labelFrame;
    /// <summary> The text component, which is the title for the input binding </summary>
    Text controlTitle;
    /// <summary> The image shown to desmonstrate the control's functionality </summary>
    GameObject displayedImage;

    /// <summary> Current index pointed to of the array 'CtrlInf' </summary>
    int index = 0;
    public int Index
    {
        get { return index; }
    }

    public void Awake()
    {
        labelFrame = GameObject.Find("ControlTextFrame");
        controlTitle = labelFrame.GetComponentInChildren<Text>();
        displayedImage = GameObject.Find("ControlImage");

        // Set the first user input binding information to be shown
        controlTitle.text = CtrlInf[index].controlTitle;    // Change the text
        displayedImage.GetComponentInChildren<Image>().sprite = CtrlInf[index].image;   // Change the image
    }

    /// <summary>
    /// Display the set of information for the next user input binding.
    /// </summary>
    public void NextBinding()
    {
        // If we are currently displaying the last element of 'CtrlInf'
        if (index + 1 >= CtrlInf.Length)
            // Point to the beginning of 'CtrlInf'
            index = 0;
        else
            ++index;

        controlTitle.text = CtrlInf[index].controlTitle;
        displayedImage.GetComponentInChildren<Image>().sprite = CtrlInf[index].image;
    }

    /// <summary>
    /// Display the set of information for the previous user input binding.
    /// </summary>
    public void PrevBinding()
    {
        // If we are currently displaying the first element of 'CtrlInf'
        if (index - 1 < 0)
            // Point to the end of 'CtrlInf'
            index = CtrlInf.Length - 1;
        else
            --index;

        controlTitle.text = CtrlInf[index].controlTitle;
        displayedImage.GetComponentInChildren<Image>().sprite = CtrlInf[index].image;
    }

}
