using UnityEngine;

public class PPhone : MonoBehaviour
{
    public Dialogue_System talker;

    void Start() { }

    void Update() { }

    void OnMouseDown()
    {
        if (!talker.talking)
        {
            if (GameState.instance.checkFlag("p7clear"))
            {

            }
            else if (GameState.instance.checkFlag("p6clear"))
            {
                talker.StartConversation(Dialogues.p7reports);
            }
            else if (!GameState.instance.checkFlag("p3clear"))
            {
                talker.StartConversation(Dialogues.p3contains);
            }
            else
            {

            }
        }
    }
}