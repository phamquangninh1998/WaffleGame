using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject worldResult;
    public GameObject instructionButton;
    public GameObject instructionParent;
    public GameObject swapCountParent;
    public GameObject winGameBanner;
    public GameObject loseGameBanner;
    public GameObject nextLevelButton;
    private void Start()
    {
        instance = this;
    }
    
    

    private void ShowResultInstruction()
    {
        worldResult.SetActive(true);
        nextLevelButton.SetActive(true);
    }

    public void OnInstructionButtonClick() {
        instructionParent.SetActive(true);
    }
    public void ContinueGame() {
        instructionButton.SetActive(true);
        instructionParent.SetActive(false);
    }
    public void RestartGame() {
        WordReader.instance.ResetMatrix();
        SwapCounter.instance.ResetCounter();
    }
    public void ShowInstruction() {
        instructionParent.SetActive(true);
    }
    public void ShowWingamBanner() {
        winGameBanner.SetActive(true);
    }
    public void ShowLoseGameBanner() {
        loseGameBanner.SetActive(true);
    }
    public  void WinGame() {
      ShowResultInstruction();
       ShowWingamBanner();
    }
    
    public  void LoseGame() {
        ShowResultInstruction();
        ShowLoseGameBanner();
    }
}
