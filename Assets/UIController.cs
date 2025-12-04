using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("UI References")]
    public Text scoreText;
    public Text progressText;
    public GameObject winScreen;

    private void Awake()
    {
        if (winScreen != null) winScreen.SetActive(false);
    }

    public void SetScore(int value)
    {
        if (scoreText != null)
            scoreText.text = $"Score: {value}";
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

}

