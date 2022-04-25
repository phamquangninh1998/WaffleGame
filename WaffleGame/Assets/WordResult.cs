using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordResult : MonoBehaviour
{
    public Text resultText;
    

    public void SetResult(string word,string definition)
    {
        resultText.text = word + " : " + definition;
    }
}
