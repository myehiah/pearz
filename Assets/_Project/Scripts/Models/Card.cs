using System;
using UnityEngine;

[Serializable]
public class Card
{
    public int id;
    public string faceId;
    public Sprite sprite;

    public bool isMatched;
    public bool isFaceUp;
    public bool isLocked;

    public Card(int id, string faceId)
    {
        this.id = id;
        this.faceId = faceId;

        isMatched = false;
        isFaceUp = false;
        isLocked = false;
    }

    public void MarkMatched() => isMatched = true;
    public void FlipUp() => isFaceUp = true;
    public void FlipDown() => isFaceUp = false;
    public void Lock() => isLocked = true;
    public void Unlock() => isLocked = false;
}