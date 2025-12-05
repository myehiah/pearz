using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX
{
    Flip,
    Match,
    Mismatch,
    Win
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Source")]
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip flipClip;
    public AudioClip matchClip;
    public AudioClip mismatchClip;
    public AudioClip winClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlaySFX(SFX type)
    {
        if (sfxSource == null) return;

        switch (type)
        {
            case SFX.Flip:
                if (flipClip) sfxSource.PlayOneShot(flipClip);
                break;

            case SFX.Match:
                if (matchClip) sfxSource.PlayOneShot(matchClip);
                break;

            case SFX.Mismatch:
                if (mismatchClip) sfxSource.PlayOneShot(mismatchClip);
                break;

            case SFX.Win:
                if (winClip) sfxSource.PlayOneShot(winClip);
                break;
        }
    }
}

