using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Toggle autoUnloadToggle;

    [SerializeField] private bool isGamePaused = false;

    [SerializeField] private Image fadeCanvas;

    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject uiCanvas;


    public TMP_Text largeBoxAmountText;
    public TMP_Text mediumBoxAmountText;
    public TMP_Text squareBoxAmountText;

    private void Start()
    {
        autoUnloadToggle.onValueChanged.AddListener(HandleAutoUnloadChanged);
    }

/*
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            TogglePause();
        }
    }*/

    public void StartGame()
    {
        UpdateBoxesAmount(GameManager.Instance.largeBoxAmount, GameManager.Instance.mediumBoxAmount, GameManager.Instance.squareBoxAmount);
        GameManager.Instance.StartGame();

        FadeInToGame(true);

        startMenu.SetActive(false);
        uiCanvas.SetActive(true);


    }

    public void UpdateBoxesAmount(int largeBox, int mediumBox, int squareBox)
    {
        largeBoxAmountText.text = largeBox.ToString();
        mediumBoxAmountText.text = mediumBox.ToString();
        squareBoxAmountText.text = squareBox.ToString();
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;

        FadeInToGame(!isGamePaused);

        pauseMenu.SetActive(isGamePaused);
        uiCanvas.SetActive(!isGamePaused);

        GameManager.Instance.PauseGame(isGamePaused);

    }

    public void OpenSettingsMenu(bool open)
    {
            startMenu.SetActive(!open);
            settingsMenu.SetActive(open);
    }

    private void FadeInToGame(bool fadeIn)
    {
        float alpha = fadeIn ? 0 : 0.5f;
        fadeCanvas.DOFade(alpha, 2);

    }

    private void HandleAutoUnloadChanged(bool isToggleOn)
    {
        BoxSpawner.Instance.AutoSpawnOn(isToggleOn);
    }
}
