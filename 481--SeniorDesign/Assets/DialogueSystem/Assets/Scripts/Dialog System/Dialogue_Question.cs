public class Dialogue_Question : Dialogue_BaseElement
{
    public string answer1Text;
    public string answer2Text;
    public Dialogue_BaseElement answer1Consequence;
    public Dialogue_BaseElement answer2Consequence; 

    public Dialogue_Question(string _header, string _text, string _answer1Text, Dialogue_BaseElement _answer1Consequence, string _answer2Text, Dialogue_BaseElement _answer2Consequence)
    {
        header = _header; 
        text = _text;
        answer1Text = _answer1Text;
        answer1Consequence = _answer1Consequence;
        answer2Text = _answer2Text;
        answer2Consequence = _answer2Consequence;
    }
}