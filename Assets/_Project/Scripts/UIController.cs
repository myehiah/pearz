using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("UI References")]
    public Text scoreText;
    public Text comboText;
    public Text progressText;
    public GameObject scoreScreen;
    public GameObject winScreen;
    public GameObject startScreen;
    public Button restartButton;
    public Button levelSelectButton;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    private void Awake()
    {
        HideScore();
        HideWin();
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(OnRestartClicked);
        }

        if (levelSelectButton != null)
        {
            levelSelectButton.onClick.RemoveAllListeners();
            levelSelectButton.onClick.AddListener(OnLevelSelectClicked);
        }

        easyButton.onClick.AddListener(() => GameManager.Instance.StartGame(Difficulty.Easy));
        mediumButton.onClick.AddListener(() => GameManager.Instance.StartGame(Difficulty.Medium));
        hardButton.onClick.AddListener(() => GameManager.Instance.StartGame(Difficulty.Hard));
    }

    public void SetScore(int score)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }

    public void SetCombo(int combo)
    {
        if (comboText != null)
            comboText.text = combo > 1 ? "Combo x" + combo : "";
    }

    public void SetProgress(int matched, int totalPairs)
    {
        if (progressText != null)
            progressText.text = $"{matched} / {totalPairs} pairs";
    }

    public void ShowWin()
    {
        if (winScreen != null) winScreen.SetActive(true);
    }

    public void HideWin()
    {
        if (winScreen != null) winScreen.SetActive(false);
    }

    public void ShowScore()
    {
        if (scoreScreen != null) scoreScreen.SetActive(true);
    }

    public void HideScore()
    {
        if (scoreScreen != null) scoreScreen.SetActive(false);
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

    public void ShowStartMenu()
    {
        if (startScreen != null)
            startScreen.SetActive(true);
    }

    public void HideStartMenu()
    {
        if (startScreen != null)
            startScreen.SetActive(false);
    }

    public void StartGame()
    {
        HideStartMenu();
        HideWin();
        ShowScore();
    }
}