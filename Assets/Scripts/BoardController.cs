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
    private Transform[,] grid;
    private Piece selectedPiece;
    private Cell prefabCellComponent;

    /// <summary>
    /// Select the given cell.
    /// </summary>
    /// <param name="cell">The cell to select.</param>
    public void OnCellClicked(Cell cell) {
        // Select the given cell or its piece, depending on if the cell is occupied or not.
        if (cell.occupied) {
            if (selectedPiece != null) {
                selectedPiece = selectedPiece.Deselect();
            }
            selectedPiece = cell.piece.Select();
            return;
        }

        // If there is a selected piece, try to move it.
        if (selectedPiece != null) {
            MovePiece(selectedPiece, cell);
        }
    }

    public void MovePiece(Piece piece, Cell cell) {
        Vector2Int coordinateDifference = cell.coordinates - piece.coordinates;
        Vector2Int absoluteDifference = new Vector2Int(Mathf.Abs(coordinateDifference.x), Mathf.Abs(coordinateDifference.y));
        if (coordinateDifference.y > 0 && absoluteDifference.x == 1 && absoluteDifference.x == absoluteDifference.y) {
            if (piece.Move(cell)) {
                selectedPiece = selectedPiece.Deselect();
            }
        }
    }

    private void Awake() {
        prefabCellComponent = cellPrefab.GetComponent<Cell>();

        grid = new Transform[gridSize.x, gridSize.y];
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
                    cell.piece = spawnedPiece.GetComponent<Piece>().Initialize(new Vector2Int(x, y), colors.white);
                }

                grid[x, y] = spawnedCell.transform;
            }
        }
    }

    private void OnDrawGizmosSelected() {
        prefabCellComponent = prefabCellComponent ?? cellPrefab.GetComponent<Cell>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, BoardSize);

        if (grid != null) {
            for (int x = 0; x < grid.GetLength(0); x++) {
                for (int y = 0; y < grid.GetLength(1); y++) {
                    Gizmos.DrawWireCube(grid[x, y].position, new Vector3(0.1f, 0.02f, 0.1f));
                }
            }
        }
    }
}
