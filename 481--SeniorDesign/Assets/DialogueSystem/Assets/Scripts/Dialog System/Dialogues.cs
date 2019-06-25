using UnityEngine;
using UnityEngine.SceneManagement;

public static class Dialogues
{
    // INTRODUCTION
    public static Dialogue_BaseElement gameintroduction =
        new Dialogue_Sentence(get("n10"), get("i00"),
        new Dialogue_Sentence(get("n10"), get("i01"),
        new Dialogue_Sentence(get("n14"), get("i02"),
        new Dialogue_Sentence(get("n14"), get("i03"),
        new Dialogue_Sentence(get("n14"), get("i04"),
        new Dialogue_Sentence(get("n14"), get("i05"),
        new Dialogue_Sentence(get("n14"), get("i06"),
        new Dialogue_Sentence(get("n14"), get("i07"),
        new Dialogue_End(get("n10"), get("i01"), () => { })))))))));

    // PUZZLE 1
    public static Dialogue_BaseElement p1bossreminder =
        new Dialogue_End(get("n05"), get("p0d009"), () => { });
    public static Dialogue_BaseElement p1pr;

    public static Dialogue_BaseElement p1engineer =
        new Dialogue_Question(get("n01"), get("p0d002"),
            get("g00"),
            new Dialogue_Sentence(get("n01"), get("g03"),
                new Dialogue_End(get("n06"), get("g03-2"), () => { GameState.instance.addFlag("p1engineer", true); })),
            get("g01"),
            new Dialogue_End(get("n06"), get("g04"), () => { }));

    public static Dialogue_BaseElement p1accountant =
        new Dialogue_Question(get("n13"), get("p0d003"),
            get("g00"),
            new Dialogue_Sentence(get("n13"), get("g03"),
                new Dialogue_End(get("n06"), get("g03-2"), () => { })),
            get("g01"),
            new Dialogue_End(get("n06"), get("g04"), () => { }));

    public static Dialogue_BaseElement p1mechanic =
        new Dialogue_Question(get("n02"), get("p0d004"),
            get("g00"),
            new Dialogue_Sentence(get("n02"), get("g03"),
                new Dialogue_End(get("n06"), get("g03-2"), () => { GameState.instance.addFlag("p1mechanic", true); })),
            get("g01"),
            new Dialogue_End(get("n06"), get("g04"), () => { }));

    public static Dialogue_BaseElement p1partsengineer =
        new Dialogue_Question(get("n04"), get("p0d005"),
            get("g00"),
            new Dialogue_Sentence(get("n04"), get("g03"),
                new Dialogue_End(get("n06"), get("g03-2"), () => { GameState.instance.addFlag("p1partsengineer", true); })),
            get("g01"),
            new Dialogue_End(get("n06"), get("g04"), () => { }));

    public static Dialogue_BaseElement p1salesperson =
        new Dialogue_Question(get("n03"), get("p0d006"),
            get("g00"),
            new Dialogue_Sentence(get("n03"), get("g03"),
                new Dialogue_End(get("n06"), get("g03-2"), () => { })),
            get("g01"),
            new Dialogue_End(get("n06"), get("g04"), () => { }));

    public static Dialogue_BaseElement p1done =
        new Dialogue_End(get("n05"), get("p0d010s"), () => { GameState.instance.addFlag("p1clear", true); SceneManager.LoadScene("Store", LoadSceneMode.Single); PopUpCaller.instance.CallCurrentStep(); }); // for in-between cutscenes you'll want to replace this with an 'in-between' scene rather than the store proper.

    // PUZZLE 2
    public static Dialogue_BaseElement p2intro =
        new Dialogue_Sentence(get("n00"), get("p1d000"),
        new Dialogue_Sentence(get("n06"), get("p1d001"),
        new Dialogue_Sentence(get("n00"), get("p1d002"),
        new Dialogue_End(get("n06"), get("p1d003"), () => { GameState.instance.addFlag("p2intro", true); }))));

