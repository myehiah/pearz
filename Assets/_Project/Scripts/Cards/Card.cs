using System;

[Serializable]
public class Card
{
    public int id;
    public string faceId;

    public Card(int id, string faceId)
    {
        this.id = id;
        this.faceId = faceId;
    }
}