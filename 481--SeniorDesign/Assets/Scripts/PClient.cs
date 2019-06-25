using UnityEngine;

public class PClient : MonoBehaviour
{
    public Dialogue_System talker;

    void Start() { }

    void Update() { }

    void OnMouseDown()
    {
        if (!talker.talking)
        {
            if (GameState.instance.checkFlag("p2intro"))
            {
                talker.StartConversation(Dialogues.p2finished);
            }
            else
            {
                talker.StartConversation(Dialogues.p2intro);
            }
        }
    }
}