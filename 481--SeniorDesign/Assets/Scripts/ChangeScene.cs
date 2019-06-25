using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public Texture2D pointer;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    bool _firstTime = false;
    //public PopUpHandler _popup;
    //public GameObject nextScene;
    //public Button button;

    // Start is called before the first frame update
    void Start()
    {
        //button.onClick.AddListener(ChangeScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        if (SceneManager.GetActiveScene().name == "Office")
        {            
            SceneManager.LoadScene("Store", LoadSceneMode.Single);
        }
        else if (SceneManager.GetActiveScene().name == "Store")
        {
            SceneManager.LoadScene("LAB", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Office", LoadSceneMode.Single);
        }
    }

 }
