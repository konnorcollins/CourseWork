using UnityEngine;

public class PPartsEngineer : MonoBehaviour
{
    public Dialogue_System talker;

    void Start() { }

    void Update() { }

    void OnMouseDown()
    {
        if (!talker.talking)
        {
            if (GameState.instance.checkFlag("p6clear"))
            {

            }
            else if (GameState.instance.checkFlag("p5clear"))
            {
                if (GameState.instance.checkFlag("p6partfound"))
                {
                    talker.StartConversation(Dialogues.p6found);
                }
                else if (GameState.instance.checkFlag("p6lookingforpart"))
                {
                    talker.StartConversation(Dialogues.p6notfound);
                }
                else if (GameState.instance.checkFlag("p5clear"))
                {
                    talker.StartConversation(Dialogues.p6partsexamine);
                }
                else
                {
                    talker.StartConversation(Dialogues.p6butnop5);
                }
            }
            else if (GameState.instance.checkFlag("p4clear"))
            {

            }
            else if (!GameState.instance.checkFlag("p1partsengineers"))
            {
                talker.StartConversation(Dialogues.p1partsengineer);
            }
        }
    }
}