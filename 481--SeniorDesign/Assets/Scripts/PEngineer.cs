using UnityEngine;

public class PEngineer : MonoBehaviour
{
    public Dialogue_System talker;

    void Start() { }

    void Update() { }

    void OnMouseDown()
    {
        if (!talker.talking)
        {
            if (GameState.instance.checkFlag("p4clear")) // Puzzle 5 or beyond
            {

            }
            else if (GameState.instance.checkFlag("p3clear")) // Puzzle 4
            {
                if (GameState.instance.checkFlag("p4testran"))
                {
                    talker.StartConversation(Dialogues.p4engineerfound);
                }
                else if (GameState.instance.checkFlag("p4testready"))
                {
                    talker.StartConversation(Dialogues.p4engineertest);
                }
                else
                {
                    talker.StartConversation(Dialogues.p4engineertalk);
                }
            }
            else
            {
                if (!GameState.instance.checkFlag("p1engineer"))
                {
                    talker.StartConversation(Dialogues.p1engineer);
                }
                else
                {

                }
            }
        }
    }
}