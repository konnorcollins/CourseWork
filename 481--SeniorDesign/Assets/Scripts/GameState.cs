using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState instance;
    private Dictionary<string, bool> flags;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        flags = new Dictionary<string, bool>();
    }

    public void addFlag(string desc, bool flag)
    {
        if (!checkFlag(desc))
            flags.Add(desc, flag);
    }

    public bool checkFlag(string desc)
    {
        bool value;
        flags.TryGetValue(desc, out value);
        return value;
    }

    public void flagPassed(string desc)
    {
        if(flags.Remove(desc)) {
            addFlag(desc, true);
        }
    }
}
