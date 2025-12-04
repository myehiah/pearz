using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComparisonEngine : MonoBehaviour
{
    [Header("Timings")]
    public float postFlipBuffer = 0.02f;
    public float mismatchRevealDuration = 0.6f;

    private readonly Queue<CardView> selectedCards = new Queue<CardView>();
    private readonly HashSet<int> selectedIds = new HashSet<int>();
    private Coroutine checkerCoroutine = null;

    public void RegisterCard(CardView cardView)
    {
        if (cardView == null) return;
        cardView.CardClicked += OnCardClicked;
    }

    private void OnCardClicked(CardView view)
    {
        if (view == null || view.Card == null) return;
        if (view.Card.isMatched) return;
        if (view.Card.isLocked) return;
        if (selectedIds.Contains(view.Card.id)) return;

        view.Show();

        selectedIds.Add(view.Card.id);
        selectedCards.Enqueue(view);

        if (checkerCoroutine == null)
            checkerCoroutine = StartCoroutine(CheckSelectedCards());
    }

    private IEnumerator CheckSelectedCards()
    {
        while (selectedCards.Count >= 2)
        {
            CardView first = selectedCards.Dequeue();
            CardView second = selectedCards.Dequeue();

            selectedIds.Remove(first.Card.id);
            selectedIds.Remove(second.Card.id);

            if (first == null || second == null) continue;

            first.Card.Lock();
            second.Card.Lock();

            float firstDuration = first.FlipDuration;
            float secondDuration = second.FlipDuration;
            float flipDelay = Mathf.Max(firstDuration, secondDuration) + postFlipBuffer;
            yield return new WaitForSeconds(flipDelay);

            bool isMatch = first.Card.faceId == second.Card.faceId;

            if (isMatch)
            {
                AudioManager.Instance.PlaySFX(SFX.Match);

                first.SetMatched();
                second.SetMatched();

                GameManager.Instance.OnPairMatched();
            }
            else
            {
                AudioManager.Instance.PlaySFX(SFX.Mismatch);

                GameManager.Instance.OnPairMismatch();

                yield return new WaitForSeconds(mismatchRevealDuration);

                first.Hide();
                second.Hide();

                yield return new WaitForSeconds(flipDelay);

                first.Card.Unlock();
                second.Card.Unlock();
            }

        }

        checkerCoroutine = null;
    }

    public void ResetEngine()
    {
        ClearSelectedCards();
    }

    private void ClearSelectedCards()
    {
        selectedCards.Clear();
        selectedIds.Clear();
        if (checkerCoroutine != null)
        {
            StopCoroutine(checkerCoroutine);
            checkerCoroutine = null;
        }
    }
}
