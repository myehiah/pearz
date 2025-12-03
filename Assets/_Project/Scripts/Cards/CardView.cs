using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardView : MonoBehaviour
{
    [Header("UI References")]
    public Image frontImage;
    public Image backImage;

    [Header("Flip Settings")]
    [Range(0.15f, 0.75f)]
    public float flipDuration = 0.15f;

    public bool IsFaceUp { get; private set; }

    private Card card;

    void Start()
    {
        frontImage.gameObject.SetActive(false);
        backImage.gameObject.SetActive(true);
        IsFaceUp = false;
    }

    public void SetCard(Card card)
    {
        this.card = card;
    }

    public void Flip()
    {
        StopAllCoroutines();
        StartCoroutine(FlipRoutine(flipDuration));
    }

    public IEnumerator FlipRoutine(float duration)
    {
        float t = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = new Vector3(0f, startScale.y, startScale.z);

        while (t < 1f)
        {
            t += Time.deltaTime / (duration / 2f);
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }
        transform.localScale = endScale;

        IsFaceUp = !IsFaceUp;
        frontImage.gameObject.SetActive(IsFaceUp);
        backImage.gameObject.SetActive(!IsFaceUp);

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / (duration / 2f);
            transform.localScale = Vector3.Lerp(endScale, startScale, t);
            yield return null;
        }
        transform.localScale = startScale;
    }
}
