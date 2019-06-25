public class Dialogue_Sentence : Dialogue_BaseElement
{
    public Dialogue_BaseElement nextElement; 

    public Dialogue_Sentence(string _header, string _text, Dialogue_BaseElement _nextElement)
    {
        header = _header; 
        text = _text;
        nextElement = _nextElement; 
    }
}
