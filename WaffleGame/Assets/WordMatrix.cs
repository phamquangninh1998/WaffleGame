using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class WordMatrix : MonoBehaviour
{
    public static WordMatrix instance;
    public bool canRecover;
    private void Start() {
        instance = this;
        canRecover = true;
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
    
    public void OnHintButtonClick() {
        RecoverOneTile();
    }
    private void RecoverOneTile() {
        if(!canRecover) return;
        List<WordTile> unsolvedWordTiles = new List<WordTile>();
        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < 5; j++) {
                WordTile wordTile = transform.GetChild(i).GetChild(j).GetComponent<WordTile>();
                if (wordTile != null&& wordTile.draggable) {
                    unsolvedWordTiles.Add(wordTile);
                }
            }
        }
        StartCoroutine(DoRecoverOneTile(unsolvedWordTiles));
    }
    IEnumerator DoRecoverOneTile(List<WordTile> wordTiles) {
        canRecover = false;
        for (int i = 0; i < wordTiles.Count; i++) {
            wordTiles[i].draggable = false;
        }
        int k = UnityEngine.Random.Range(0, wordTiles.Count);
        wordTiles[k].Recover();
        yield return new WaitForSeconds(1);
        wordTiles[k].UnRecover();
        for (int i = 0; i < wordTiles.Count; i++) {
            wordTiles[i].draggable = true;
        }
        canRecover = true;
    }
}
