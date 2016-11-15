using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject selectedObject;

    /// <summary> Is this button currently selected? </summary>
    bool buttonSelected;

    void Update()
    {
        if(Input.GetAxisRaw("Vertical") != 0 && !buttonSelected)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }

    void OnDisable()
    {
        buttonSelected = false;
    }
}
