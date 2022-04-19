using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordReader : MonoBehaviour
{
    public TextAsset textFile;
    public string[] wordSet;
    public Transform matrix;
    public static WordReader instance;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

        GenerateWordSet();
        SetWordSetToUpperCase();
        FillWordSetToMatrix();

        SuffleMatrix();
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

    void GenerateWordSet()
    {
        wordSet = new string[6];
        string[] array = textFile.text.Split(new[] { "\n" }, System.StringSplitOptions.None);

        wordSet[0] = array[Random.Range(0, array.Length)];

        int length = array.Length;
        for (int i = 0; i < length; i++)
        {
            if (array[i][0] == wordSet[0][0] && array[i] != wordSet[0])
            {
                for (int j = 0; j < length; j++)
                {
                    if (array[j][0] == array[i][4] && array[j] != wordSet[0] && array[j] != array[i])
                    {
                        for (int k = 0; k < length; k++)
                        {
                            if (array[k][0] == wordSet[0][4] && array[k][4] == array[j][4] && array[k] != wordSet[0] && array[k] != array[j] && array[k] != array[i])
                            {
                                for (int h = 0; h < length; h++)
                                {
                                    if (array[h][0] == array[i][2] && array[h][4] == array[k][2] && array[h] != wordSet[0] && array[h] != array[j] && array[h] != array[i] && array[h] != array[k])
                                    {
                                        for (int g = 0; g < length; g++)
                                        {
                                            if (array[g][0] == wordSet[0][2] && array[g][2] == array[h][2] && array[g][4] == array[j][2] && array[g] != wordSet[0] && array[g] != array[j] && array[g] != array[i] && array[g] != array[k] && array[g] != array[h])
                                            {
                                                wordSet[1] = array[i];
                                                wordSet[2] = array[j];
                                                wordSet[3] = array[k];
                                                wordSet[4] = array[h];
                                                wordSet[5] = array[g];
                                                for (int t = 0; t < 6; t++)
                                                {
                                                    Debug.Log(wordSet[t]);
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
        for (int i = 0; i < wordSet.Length; i++)
        {
            wordSet[i] = wordSet[i].ToUpper();
        }
    }

}
