using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPen : MonoBehaviour
{
    public GameObject pickup;
    public Text text;
    public Texture2D pointer;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public SInventory inventory;

    bool pickedUp = false;

    void OnMouseDown() // current active item
    {
        text.enabled = false;

        if (pickedUp)
        {
            text.text = "You already picked this up the pen.";
        } else
        {
            text.text = "You pick up the pen.";
            pickedUp = true;
            inventory.Add("pen");
        }

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
