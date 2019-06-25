using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SLaptop : MonoBehaviour
{
    public GameObject pickup;
    public Text text;
    public Texture2D pointer;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public SInventory inventory;

    bool passLearned = false;

    public Dialogue_System talker;

    void OnMouseDown() // current active item
    {

        
        text.enabled = false;

        if (passLearned)
        {
            text.text = "You remember your PIN was: 1024";
        }
        else if (inventory.GetActiveItem() == "pen")
        {
            text.text = "You use the Stylus to find your PIN: 1024";
            passLearned = true;

        } else
        {
            text.text = "The keyboard doesn't work.  Where did you put your Stylus?";
        }

        text.enabled = true;
        text.CrossFadeAlpha(0, 5.0f, false);
        

        if (!talker.talking)
            talker.StartConversation(Dialogues.test);
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
