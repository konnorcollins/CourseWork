using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeRoom : MonoBehaviour
{
    public Texture2D pointer;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public GameObject currentCam;
    public GameObject conCam;

    bool _firstTime = false;
    public PopUpHandler _popup;

    void OnMouseDown()
    {
        currentCam.SetActive(false);
        conCam.SetActive(true);
        if (!_firstTime)
        {
            _firstTime = true;
            _popup.SetHeaderText(GameObject.Find("LO1H").GetComponent<Text>().text);
            _popup.SetFooterText(GameObject.Find("LO1B").GetComponent<Text>().text);
            _popup.Show();
        }
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(pointer, hotSpot, cursorMode);
        Behaviour halo = (Behaviour)gameObject.GetComponent("Halo");
        halo.enabled = true;
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, hotSpot, cursorMode);
        Behaviour halo = (Behaviour)gameObject.GetComponent("Halo");
        halo.enabled = false;
    }
}
