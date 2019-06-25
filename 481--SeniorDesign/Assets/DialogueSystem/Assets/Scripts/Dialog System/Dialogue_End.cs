using System; 

public class Dialogue_End : Dialogue_BaseElement
{
    public Action endAction; 
    public Dialogue_End(string _header, string _text, Action _endAction)
    {
        header = _header; 
        text = _text;
        endAction = _endAction;
    }
}
