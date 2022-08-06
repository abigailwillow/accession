using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {
    /// <summary>
    /// The size of this board.
    /// </summary>
    public Vector3 BoardSize { get => new Vector3(prefabCellComponent.size.x * gridSize.x, 0, prefabCellComponent.size.z * gridSize.y); }
    [Tooltip("The amount of rows and columns on the board.")]
    [SerializeField] private Vector2Int gridSize = new Vector2Int(8, 8);
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private ColorTheme colors;
    private List<Cell> cells = new List<Cell>();
    private Piece selectedPiece;
    private Cell prefabCellComponent;

    /// <summary>
    /// Select the given cell.
    /// </summary>
    /// <param name="cell">The cell to select.</param>
    public void OnCellClicked(Cell cell) {
        // Select piece if this cell contains one.
        if (cell.occupied) {
            if (selectedPiece != null) {
                selectedPiece = selectedPiece.Deselect();
                UnhighlightAllCells();
            }
            HighlightCells(GetValidMoves(cell.piece));
            selectedPiece = cell.piece.Select();
        } else {
            // If a piece is selected already, move it to this cell.
            if (selectedPiece != null) {
                // If the list of valid moves contains this cell, then move it and deselect this piece.
                if (GetValidMoves(selectedPiece).Contains(cell)) {
                    selectedPiece.Move(cell);
                    selectedPiece = selectedPiece.Deselect();
                    UnhighlightAllCells();
                }
            }
        }

    }

    /// <summary>
    /// Get all cells that this piece can move to.
    /// </summary>
    /// <param name="piece">The piece to check valid moves for.</param>
    /// <returns>A list of cells that this piece can move to.</returns>
    public List<Cell> GetValidMoves(Piece piece) {
        List<Cell> validCells = new List<Cell>();
        cells.ForEach(cell => {
            Vector2Int difference = cell.coordinates - piece.coordinates;
            Vector2Int absoluteDifference = new Vector2Int(Mathf.Abs(difference.x), Mathf.Abs(difference.y));
            if (difference.y == 1 && absoluteDifference.x == 1 && !cell.occupied) {
                validCells.Add(cell);
            }
        });

        return validCells;
    }

    public void HighlightCells(List<Cell> cells) {
        cells.ForEach(cell => {
            cell.SetHighlight(true);
        });
    }

    public void UnhighlightAllCells() {
        cells.ForEach(cell => {
            cell.SetHighlight(false);
        });
    }

    private void Awake() {
        prefabCellComponent = cellPrefab.GetComponent<Cell>();

        Vector3 cellSize = prefabCellComponent.size;
        Vector3 leftBottomCorner = transform.position - BoardSize / 2;
        Vector3 offset = cellSize / 2;
        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                Vector3 position = new Vector3(leftBottomCorner.x + cellSize.x * x , 0, leftBottomCorner.z + cellSize.z * y) + offset;
                
                Color color = (x + y) % 2 == 1 ? colors.lightCell : colors.darkCell;
                GameObject spawnedCell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                Cell cell = spawnedCell.GetComponent<Cell>().Initialize(new Vector2Int(x, y), color, null);

                // TODO: REMOVE AFTER DEBUGGING!
                if ((x + y) % 7 == 0) {
                    GameObject spawnedPiece = Instantiate(piecePrefab, spawnedCell.transform, false);
                    cell.piece = spawnedPiece.GetComponent<Piece>().Initialize(cell, colors.white);
                }

                cells.Add(cell);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        prefabCellComponent = prefabCellComponent ?? cellPrefab.GetComponent<Cell>();
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, BoardSize);

        cells.ForEach(cell => {
            Gizmos.color = cell.occupied ? Color.red : Color.green;
            Gizmos.DrawWireCube(cell.transform.position, prefabCellComponent.size * 0.99f + Vector3.up * 0.02f);
        });
    }
}
