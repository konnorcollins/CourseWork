using UnityEngine;

public class PCar: MonoBehaviour
{
    public Dialogue_System talker;

    void Start() { }

    void Update() { }

    void OnMouseDown()
    {
        if (!talker.talking)
        {
            if (GameState.instance.checkFlag("p5clear")) // PUZZLE 6 OR LATER
            {

            }
            else if (GameState.instance.checkFlag("p4clear")) // PUZZLE 5
            {
            }
            else if (GameState.instance.checkFlag("p3clear")) // PUZZLE 4
            {
                if (GameState.instance.checkFlag("p4testran")) // car already tested
                {

                }
                else if (GameState.instance.checkFlag("p4testready")) // ready to test the car
                {
                    talker.StartConversation(Dialogues.p4cartest);
                }
                else
                {
                }
            }
            else // PUZZLE 3 OR EARLIER
            {
            }
        }
    }
}