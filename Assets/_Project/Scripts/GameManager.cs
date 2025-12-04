using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Scoring")]
    public int pointsPerMatch = 100;
    public int penaltyPerMismatch = 50;

    [Header("State (read-only)")]
    public int totalPairs = 0;
    public int matchedPairs = 0;
    public int score = 0;

    public UIController uiController;

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
        matchedPairs++;
        score += pointsPerMatch;
        UpdateUI();

        if (matchedPairs >= totalPairs && totalPairs > 0)
        {
            OnWin();
        }
    }

    public void OnPairMismatch()
    {
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
            uiController.SetProgress(matchedPairs, totalPairs);
        }
    }

    private void OnWin()
    {
        Debug.Log("GameManager: YOU WIN!");
        if (uiController != null) uiController.ShowWin();
        AudioManager.Instance.PlaySFX(SFX.Win);
    }

    private void ResetState()
    {
        matchedPairs = 0;
        score = 0;
        UpdateUI();
    }

    public void RestartGame()
    {
        ResetState();
        FindObjectOfType<ComparisonEngine>().ResetEngine();
        FindObjectOfType<DeckManager>().ResetBoard();

        UpdateUI();
    }
}

