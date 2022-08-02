using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {
    [Tooltip("The coordinates of the bottom left corner of the board in local space.")]
    [SerializeField] private Vector2 bottomLeftCorner;
    /// <summary>
    /// The coordinates of the bottom left corner of the board in local space.
    /// </summary>
    public Vector3 BottomLeftCorner { get => bottomLeftCorner.XZ() + transform.position; }
    [Tooltip("The coordinates of the top right corner of the board in local space.")]
    [SerializeField] private Vector2 topRightCorner;
    /// <summary>
    /// The coordinates of the top right corner of the board in local space.
    /// </summary>
    public Vector3 TopRightCorner { get => topRightCorner.XZ() + transform.position; }
    [Tooltip("The amount of rows and columns on the board.")]
    [SerializeField] private Vector2Int gridSize = new Vector2Int(8, 8);
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private ColorTheme colors;
    private Transform[,] grid;
    private Piece selectedPiece;

    public void SelectPiece(Piece piece) {
        if (selectedPiece != null) {
            selectedPiece.Deselect();
        }
        selectedPiece = piece;
        piece.Select();
    }

    private void Awake() {
        grid = new Transform[gridSize.x, gridSize.y];
        Vector3 boardSize = TopRightCorner - BottomLeftCorner;
        Vector3 cellSize = new Vector3(boardSize.x / gridSize.x, 0, boardSize.z / gridSize.y);
        Vector3 offset = cellSize / 2;
        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                Vector3 position = new Vector3(BottomLeftCorner.x + cellSize.x * x, 0, BottomLeftCorner.z + cellSize.z * y) + offset;
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                GameObject piece = Instantiate(piecePrefab, cell.transform, false);
                cell.name = $"Cell ({x}, {y})";
                piece.name = $"Piece ({x}, {y})";
                grid[x, y] = cell.transform;

                if ((x + y) % 2 == 0) {
                    cell.GetComponent<Cell>().renderer.material.color = colors.lightCell;
                } else {
                    cell.GetComponent<Cell>().renderer.material.color = colors.darkCell;
                }
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Vector3 boardSize = TopRightCorner - BottomLeftCorner;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boardSize);

        if (grid != null) {
            for (int x = 0; x < grid.GetLength(0); x++) {
                for (int y = 0; y < grid.GetLength(1); y++) {
                    Gizmos.DrawWireCube(grid[x, y].position, new Vector3(0.1f, 0.02f, 0.1f));
                }
            }
        }
    }
}
