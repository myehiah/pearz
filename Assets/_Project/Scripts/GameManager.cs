using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
        UpdateUI();
    }

    public void SetTotalPairs(int pairs)
    {
        totalPairs = pairs;
        matchedPairs = 0;
        score = 0;
        UpdateUI();
    }

    public void OnPairMatched()
    {
        AudioManager.Instance.PlaySFX(SFX.Match);

        matchedPairs++;
        combo++;
        score += pointsPerMatch * combo;
        UpdateUI();

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
    }

    private void UpdateUI()
    {
        if (uiController != null)
        {
            uiController.SetScore(score);
            uiController.SetCombo(combo);
            uiController.SetProgress(matchedPairs, totalPairs);
        }
    }

    private void OnWin()
    {
        if (uiController != null) uiController.ShowWin();
        AudioManager.Instance.PlaySFX(SFX.Win);
    }

    private void ResetState()
    {
        matchedPairs = 0;
        combo = 0;
        score = 0;
        UpdateUI();
    }

    public void RestartGame()
    {
        ResetState();
        comparisonEngine.ResetEngine();
        deckManager.ResetBoard();

        UpdateUI();
    }

    public void LevelSelect()
    {
        ResetState();
        comparisonEngine.ResetEngine();
        deckManager.ClearBoard();

        SetTotalPairs(0);

        UpdateUI();
    }

    public Grid GetGrid(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy: return easyGrid;
            case Difficulty.Medium: return mediumGrid;
            case Difficulty.Hard: return hardGrid;
        }
        return mediumGrid;
    }

    public void StartGame(Difficulty difficulty)
    {
        ResetState();

        Grid gridLevel = GetGrid(difficulty);

        deckManager.currentGrid = gridLevel;
        deckManager.GenerateBoard();

        SetTotalPairs(gridLevel.TotalCards / 2);

        uiController?.StartGame();
    }
}