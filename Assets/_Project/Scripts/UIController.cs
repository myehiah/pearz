using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Screen References")]
    public GameObject startScreen;
    public GameObject scoreScreen;
    public GameObject winScreen;

    [Header("Scoring References")]
    public Text scoreText;
    public Text comboText;
    public Text progressText;

    [Header("StartScreen References")]
    public Button continueButton;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    [Header("WinScreen References")]
    public Button restartButton;
    public Button levelSelectButton;

    private void Awake()
    {
        ShowStartMenu();
        HideScore();
        HideWin();

        SetupButtonListeners();
    }

    public void SetScore(int score) =>
    scoreText.text = $"Score: {score}";

    public void SetCombo(int combo) =>
        comboText.text = combo > 1 ? $"Combo x{combo}" : "";

    public void SetProgress(int matched, int total) =>
        progressText.text = $"{matched} / {total} pairs";


    public void ShowStartMenu() => startScreen.SetActive(true);
    public void HideStartMenu() => startScreen.SetActive(false);
    public void ShowScore() => scoreScreen.SetActive(true);
    public void HideScore() => scoreScreen.SetActive(false);
    public void ShowWin() => winScreen.SetActive(true);
    public void HideWin() => winScreen.SetActive(false);
    public void ShowContinue() => continueButton.gameObject.SetActive(true);
    public void HideContinue() => continueButton.gameObject.SetActive(false);

    public void UpdateContinueButton()
    {
        if (SaveSystem.SaveExists())
            ShowContinue();
        else
            HideContinue();
    }

    public void StartGame()
    {
        HideStartMenu();
        HideWin();
        ShowScore();
    }

    private void OnRestartClicked()
    {
        HideWin();
        GameManager.Instance.RestartGame();
    }

    private void OnLevelSelectClicked()
    {
        HideWin();
        HideScore();
        GameManager.Instance.LevelSelect();
        ShowStartMenu();
    }

    private void OnContinueClicked()
    {
        HideStartMenu();
        GameManager.Instance.Load();
    }

    private void SetupButtonListeners()
    {
        continueButton.onClick.AddListener(OnContinueClicked);

        easyButton.onClick.AddListener(() => GameManager.Instance.StartGame(Difficulty.Easy));
        mediumButton.onClick.AddListener(() => GameManager.Instance.StartGame(Difficulty.Medium));
        hardButton.onClick.AddListener(() => GameManager.Instance.StartGame(Difficulty.Hard));

        restartButton.onClick.AddListener(OnRestartClicked);
        levelSelectButton.onClick.AddListener(OnLevelSelectClicked);
    }
}