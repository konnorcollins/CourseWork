using UnityEngine;

public class PMechanic : MonoBehaviour
{
    public Dialogue_System talker;

    void Start() { }

    void Update() { }

    void OnMouseDown()
    {
        if (!talker.talking)
        {
            
            if (GameState.instance.checkFlag("p4clear"))
            {
                talker.StartConversation(Dialogues.p5mechanicgrab);
            }
            else if (GameState.instance.checkFlag("p1clear"))
            {
                talker.StartConversation(Dialogues.p5mechaniclook);
            }
            else if (!GameState.instance.checkFlag("p1mechanic"))
            {
                talker.StartConversation(Dialogues.p1mechanic);
            }
        }
    }
}