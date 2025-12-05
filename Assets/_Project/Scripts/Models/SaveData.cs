using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public Grid grid;
    public int score;
    public int currentCombo;
    public List<CardSaveData> cards;

}

[System.Serializable]
public class CardSaveData
{
    public int id;
    public string faceId;
    public string spriteName;
    public bool isMatched;
}
