using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [Header("References")]
    public CardView cardPrefab;
    public GridManager gridManager;
    public RectTransform gameBoard;
    public ComparisonEngine comparisonEngine;

    [Header("Current State")]
    public List<Card> cards = new List<Card>();
    public List<CardView> cardViews = new List<CardView>();

    public Grid currentGrid = new Grid(2, 2);

    public void GenerateBoard(bool clearAndShuffle = true)
    {
        ClearBoard(clearAndShuffle);

        if (cards == null || cards.Count == 0)
        {
            int numberOfCards = currentGrid.TotalCards;
            cards = CreateCardList(numberOfCards);
        }

        gridManager.CalculateGrid(currentGrid, out Vector2 cardSize, out Vector2[,] positions);

        int index = 0;
        for (int r = 0; r < currentGrid.rows; r++)
        {
            for (int c = 0; c < currentGrid.cols; c++)
            {
                Card card = cards[index];

                CardView view = Instantiate(cardPrefab, gameBoard);
                RectTransform rt = view.GetComponent<RectTransform>();
                rt.sizeDelta = cardSize;
                rt.anchoredPosition = positions[r, c];

                view.name = card.faceId;
                view.SetCard(card);
                comparisonEngine.RegisterCard(view);

                cardViews.Add(view);
                index++;
            }
        }
    }

    private List<Card> CreateCardList(int total)
    {
        List<Card> cardList = new List<Card>();

        int numberOfPairs = total / 2;
        int idCounter = 0;

        for (int i = 0; i < numberOfPairs; i++)
        {
            cardList.Add(new Card(idCounter++, $"Face_{i}"));
            cardList.Add(new Card(idCounter++, $"Face_{i}"));
        }

        // Fisher-Yates shuffle
        for (int i = 0; i < cardList.Count; i++)
        {
            int rand = Random.Range(i, cardList.Count);
            (cardList[i], cardList[rand]) = (cardList[rand], cardList[i]);
        }

        return cardList;
    }

    public void ClearBoard(bool includeCardModels = true)
    {
        foreach (var view in cardViews)
            if (view != null) Destroy(view.gameObject);

        cardViews.Clear();

        if(includeCardModels)
            cards.Clear();
    }

    public void ResetBoard()
    {
        GenerateBoard();
    }

    public void RestoreCards(List<CardSaveData> savedCards)
    {
        List<Card> restoredCardList = new List<Card>();

        foreach (var savedCard in savedCards)
        {
            var restoredCard = new Card(savedCard.id, savedCard.faceId);
            restoredCard.isMatched = savedCard.isMatched;
            restoredCard.isLocked = savedCard.isMatched;
            restoredCard.isFaceUp = savedCard.isMatched;
            restoredCardList.Add(restoredCard);
        }

        cards = restoredCardList;

        GenerateBoard(clearAndShuffle: false);
    }

}

