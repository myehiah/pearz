using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("References")]
    public RectTransform gameBoard;

    public void CalculateGrid(Grid grid, out Vector2 cardSize, out Vector2[,] positions)
    {
        // screen dimensions
        float width = gameBoard.rect.width;
        float height = gameBoard.rect.height;

        int rows = grid.rows;
        int cols = grid.cols;

        // find limiting dimension
        // Todo: add spacing between cards
        float cellWidth = width / cols;
        float cellHeight = height / rows;

        float size = Mathf.Min(cellWidth, cellHeight);
        cardSize = new Vector2(size, size);

        positions = new Vector2[rows, cols];

        // center grid
        float offsetX = (width - (cols * size)) / 2f;
        float offsetY = (height - (rows * size)) / 2f;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                float x = offsetX + (c * size) + size / 2f;
                float y = -(offsetY + (r * size) + size / 2f);

                positions[r, c] = new Vector2(x, y);
            }
        }
    }
}

