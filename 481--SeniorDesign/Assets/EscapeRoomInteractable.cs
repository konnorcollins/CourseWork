using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeRoomInteractable : MonoBehaviour
{
    public GameObject pickup;
    public Text text;
    public Texture2D pointer;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void OnMouseDown(/* string item */) // current active item
    {
        text.enabled = false;
        text.text = "This is a test, HIIIIII!!!!!!";
        text.enabled = true;
        text.CrossFadeAlpha(0, 5.0f, false);
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(pointer, hotSpot, cursorMode);
        Behaviour halo = (Behaviour)pickup.GetComponent("Halo");
        halo.enabled = true;
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, hotSpot, cursorMode);
        Behaviour halo = (Behaviour)pickup.GetComponent("Halo");
        halo.enabled = false;
    }
}
