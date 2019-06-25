using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SInventory : MonoBehaviour
{
    List<string> _items;
    int _offset;
    int _active;
    public List<GameObject> _buttons;


    public Sprite pen;
    public Sprite icecream;


    // Start is called before the first frame update
    void Start()
    {
        _items = new List<string>();
        _offset = 0;
        _active = -1;

    }

    // Update is called once per frame
    //Might look at moving this to a variable to keep things from pining each sec.
    void Update()
    {
        for (int i = _offset; i < 4; i++)
        {
            if (_items[i] == null) { }

            else if (_items[i] == "pen")
                _buttons[i].GetComponent<Image>().sprite = pen;
            else if (_items[i] == "icecream")
                _buttons[i].GetComponent<Image>().sprite = icecream;
        }
    }

    // changes item view, used with arrow buttons
    public void Scroll(int delta)
    {
        if (delta < 0) // scroll left
        {
            if (_offset - delta >= 0)
                _offset -= delta;
        }

        if (delta > 0) // scroll right
        {
            if (_offset + delta <= _items.Count - 2)
                _offset += delta;
        }
    }

    public void SetActiveSlot(int active)
    {
        if (_active == active)
            _active = -1;
        else
            _active = active;
    }


    public string GetActiveItem()
    {
        if (_active < 0 || _items[_active + _offset] == null)
        {
            return "nothing";
        }
        else
            return _items[_active + _offset];
    }

    public string GetItemSlot(int slot)
    {
        if (_items[_offset + slot] == null)
        {
            return "nothing";
        }
        else
            return _items[_offset + slot];
    }

    public void Add(string item)
    {
        if (!(_items.Contains(item)))
        {
            _items.Add(item);
        }

    }

}
