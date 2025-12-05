using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Save(GameManager gm, DeckManager dm)
    {
        SaveData data = BuildSaveData(gm, dm);
        SaveSystem.Save(data);
    }

    private SaveData BuildSaveData(GameManager gm, DeckManager dm)
    {
        SaveData data = new SaveData();
        data.grid = dm.currentGrid;

        data.score = gm.score;
        data.currentCombo = gm.combo;

        data.cards = new List<CardSaveData>();

        foreach (var card in dm.cards)
        {
            data.cards.Add(new CardSaveData
            {
                id = card.id,
                faceId = card.faceId,
                spriteName = card.sprite.name,
                isMatched = card.isMatched
            });
        }
        return data;
    }

    public void LoadSave(GameManager gm, DeckManager dm)
    {
        SaveData data = SaveSystem.Load();
        if (data == null) return;

        gm.score = data.score;
        gm.combo = data.currentCombo;
        gm.matchedPairs = data.cards.Count(c => c.isMatched) / 2;
        gm.totalPairs = data.cards.Count / 2;

        dm.currentGrid = data.grid;
        dm.RestoreCards(data.cards);
    }
}

