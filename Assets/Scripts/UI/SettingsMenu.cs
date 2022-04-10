using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField largeBoxInput;
    [SerializeField] TMP_InputField mediumBoxInput;
    [SerializeField] TMP_InputField squareBoxInput;

    [SerializeField] Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(HandleBackButtonPressed);
    }

    private void HandleBackButtonPressed()
    {
        GameManager.Instance.largeBoxAmount = int.Parse(largeBoxInput.text);
        GameManager.Instance.mediumBoxAmount = int.Parse(mediumBoxInput.text);
        GameManager.Instance.squareBoxAmount = int.Parse(squareBoxInput.text);

        UIManager.Instance.OpenSettingsMenu(false);
    }
}