    public static Dialogue_BaseElement p2icecream =
        new Dialogue_Sentence(get("n06"), get("p1d020"),
        new Dialogue_Sentence(get("n06"), get("p1d021"),
        new Dialogue_Sentence(get("n06"), get("p1d022"),
        new Dialogue_Sentence(get("n06"), get("p1d023"),
        new Dialogue_Sentence(get("n06"), get("p1d024"),
        new Dialogue_End(get("n06"), get("p1d025"), () => { GameState.instance.addFlag("p2icecream",true); }))))));

    public static Dialogue_BaseElement p2incomplete =
        new Dialogue_End(get("n06"), get("p1d003"), () => {  });
    public static Dialogue_BaseElement p2finished =
        new Dialogue_End(get("n06"), get("p1d090"), () => { GameState.instance.addFlag("p2clear", true); SceneManager.LoadScene("LAB", LoadSceneMode.Single); PopUpCaller.instance.CallCurrentStep(); }); // See above comment for 'cutscene' transitions

    // PUZZLE 3
    public static Dialogue_BaseElement p3containf =
        new Dialogue_Sentence(get("n06"), get("p2d000"),
        new Dialogue_End(get("n06"), get("p2d001f"), () => { }));
    public static Dialogue_BaseElement p3contains =
        new Dialogue_Sentence(get("n06"), get("p2d000"),
        new Dialogue_Sentence(get("n10"), get("p2d002"),
        new Dialogue_Sentence(get("n10"), get("p2d003"),
        new Dialogue_Sentence(get("n12"), get("p2d004"),
        new Dialogue_Question(get("n06"), get("p2d005"),
            get("p2d005a"),
            new Dialogue_Sentence(get("n12"), get("p2d006"),
                new Dialogue_End(get("n06"), get("p2d007"), () => { GameState.instance.addFlag("p3clear", true); PopUpCaller.instance.CallCurrentStep(); })),
            get("p2d005b"),
            new Dialogue_End(get("n06"), get("p2d008"), () => { })
            )))));

    // PUZZLE 4
    public static Dialogue_BaseElement p4engineertalk =
        new Dialogue_Sentence(get("n06"), get("p3d000"),
        new Dialogue_Sentence(get("n01"), get("p3d001"),
        new Dialogue_Question(get("n06"), get("p3d002"),
            get("p3d002a"),
            new Dialogue_End(get("n06"), "DEBUG: Fail state", () => { }),
            get("p3d002b"),
            new Dialogue_Question(get("n01"), get("p3d003"),
                get("p3d003a"),
                new Dialogue_Sentence(get("n01"), get("p3d004"),
                    new Dialogue_End(get("n01"), get("p3d005"), () => { GameState.instance.addFlag("p4testready", true); })),
                get("p3d003b"),
                new Dialogue_End(get("n06"), "DEBUG: Fail state", () => { })))));

    public static Dialogue_BaseElement p4cartest =
        new Dialogue_Sentence("", get("p3d091"),
        new Dialogue_End("", get("p3d092"), () => { GameState.instance.addFlag("p4testran", true); }));

    public static Dialogue_BaseElement p4engineertest =
        new Dialogue_End(get("n01"), get("p3d005"), () => { });
    public static Dialogue_BaseElement p4engineerfound =
        new Dialogue_End(get("n01"), get("p3d090"), () => { GameState.instance.addFlag("p4clear", true); PopUpCaller.instance.CallCurrentStep(); });


    // PUZZLE 5
    // public static Dialogue_BaseElement p5mechanichood =
    //    new Dialogue_Sentence(get("n06"), get("p4d000"), );
    public static Dialogue_BaseElement p5mechaniclook =
        new Dialogue_End(get("n06"), get("p4d010f"), () => { });
    //public static Dialogue_BaseElement p5enginelook =
    //   new Dialogue_Sentence();
    public static Dialogue_BaseElement p5mechanicgrab =
        new Dialogue_Sentence(get("n06"), get("p4d010s"),
        new Dialogue_Sentence(get("n02"), get("p4d011"),
        new Dialogue_Sentence("", get("p4d011w"),
        new Dialogue_End(get("n02"), get("p4d012"), () => { GameState.instance.addFlag("p5clear", true); PopUpCaller.instance.CallCurrentStep(); }))));

