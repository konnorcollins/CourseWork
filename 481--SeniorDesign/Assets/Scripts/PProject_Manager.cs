using UnityEngine;

public class PProject_Manager : MonoBehaviour
{
    public Dialogue_System talker;

    void Start() { }

    void Update() { }

    void OnMouseDown()
    {
        if (!talker.talking)
        {
            if (GameState.instance.checkFlag("p1engineer") && GameState.instance.checkFlag("p1partsengineer") && GameState.instance.checkFlag("p1mechanic"))
            {
                talker.StartConversation(Dialogues.p1done);
            }
            else
            {
                talker.StartConversation(Dialogues.p1bossreminder);
            }
        }
    }
}