using UnityEngine;

// Just a super simple delivery vehicle for introductory conversaition(s) AND NOTHING ELSE
public class PIntro : MonoBehaviour
{
    public Dialogue_System talker;

    bool talked = false;

    void Start()
    {
    }

    void Update()
    {
        if (!talked)
        {
            if (!talker.talking)
            {
                talker.StartConversation(Dialogues.gameintroduction);
                talked = true;
            }
        }
    }

}