[System.Serializable]
public struct Grid
{
    public int rows;
    public int cols;

    public Grid(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
    }

    public readonly int TotalCards => rows * cols;
}

