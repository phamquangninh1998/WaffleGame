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

    public void ConsumSwap()
    {
        swapsRemain--;
        SetSwapText();
    }

    public bool EnoughSwapToConsume()
    {
        return swapsRemain > 0;
    }
}
