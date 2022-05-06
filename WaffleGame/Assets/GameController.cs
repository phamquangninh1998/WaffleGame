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
    private void Start()
    {
        instance = this;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame()
    {
        ShowResultInstruction();
        swapCountParent.gameObject.SetActive(false);
    }

    private void ShowResultInstruction()
    {
        worldResult.SetActive(true);
    }

    public void OnInstructionButtonClick() {
        instructionParent.SetActive(true);
    }
    public void ContinueGame() {
        instructionButton.SetActive(true);
        instructionParent.SetActive(false);
    }

    public void ShowInstruction() {
        instructionParent.SetActive(true);
    }
}
