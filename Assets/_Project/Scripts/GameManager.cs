using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    public DeckManager deckManager;
    public ComparisonEngine comparisonEngine;
    public UIController uiController;

    [Header("Scoring")]
    public int pointsPerMatch = 100;
    public int penaltyPerMismatch = 50;

    [Header("Levels")]
    public Grid easyGrid = new Grid { rows = 2, cols = 2 };
    public Grid mediumGrid = new Grid { rows = 3, cols = 4 };
    public Grid hardGrid = new Grid { rows = 4, cols = 4 };

    [Header("State (read-only)")]
    public int totalPairs = 0;
    public int matchedPairs = 0;
    public int score = 0;
    public int combo = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        uiController.UpdateContinueButton();
    }

    //start
    public void StartGame(Difficulty difficulty)
    {
        Grid gridLevel = GetGrid(difficulty);

        deckManager.currentGrid = gridLevel;
        deckManager.GenerateBoard();

        ResetState();
        UpdateUI();
        uiController?.StartGame();

        SaveProgress();
    }

    //restart & level select
    public void RestartGame()
    {
        ResetState();
        comparisonEngine.ResetEngine();
        deckManager.ResetBoard();

        UpdateUI();

        SaveProgress();
    }

    public void LevelSelect()
    {
        ResetState();
        comparisonEngine.ResetEngine();
        deckManager.ClearBoard();

        UpdateUI();

        SaveSystem.DeleteSave();

        uiController.UpdateContinueButton();
    }

    //win
    private void OnWin()
    {
        if (uiController != null) uiController.ShowWin();
        AudioManager.Instance.PlaySFX(SFX.Win);

        SaveSystem.DeleteSave();
    }

    //save
    public void SaveProgress() => SaveManager.Instance.Save(this, deckManager);

    //load
    public void Load()
    {
        SaveManager.Instance.LoadSave(this, deckManager);
        UpdateUI();
        uiController?.StartGame();

    }

    //matching logic
    public void OnPairMatched()
    {
        AudioManager.Instance.PlaySFX(SFX.Match);

        matchedPairs++;
        combo++;
        score += pointsPerMatch * combo;
        UpdateUI();

        SaveProgress();

        if (matchedPairs >= totalPairs && totalPairs > 0)
        {
            OnWin();
        }
    }

    public void OnPairMismatch()
    {
        AudioManager.Instance.PlaySFX(SFX.Mismatch);

        combo = 0;

        if (penaltyPerMismatch != 0)
        {
            score = Mathf.Max(0, score - penaltyPerMismatch); //max to prevent negative score
            UpdateUI();
        }

        SaveProgress();
    }

    //helpers
    private void ResetState()
    {
        totalPairs = deckManager.currentGrid.TotalCards / 2;
        matchedPairs = 0;
        combo = 0;
        score = 0;

        SaveSystem.DeleteSave();

        UpdateUI();
    }

    private void UpdateUI()
    {
        uiController?.SetScore(score);
        uiController?.SetCombo(combo);
        uiController?.SetProgress(matchedPairs, totalPairs);
    }

    private Grid GetGrid(Difficulty difficulty)
    {
        return difficulty switch
        {
            Difficulty.Easy => easyGrid,
            Difficulty.Medium => mediumGrid,
            Difficulty.Hard => hardGrid,
            _ => mediumGrid
        };
    }
}