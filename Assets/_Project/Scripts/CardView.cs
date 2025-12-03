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

    void Start()
    {
        frontImage.gameObject.SetActive(false);
        backImage.gameObject.SetActive(true);
        IsFaceUp = false;
    }

    public void Flip()
    {
        StopAllCoroutines();
        StartCoroutine(FlipRoutine());
    }

    private IEnumerator FlipRoutine()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime / flipDuration)
        {
            float scaleX = Mathf.Lerp(1f, 0f, t);
            transform.localScale = new Vector3(scaleX, 1f, 1f);
            yield return null;
        }

        IsFaceUp = !IsFaceUp;
        frontImage.gameObject.SetActive(IsFaceUp);
        backImage.gameObject.SetActive(!IsFaceUp);

        for (float t = 0; t < 1f; t += Time.deltaTime / flipDuration)
        {
            float scaleX = Mathf.Lerp(0f, 1f, t);
            transform.localScale = new Vector3(scaleX, 1f, 1f);
            yield return null;
        }
    }
}
