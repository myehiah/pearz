using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CardView : MonoBehaviour
{
    [Header("UI References")]
    public Image frontImage;
    public Image backImage;
    public CanvasGroup canvasGroup;

    [SerializeField]
    [Header("Flip Settings")]
    [Range(0.15f, 0.75f)]
    private float flipDuration = 0.15f;
    public float FlipDuration => flipDuration;

    public Card Card { get; private set; }

    public event Action<CardView> CardClicked;

    private bool isFlipping = false;

    void Start()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetCard(Card card)
    {
        Card = card;
        frontImage.sprite = card.sprite;
        frontImage.gameObject.SetActive(Card.isFaceUp);
        backImage.gameObject.SetActive(!Card.isFaceUp);
    }

    public void OnClick()
    {
        if (Card == null) return;
        if (Card.isLocked) return;
        if (Card.isMatched) return;
        if (isFlipping) return;

        CardClicked?.Invoke(this);
    }

    public void Show()
    {
        if (Card == null) return;
        if (Card.isFaceUp) return;

        Card.FlipUp();
        StartCoroutine(FlipRoutine(true));
        AudioManager.Instance.PlaySFX(SFX.Flip);
    }

    public void Hide()
    {
        if (Card == null) return;
        if (!Card.isFaceUp) return;

        Card.FlipDown();
        StartCoroutine(FlipRoutine(false));
    }

    public void SetMatched()
    {
        if (Card == null) return;
        Card.MarkMatched();

        if (canvasGroup != null)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        // todo: add animation for matching
    }

    private IEnumerator FlipRoutine(bool showFace)
    {
        isFlipping = true;

        float t = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = new Vector3(0f, startScale.y, startScale.z);

        while (t < 1f)
        {
            t += Time.deltaTime / (flipDuration / 2f);
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }
        transform.localScale = endScale;

        frontImage.gameObject.SetActive(showFace);
        backImage.gameObject.SetActive(!showFace);

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / (flipDuration / 2f);
            transform.localScale = Vector3.Lerp(endScale, startScale, t);
            yield return null;
        }
        transform.localScale = startScale;

        isFlipping = false;
    }
}
