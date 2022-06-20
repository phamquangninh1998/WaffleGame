using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEditor;

public class WordScanner : MonoBehaviour
{
    public static Result info;
    public TextAsset WordListTextAsset;
    public TextAsset WordPool;
    public TextAsset DefinitionPool;
    // Start is called before the first frame update
    private void Start() {
        ScanAndWriteToFile();
    }

    public void ScanAndWriteToFile() {
        string[] array = WordListTextAsset.text.Split(new[] { "\n" }, System.StringSplitOptions.None);
        StartCoroutine(DoScanWordList(array));
    }
    // Update is called once per frame

    IEnumerator DoScanWordList(string[] wordList)
    {
        for (int i = 0; i < wordList.Length; i++)
        {
            if(wordList[i].Length!=5) continue;
            String APIrq = "https://api.dictionaryapi.dev/api/v2/entries/en/";
            using (UnityWebRequest req = UnityWebRequest.Get(APIrq + wordList[i]))
            {
                yield return req.Send();
                while (!req.isDone)
                    yield return null;
                byte[] result = req.downloadHandler.data;
                string stringJsonResult = System.Text.Encoding.Default.GetString(result);

                if (stringJsonResult[0] == '[') {

                    info = JsonUtility.FromJson<Result>("{\"Results\":" + stringJsonResult + "}");

                    if (info.Results.Count > 0 && info.Results[0].meanings.Count > 0) {
                        Debug.Log(stringJsonResult);
                        WriteWordToFile(wordList[i], info.Results[0].meanings[0].definitions[0].definition);
                    }
                }
                
            }
        }
    }

    public void WriteWordToFile(string word, string def) {
        using (StreamWriter wordStreamWriter = File.AppendText(AssetDatabase.GetAssetPath(WordPool)))
        {
            wordStreamWriter.WriteLine(word);
        }
        using (StreamWriter defStreamWriter = File.AppendText(AssetDatabase.GetAssetPath(DefinitionPool)))
        {
            defStreamWriter.WriteLine(def);
        }
        EditorUtility.SetDirty(WordPool);
        EditorUtility.SetDirty(DefinitionPool);
    }
}
