using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
    Changes to a certain camera when a button is clicked. Add to camera object as a component
 */
public class ChangeCam : MonoBehaviour
{
    public GameObject nextCamera; //The camera to be swithed to
    public Button button; //The button the user presses to go to the next camera.

    void Start() 
    {
        button.onClick.AddListener(ChangeCamera);
    }

    void ChangeCamera()
    {
        StartCoroutine(testChange());
    }

    IEnumerator testChange()
    {
        if(gameObject.activeSelf) {
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
            nextCamera.SetActive(true);
        }
    }
}