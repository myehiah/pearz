using UnityEngine;

[System.Serializable]
public struct Grid
{
    public int rows;
    public int cols;

    [Header("Spacing")]
    public float horizontalSpacing;
    public float verticalSpacing;

    public Grid(int rows, int cols, float horizontalSpacing = 0, float verticalSpacing = 0)
    {
        this.rows = rows;
        this.cols = cols;
        this.horizontalSpacing = horizontalSpacing;
        this.verticalSpacing = verticalSpacing;
    }

    public readonly int TotalCards => rows * cols;
}

