using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageSlideshow : MonoBehaviour
{
    public CtrlInfObject[] CtrlInf;

    GameObject labelFrame;          // This game object has the Control title Text component as a child
    Text controlTitle;              // The text component, which is the title for the input binding
    GameObject displayedImage;      // The image shown to desmonstrate the control's functionality

    int index = 0;                  // Current index pointed to of the array 'CtrlInf'
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

    /**
     * Display the set of information for the next user input binding
     */
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

    /**
     * Display the set of information for the previous user input binding
     */
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
