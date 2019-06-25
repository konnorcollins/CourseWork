using UnityEngine;

public class PPart : MonoBehaviour
{
    public Dialogue_System talker;

    void Start() { }

    void Update() { }

    void OnMouseDown()
    {
        if (!talker.talking)
        {
            if (GameState.instance.checkFlag("p6partfound"))
            {

            }
            else if (GameState.instance.checkFlag("p6lookingforpart"))
            {
                talker.StartConversation(Dialogues.p6partgrab);
            }
            else
            {
                talker.StartConversation(Dialogues.p6partearly);
            }
        }
    }
}