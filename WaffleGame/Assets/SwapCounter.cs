using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapCounter : MonoBehaviour
{
    public int swapsRemain;
    public Text swapText;
    public static SwapCounter instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        swapsRemain = 15;
        SetSwapText();
    }

    private void SetSwapText()
    {
        swapText.text = "" + swapsRemain;
    }

    public void ResetCounter() {
        swapsRemain = 15;
        SetSwapText();
    }
    public void ConsumSwap()
    {
        if (swapsRemain == 1 && !WordMatrix.instance.WonGame()) {
            GameController.instance.LoseGame();
            WordMatrix.instance.SetLoseMatrixState();
        } else {
            if (WordMatrix.instance.WonGame()) {
                GameController.instance.WinGame();
            }
        }
        swapsRemain--;
        SetSwapText();

    }

    public bool EnoughSwapToConsume()
    {
        return swapsRemain > 0;
    }
}
