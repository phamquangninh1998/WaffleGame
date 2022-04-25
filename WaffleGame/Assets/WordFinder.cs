using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WordFinder : MonoBehaviour
{
    public static Result info;
    public WordResult wordResultPrefab;
    public Transform wordResultParent;
    // Start is called before the first frame update

    // Update is called once per frame

    IEnumerator GetWordDefinition(string[] wordSet)
    {
        for (int i = 0; i < wordSet.Length; i++)
        {
            String APIrq = "https://api.dictionaryapi.dev/api/v2/entries/en/";
            using (UnityWebRequest req = UnityWebRequest.Get(APIrq + wordSet[i]))
            {
                yield return req.Send();
                while (!req.isDone)
                    yield return null;
                byte[] result = req.downloadHandler.data;
                string wordResult = System.Text.Encoding.Default.GetString(result);

                Debug.Log(wordResult);
                info = JsonUtility.FromJson<Result>("{\"Results\":" + wordResult + "}");
                if (info.Results.Count > 0 && info.Results[0].meanings.Count > 0)
                {
                    CreateWordResult(wordSet[i], info.Results[0].meanings[0].definitions[0].definition);
                }
            }
        }
    }

    public void CreateWordResult(string word, string def)
    {
        WordResult newWordResult = Instantiate(wordResultPrefab, wordResultParent);
        newWordResult.SetResult(word, def);
    }


    internal void FindDefinitonForWordSet(string[] wordSet)
    {
        StartCoroutine(GetWordDefinition(wordSet));
    }
}
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
[Serializable]
public class License
{
    public String name;
    public String url;

}

[Serializable]
public class Phonetic
{
    public string audio;
    public string sourceUrl;
    public License license;
    public string text;
}

[Serializable]
public class Definition
{
    public string definition;
    public List<string> synonyms;
    public List<string> antonyms;
    public string example;
}

[Serializable]
public class Meaning
{
    public string partOfSpeech;
    public List<Definition> definitions;
    public List<string> synonyms;
    public List<string> antonyms;
}

[Serializable]
public class Root
{
    public string word;
    public List<Phonetic> phonetics;
    public List<Meaning> meanings;
    public License license;
    public List<string> sourceUrls;
}


[Serializable]
public class Result
{
    public List<Root> Results;
}


