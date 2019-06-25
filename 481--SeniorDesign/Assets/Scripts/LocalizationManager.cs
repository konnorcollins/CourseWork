using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;
    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    private string missingTextString = "Localized Text Not Found!";

    // Start is called before the first frame update
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

    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath + "/", fileName);
        string dataAsJson;

        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            StartCoroutine("GetTextWeb", filePath);
        } else {
            dataAsJson = File.ReadAllText(filePath);

            if (File.Exists(filePath))
            {
                LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

                for (int i = 0; i < loadedData.items.Length; i++)
                {
                    localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
                }
            }
            else
            {
                Debug.LogError("Cannot find file");
            }

            Debug.Log("Text is now ready");

            isReady = true;
        }
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        return result;
    }

    public bool GetIsReady()
    {
        return isReady;
    }

    IEnumerator GetTextWeb(string filePath)
    {
        UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get (filePath);
        yield return www.SendWebRequest();
        string dataAsJson = www.downloadHandler.text;

        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData> (dataAsJson);

		for (int i = 0; i < loadedData.items.Length; i++) {
			localizedText.Add (loadedData.items [i].key, loadedData.items [i].value);
			Debug.Log ("KEYS:" + loadedData.items [i].key);
		}

        Debug.Log("Text is now ready (web)");

		isReady = true;
    }
}