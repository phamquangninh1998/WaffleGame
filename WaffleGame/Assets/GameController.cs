using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject worldResult;
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
    }

    private void ShowResultInstruction()
    {
        worldResult.SetActive(true);
    }
}
