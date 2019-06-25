using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpHandler : MonoBehaviour
{

    public GameObject _popupbox;
    public Text _header;
    public Text _body;


    // Start is called before the first frame update
    void Start()
    {
        _popupbox = GameObject.Find("PopUpContainer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHeaderText(string text)
    {
        _header.text = text;
    }

    public void SetFooterText(string text)
    {
        _body.text = text;
    }

    public void Hide()
    {
        _popupbox.gameObject.SetActive(false);
    }

    public void Show()
    {
        _popupbox.gameObject.SetActive(true);
    }
}
