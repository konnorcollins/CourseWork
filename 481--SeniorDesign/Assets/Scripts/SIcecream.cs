using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SIcecream : MonoBehaviour
{
    public GameObject pickup;
    public Text text;
    public Texture2D pointer;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public SInventory inventory;

    public Dialogue_System talker;

    bool pickedUp = false;

    void OnMouseDown() // current active item
    {

        if (!talker.talking && !pickedUp)
        {
            talker.StartConversation(Dialogues.p2icecream);
        }
        text.enabled = false;

        if (pickedUp)
        {
            text.text = "You already picked this up the icecream.";
        }
        else
        {
            text.text = "You pick up the icecream.";
            pickedUp = true;
            inventory.Add("icecream");
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

