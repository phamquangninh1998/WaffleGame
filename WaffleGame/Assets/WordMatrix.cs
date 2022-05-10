using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordMatrix : MonoBehaviour
{
    public static WordMatrix instance;
    private void Start() {
        instance = this;
    }
    public bool WonGame() {
        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < 5; j++) {
                WordTile wordTile = transform.GetChild(i).GetChild(j).GetComponent<WordTile>();
                if (wordTile != null && wordTile.draggable == true) {
                    return false;
                }
            }
        }
        return true;
    }
    public void SetLoseMatrixState() {
        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < 5; j++) {
                WordTile wordTile = transform.GetChild(i).GetChild(j).GetComponent<WordTile>();
                if (wordTile != null) {
                    wordTile.SetLoseState();
                }
            }
        }
    }

}
