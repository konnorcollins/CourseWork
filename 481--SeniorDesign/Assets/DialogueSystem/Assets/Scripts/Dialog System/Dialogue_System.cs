using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System; 

public class Dialogue_System : MonoBehaviour
{
    public bool talking = false;

    public float readingSpeed = 0.05f;
    public float buttonFadingTime = 2f;
    public Color answer1ButtonColor = Color.white;
    public Color answer2ButtonColor = Color.white;
    public Color answer1ButtonTextColor = Color.black;
    public Color answer2ButtonTextColor = Color.black; 

    private Image panel;
    private Text header;
    private Text body;
    private Image answer1;
    private Image answer2;
    private Text answer1Text;
    private Text answer2Text;
    private bool listeningForClicks = false; 
    private bool clicked = false;
    private int answerSelected = -1;

    private void Start()
    {
        panel = GetComponent<Image>();

        header = transform.Find("Header").GetComponent<Text>(); 
        body = transform.Find("Body").GetComponent<Text>();

        answer1 = transform.Find("Answer1").GetComponent<Image>();
        answer2 = transform.Find("Answer2").GetComponent<Image>();
        answer1Text = answer1.transform.Find("Text").GetComponent<Text>();
        answer2Text = answer2.transform.Find("Text").GetComponent<Text>();

        Disable(); 

        //StartConversation((Dialogue_Sentence)Dialogues.conversation1);
    }

    private void Update()
    {
        if (listeningForClicks)
            if (Input.GetMouseButtonDown(0))
            {
                clicked = true;
            }
    }

    private void Disable()
    {
        talking = false;
        panel.enabled = false;
        answer1.enabled = false;
        answer2.enabled = false;
        header.enabled = false;
        body.enabled = false; 
    }

    private void Enable()
    {
        talking = true;
        panel.enabled = true;
        header.enabled = true;
        body.enabled = true;
    }

    public void Answer(int answerNumber)
    {
        answerSelected = answerNumber;
    }

    public void StartConversation(Dialogue_BaseElement conversation)
    {
        Enable(); 

        if(conversation is Dialogue_Sentence)
        {
            header.text = conversation.header; 
            Dialogue_Sentence e = (Dialogue_Sentence)conversation;
            StartCoroutine(WriteSentence(e.text,e.nextElement));
        }
        else if(conversation is Dialogue_Question)
        {
            header.text = conversation.header;
            Dialogue_Question e = (Dialogue_Question)conversation;
            StartCoroutine(WriteQuestion(e.text, e.answer1Text, e.answer2Text, e.answer1Consequence, e.answer2Consequence));
        }
        else if(conversation is Dialogue_End)
        {
            header.text = conversation.header;
            Dialogue_End e = (Dialogue_End)conversation;
            StartCoroutine(WriteEnd(e.text, e.endAction)); 
        }
    }

    public IEnumerator WriteEnd(string text, Action endAction)
    {
        char[] textArr = text.ToCharArray();

        listeningForClicks = true; 
        for (int i = 0; i < textArr.Length; i++)
        {
            if (clicked == true)
            {
                body.text = text;
                clicked = false;
                break;
            }
            yield return new WaitForSeconds(readingSpeed);
            body.text += textArr[i].ToString();

        }

        while (true)
        {
            if (clicked == true)
            {
                clicked = false;
                break;

            }
            yield return null;
        }
        listeningForClicks = false;

        body.text = "";
        endAction();
        Disable(); 
        yield return null;

    }

    public IEnumerator WriteQuestion(string text, string _answer1, string _answer2, Dialogue_BaseElement consequence1, Dialogue_BaseElement consequence2)
    {
        char[] textArr = text.ToCharArray();

        listeningForClicks = true;

        for (int i = 0; i < textArr.Length; i++)
        {
            if (clicked == true)
            {
                body.text = text;
                clicked = false;
                break;
            }
            yield return new WaitForSeconds(readingSpeed);
            body.text += textArr[i].ToString();

        }

        listeningForClicks = false; 

        answer1Text.text = _answer1;
        answer2Text.text = _answer2;
        answer1.enabled = true;
        answer2.enabled = true;
        answer1.color = new Color(0, 0, 0, 0);
        answer2.color = new Color(0, 0, 0, 0);
        answer1Text.color = new Color(0, 0, 0, 0);
        answer2Text.color = new Color(0, 0, 0, 0);

        float current = 0;

        while (answer1.color != answer1ButtonColor)
        {
            current += Time.deltaTime * buttonFadingTime;

            answer1.color = Color.Lerp(new Color(0, 0, 0, 0), answer1ButtonColor, Mathf.Lerp(0, 1f, current));
            answer2.color = Color.Lerp(new Color(0, 0, 0, 0), answer2ButtonColor, Mathf.Lerp(0, 1f, current));

            answer1Text.color = Color.Lerp(new Color(0, 0, 0, 0), answer1ButtonTextColor, Mathf.Lerp(0, 1f, current));
            answer2Text.color = Color.Lerp(new Color(0, 0, 0, 0), answer2ButtonTextColor, Mathf.Lerp(0, 1f, current));
            yield return null;

        }

        while(answerSelected == -1)
        {
            yield return null;
        }

        current = 0;

        while (answer1.color != new Color(0, 0, 0, 0))
        {
            current += Time.deltaTime * buttonFadingTime;

            answer1.color = Color.Lerp(answer1ButtonColor, new Color(0, 0, 0, 0), Mathf.Lerp(0, 1f, current));
            answer2.color = Color.Lerp(answer2ButtonColor, new Color(0, 0, 0, 0), Mathf.Lerp(0, 1f, current));

            answer1Text.color = Color.Lerp(answer1ButtonTextColor, new Color(0, 0, 0, 0), Mathf.Lerp(0, 1f, current));
            answer2Text.color = Color.Lerp(answer2ButtonTextColor, new Color(0, 0, 0, 0), Mathf.Lerp(0, 1f, current));
            yield return null;

        }

        body.text = "";

        if (answerSelected == 1)
        {
            StartConversation(consequence1);
        }
        else if(answerSelected == 2)
        {
            StartConversation(consequence2);
        }
        answerSelected = -1;
        yield return null;
    }

    public IEnumerator WriteSentence(string text, Dialogue_BaseElement nextElement)
    {
        char[] textArr = text.ToCharArray();

        listeningForClicks = true; 

        for(int i = 0; i < textArr.Length; i++)
        {
            if(clicked == true)
            {
                body.text = text;
                clicked = false;
                break;
            }

            yield return new WaitForSeconds(readingSpeed);
            body.text += textArr[i].ToString();
        }

        while (true)
        {
            if (clicked == true)
            {
                clicked = false;
                break;

            }
            yield return null;
        }

        listeningForClicks = false;

        body.text = "";
        StartConversation(nextElement);

        yield return null; 
    }
}
