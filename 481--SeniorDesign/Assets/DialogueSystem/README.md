# DialogueSystem
A simple RPG-style text dialogue system for Unity. 

<a href="https://imgur.com/zudLOXE"><img src="https://i.imgur.com/zudLOXE.png" title="source: imgur.com" /></a>

# Examples
You can create a dialogue like this: 

```csharp
  public static Dialogue_BaseElement example1 = new Dialogue_End("Header","Dialoge text", ()=> { /* This will be called at the end of the dialogue*/ });
```
You can create a longer and more complex dialogue like this: 

```csharp
    public static Dialogue_BaseElement example2 =
        new Dialogue_Sentence("Martha", "Hello, John. How are you?",
        new Dialogue_Sentence("John", "Hi, I am just fine. And how are you, Martha?",
        new Dialogue_Question("Martha", "Thank you for asking. I am fine. How's your job search coming along?",
            "Good",
            new Dialogue_Sentence("John", "Really good! I got several interviews next week!",
                new Dialogue_End("John", "I have to go. See you later!", () => { Debug.Log("Possible to do something here."); })),
            "Badly",
            new Dialogue_Sentence("John", "My job searching is going rather slowly, I'm afraid.",
                new Dialogue_End("John", "I have to go. See you later!", () => { Debug.Log("Possible to do something here."); })))));
```

# Dialogue objects

```csharp
Dialogue_Sentence(string _header, string _text, Dialogue_BaseElement _nextElement)
```
Display a sentence. It needs another Dialogue object to display next. 



```csharp
Dialogue_Question(string _header, string _text, string _answer1Text, Dialogue_BaseElement _answer1Consequence, string _answer2Text, Dialogue_BaseElement _answer2Consequence)
```
This is used when you want the player to answer a question. ```string _answer1Text``` and ```string _answer2Text``` will be used on the Button text. If the player is clicking on button 1, the dialogue will continue with ```Dialogue_BaseElement _answer1Consequence``` as the next dialogue object.



```csharp
Dialogue_End(string _header, string _text, Action _endAction)
```
This is used to end the dialogue. ```Action _endAction``` will be called when the dialogue UI is closing.