    // PUZZLE 6
    public static Dialogue_BaseElement p6butnop5 =
        new Dialogue_End(get("n06"), get("p4d020f"), () => { });
    public static Dialogue_BaseElement p6partsexamine =
        new Dialogue_Sentence(get("n06"), get("p4d020s"),
        new Dialogue_Sentence(get("n04"), get("p4d021"),
        new Dialogue_Sentence("", get("p4d022w"),
        new Dialogue_Sentence(get("n04"), get("p4d022"),
        new Dialogue_End(get("n04"), get("p5d000"), () => { GameState.instance.addFlag("p6lookingforpart", true); })))));
    public static Dialogue_BaseElement p6partslook;
    public static Dialogue_BaseElement p6notfound =
        new Dialogue_End(get("n06"), get("p5d010f"), () => { });
    public static Dialogue_BaseElement p6partearly = 
        new Dialogue_End(get("n06"), get("p5d001"), () =>{});
    public static Dialogue_BaseElement p6partgrab =
        new Dialogue_Sentence(get("n06"), get("p5d002"),
        new Dialogue_End(get("n06"), get("p5d003"), ()=> { GameState.instance.addFlag("p6partfound", true); }));
    public static Dialogue_BaseElement p6found =
        new Dialogue_Sentence(get("n06"), get("p5d010s"),
        new Dialogue_Sentence(get("n04"), get("p5d011"),
        new Dialogue_End(get("n06"), get("p5d012"), () => { GameState.instance.addFlag("p6clear", true); PopUpCaller.instance.CallCurrentStep(); })));

    // PUZZLE 7
    public static Dialogue_BaseElement p7reportf = new Dialogue_End(get("n06"), get("p6d000f"), () => { });
    public static Dialogue_BaseElement p7reports =
      new Dialogue_Sentence(get("n06"), get("p6d000s"),
      new Dialogue_Sentence(get("n10"), get("p6d001"),
      new Dialogue_Sentence(get("n10"), get("p6d002"),
      new Dialogue_Sentence(get("n11"), get("p6d003"),
      new Dialogue_Question(get("n06"), get("p6d004"),
          get("p6d004a"),
          new Dialogue_Sentence(get("n11"), get("p6d005"),
          new Dialogue_Question(get("n06"), get("p6d006"),
              get("p6d006a"),
              new Dialogue_End(get("n06"), "DEBUG: Failstate", () => { }),
              get("p6d006b"),
              new Dialogue_End(get("n11"), get("p6d007"), () => { GameState.instance.addFlag("p7clear", true); SceneManager.LoadScene("EndScrene", LoadSceneMode.Single); }))),
          get("p6d004b"),
          new Dialogue_End(get("n06"), "DEBUG: Failstate", () => { }))))));

    public static string get(string n)
    {
        return LocalizationManager.instance.GetLocalizedValue(n);
    }

    // IN CASE OF EMERGENCY, BREAK GLASS
    public static Dialogue_BaseElement test =
    new Dialogue_Sentence("Martha", "Hello, John. How are you?",
    new Dialogue_Sentence("John", "Hi, I am just fine. And how are you, Martha?",
    new Dialogue_Question("Martha", "Thank you for asking. I am fine. How's your job search coming along?",
        "Good",
        new Dialogue_Sentence("John", "Really good! I got several interviews next week!",
            new Dialogue_End("John", "I have to go. See you later!", () => { Debug.Log("Possible to do something here."); })),
        "Badly",
        new Dialogue_Sentence("John", "My job searching is going rather slowly, I'm afraid.",
            new Dialogue_End("John", "I have to go. See you later!", () => { Debug.Log("Possible to do something here."); })))));
}