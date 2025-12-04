using UnityEngine;

[System.Serializable]
public struct Grid
{
    public int rows;
    public int cols;

    [Header("Spacing")]
    public float horizontalSpacing;
    public float verticalSpacing;

    public Grid(int rows, int cols, float horizontalSpacing = 5, float verticalSpacing = 5)
    {
        this.rows = rows;
        this.cols = cols;
        this.horizontalSpacing = horizontalSpacing;
        this.verticalSpacing = verticalSpacing;
    }

    public readonly int TotalCards => rows * cols;
}

