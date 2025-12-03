using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("References")]
    public RectTransform gameBoard;

    public void CalculateGrid(Grid grid, out Vector2 cardSize, out Vector2[,] positions)
    {
        // screen and grid dimensions
        float width = gameBoard.rect.width;
        float height = gameBoard.rect.height;

        int rows = grid.rows;
        int cols = grid.cols;

        float horizontalSpacing = grid.horizontalSpacing;
        float verticalSpacing = grid.verticalSpacing;

        // find limiting dimension
        float cellWidth = width / cols;
        float cellHeight = height / rows;

        float sizeX = cellWidth - horizontalSpacing;
        float sizeY = cellHeight - verticalSpacing;
        float size = Mathf.Min(sizeX, sizeY);

        cardSize = new Vector2(size, size);

        // position and center grid
        positions = new Vector2[rows, cols];

        float cardsWidth = cols * size;
        float gapsWidth = (cols - 1) * horizontalSpacing;
        float cardsHeight = rows * size;
        float gapsHeight = (rows - 1) * verticalSpacing;

        float offsetX = (width - (cardsWidth + gapsWidth)) / 2f;
        float offsetY = (height - (cardsHeight + gapsHeight)) / 2f;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                float x = offsetX + (c * (size + horizontalSpacing)) + size / 2f;
                float y = -(offsetY + (r * (size + verticalSpacing)) + size / 2f);

                positions[r, c] = new Vector2(x, y);
            }
        }
    }
}

