using UnityEngine;

public class PopUpCaller : MonoBehaviour
{
    public PopUpHandler _target;
    public static PopUpCaller instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _target = GameObject.Find("PopUpContainer").GetComponent<PopUpHandler>();
    }

    void Update(){    }


    public void CallCurrentStep()
    {
        if (GameState.instance.checkFlag("p6clear"))
        {
            _target.SetHeaderText(LocalizationManager.instance.GetLocalizedValue("h7"));
            _target.SetFooterText(LocalizationManager.instance.GetLocalizedValue("b7"));
            _target.Show();
        }
        else if (GameState.instance.checkFlag("p5clear"))
        {
            _target.SetHeaderText(LocalizationManager.instance.GetLocalizedValue("h6"));
            _target.SetFooterText(LocalizationManager.instance.GetLocalizedValue("b6"));
            _target.Show();
        }
        else if (GameState.instance.checkFlag("p4clear"))
        {
            _target.SetHeaderText(LocalizationManager.instance.GetLocalizedValue("h5"));
            _target.SetFooterText(LocalizationManager.instance.GetLocalizedValue("b5"));
            _target.Show();
        }
        else if (GameState.instance.checkFlag("p3clear"))
        {
            _target.SetHeaderText(LocalizationManager.instance.GetLocalizedValue("h4"));
            _target.SetFooterText(LocalizationManager.instance.GetLocalizedValue("b4"));
            _target.Show();
        }
        else if (GameState.instance.checkFlag("p2clear"))
        {
            _target.SetHeaderText(LocalizationManager.instance.GetLocalizedValue("h3"));
            _target.SetFooterText(LocalizationManager.instance.GetLocalizedValue("b3"));
            _target.Show();
        }
        else if (GameState.instance.checkFlag("p1clear"))
        {
            _target.SetHeaderText(LocalizationManager.instance.GetLocalizedValue("h2"));
            _target.SetFooterText(LocalizationManager.instance.GetLocalizedValue("b2"));
            _target.Show();
        }
        else
        {
            _target.SetHeaderText(LocalizationManager.instance.GetLocalizedValue("h1"));
            _target.SetFooterText(LocalizationManager.instance.GetLocalizedValue("b1"));
            _target.Show();
        }
    }

}