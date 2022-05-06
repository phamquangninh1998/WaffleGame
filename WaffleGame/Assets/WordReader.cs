using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordReader : MonoBehaviour
{
    public TextAsset wordPool;
    public TextAsset definitionPool;
    public string[] wordSet;
    public string[] definitionSet;
    public Transform matrix;
    public Transform wordResultParent;
    public WordResult wordResultPrefab;
    public static WordReader instance;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

        GenerateWordSet();
        SetFinalResult();
        SetWordSetToUpperCase();
        FillWordSetToMatrix();
        SuffleMatrix();
        SetWordResult();
    }

    private void SetWordResult()
    {
        for (int i = 0; i < 6; i++)
        {
            WordResult newWordResult = Instantiate(wordResultPrefab, wordResultParent);
            newWordResult.SetResult(wordSet[i], definitionSet[i]);
        }
    }

    public string[] GetWordSet()
    {
        return wordSet;
    }
    private void SuffleMatrix()
    {
        for (int i = 0; i < 26; i++)
        {
            WordTile wordA = matrix.GetChild(Random.Range(0, 5)).GetChild(Random.Range(0, 5)).GetComponent<WordTile>();
            WordTile wordB = matrix.GetChild(Random.Range(0, 5)).GetChild(Random.Range(0, 5)).GetComponent<WordTile>();

            if ((wordA != null) && (wordB != null))
            {

                wordA.SwapValue(wordB);
            }
        }
    }

    private void FillWordSetToMatrix()
    {
        for (int i = 0; i < 5; i++)
        {
            matrix.GetChild(0).GetChild(i).GetComponent<WordTile>().SetStartValue(wordSet[0][i].ToString());
            matrix.GetChild(i).GetChild(0).GetComponent<WordTile>().SetStartValue(wordSet[1][i].ToString());
            matrix.GetChild(4).GetChild(i).GetComponent<WordTile>().SetStartValue(wordSet[2][i].ToString());
            matrix.GetChild(i).GetChild(4).GetComponent<WordTile>().SetStartValue(wordSet[3][i].ToString());
            matrix.GetChild(2).GetChild(i).GetComponent<WordTile>().SetStartValue(wordSet[4][i].ToString());
            matrix.GetChild(i).GetChild(2).GetComponent<WordTile>().SetStartValue(wordSet[5][i].ToString());
        }
    }

    void GenerateWordSet() {
        wordSet = new string[6];
        definitionSet = new string[6];
        string[] wordArray = wordPool.text.Split(new[] { "\n" }, System.StringSplitOptions.None);
        string[] definitionArray = definitionPool.text.Split(new[] { "\n" }, System.StringSplitOptions.None);

        int count = 0;
        while (true) {
            count++;
            int i = Random.Range(0, wordArray.Length);
            wordSet[0] = wordArray[i];
            definitionSet[0] = definitionArray[i];
            if (wordSet[0].Length == 5||count==9999) break;
        }

        int length = wordArray.Length;
        for (int i = 0; i < length; i++) {
            if (wordArray[i].Length == 5 && wordArray[i][0] == wordSet[0][0] && wordArray[i] != wordSet[0]) {
                for (int j = 0; j < length; j++) {
                    if (wordArray[j].Length == 5 && wordArray[j][0] == wordArray[i][4] && wordArray[j] != wordSet[0] && wordArray[j] != wordArray[i]) {
                        for (int k = 0; k < length; k++) {
                            if (wordArray[k].Length == 5 && wordArray[k][0] == wordSet[0][4] && wordArray[k][4] == wordArray[j][4] && wordArray[k] != wordSet[0] && wordArray[k] != wordArray[j] && wordArray[k] != wordArray[i]) {
                                for (int h = 0; h < length; h++) {
                                    if (wordArray[h].Length == 5 && wordArray[h][0] == wordArray[i][2] && wordArray[h][4] == wordArray[k][2] && wordArray[h] != wordSet[0] && wordArray[h] != wordArray[j] && wordArray[h] != wordArray[i] && wordArray[h] != wordArray[k]) {
                                        for (int g = 0; g < length; g++) {
                                            if (wordArray[g].Length == 5 && wordArray[g][0] == wordSet[0][2] && wordArray[g][2] == wordArray[h][2] && wordArray[g][4] == wordArray[j][2] && wordArray[g] != wordSet[0] && wordArray[g] != wordArray[j] && wordArray[g] != wordArray[i] && wordArray[g] != wordArray[k] && wordArray[g] != wordArray[h]) {
                                                wordSet[1] = wordArray[i];
                                                wordSet[2] = wordArray[j];
                                                wordSet[3] = wordArray[k];
                                                wordSet[4] = wordArray[h];
                                                wordSet[5] = wordArray[g];
                                                definitionSet[1] = definitionArray[i];
                                                definitionSet[2] = definitionArray[j];
                                                definitionSet[3] = definitionArray[k];
                                                definitionSet[4] = definitionArray[h];
                                                definitionSet[5] = definitionArray[g];
                                                for (int t = 0; t < 6; t++) {
                                                    Debug.Log(wordSet[t]);
                                                    Debug.Log(definitionSet[t]);
                                                }
                                                return;
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

    }

    void SetWordSetToUpperCase()
    {
        Debug.Log("Set to upper case");
        for (int i = 0; i < wordSet.Length; i++)
        {
            wordSet[i] = wordSet[i].ToUpper();
        }
    }
    public void SetFinalResult()
    {
       
    }
}
