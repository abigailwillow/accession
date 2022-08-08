using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardController : MonoBehaviour {
    /// <summary>
    /// The size of this board.
    /// </summary>
    public Vector3 boardSize => new Vector3(prefabCellComponent.size.x * gridSize.x, 0, prefabCellComponent.size.z * gridSize.y);
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
                cells.ForEach(c => c.SetOutline(false));
            }

            GetValidMoves(cell.piece).ForEach(move => {
                // Unity for some dumb reason doesn't clamp the color's components to 0..1, so lerp them halfway instead. 
                Color color = move.isJump ? Color.Lerp(move.instigator.color, move.target.color, 0.5f) : colors.validMove;
                move.cell.SetOutline(true, color);
                Debug.Log($"Valid move: {move.cell.position} (color: {color})");
            });

            selectedPiece = cell.piece.Select();
        } else {
            // If a piece is selected already, move it to this cell.
            if (selectedPiece != null) {
                // If the list of valid moves contains this cell, then move it and deselect this piece.
                if (GetValidMoves(selectedPiece).Where(move => move.cell == cell).Any()) {
                    selectedPiece.Move(cell);
                    selectedPiece = selectedPiece.Deselect();
                    cells.ForEach(c => c.SetOutline(false));
                }
            }
        }

    }

    /// <summary>
    /// Get all cells that this piece can move to.
    /// </summary>
    /// <param name="piece">The piece to check valid moves for.</param>
    /// <returns>A list of moves that this piece can execute.</returns>
    public List<Move> GetValidMoves(Piece piece) {
        List<Move> moves = new List<Move>();
        cells.ForEach(cell => {
            Vector2Int difference = cell.position - piece.position;
            Vector2Int absoluteDifference = new Vector2Int(Mathf.Abs(difference.x), Mathf.Abs(difference.y));

            if (absoluteDifference.x == 1 && difference.y == 1 && !cell.occupied) {
                moves.Add(new Move(cell, piece));
            }

            Cell target = GetCell(piece.position + difference / 2);
            if (absoluteDifference.x == 2 && difference.y == 2 && !cell.occupied && target.occupied) {
                moves.Add(new Move(cell, piece, target.piece));
            }
        });

        return moves;
    }

    public Cell GetCell(Vector2Int coordinates) => cells.Find(cell => cell.position == coordinates);

    private void Awake() {
        prefabCellComponent = cellPrefab.GetComponent<Cell>();

        Vector3 cellSize = prefabCellComponent.size;
        Vector3 leftBottomCorner = transform.position - boardSize / 2;
        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                Vector3 position = new Vector3(leftBottomCorner.x + cellSize.x * x , 0, leftBottomCorner.z + cellSize.z * y) + cellSize / 2;
                
                Color color = (x + y) % 2 == 0 ? colors.cell.dark : colors.cell.light;
                GameObject spawnedCell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                Cell cell = spawnedCell.GetComponent<Cell>().Initialize(new Vector2Int(x, y), color);

                // TODO: REMOVE AFTER DEBUGGING!
                if ((x + y) % 7 == 0) {
                    GameObject spawnedPiece = Instantiate(piecePrefab, spawnedCell.transform, false);
                    cell.piece = spawnedPiece.GetComponent<Piece>().Initialize(cell, Random.Range(0f, 1f) > 0.5f ? colors.piece.red : colors.piece.blue);
                }

                cells.Add(cell);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        prefabCellComponent = prefabCellComponent ?? cellPrefab.GetComponent<Cell>();
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, boardSize);

        cells.ForEach(cell => {
            Gizmos.color = cell.occupied ? Color.red : Color.green;
            Gizmos.DrawWireCube(cell.transform.position, prefabCellComponent.size * 0.99f + Vector3.up * 0.02f);
        });
    }
}
