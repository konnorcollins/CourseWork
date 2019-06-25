using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    This needs to be added as a GameObject to every scene! Put the starting camera in the 0 index of the list
 */
public class CameraController : MonoBehaviour
{

    public List<GameObject> cameras = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        disableCams();

        //Sets the camera at index 0 to active, so make sure the starting camera is in index 0
        cameras[0].SetActive(true);
        
    }

    //Disables all cameras in the scene
    public void disableCams()
    {
        foreach(GameObject c in cameras) {
            c.SetActive(false);
        }
    }
}
