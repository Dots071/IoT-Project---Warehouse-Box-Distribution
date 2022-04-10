using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{

    public int largeBoxAmount = 75;
    public int mediumBoxAmount = 15;
    public int squareBoxAmount = 10;

    [SerializeField] GameObject boxSpawner;

    public void StartGame()
    {
        boxSpawner.SetActive(true);
    }

    public void ReStartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0 : 1.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
