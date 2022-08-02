using UnityEngine;

public class BoardController : MonoBehaviour {
    /// <summary>
    /// The size of this board.
    /// </summary>
    public Vector3 BoardSize { get => new Vector3(cell.Size.x * gridSize.x, 0, cell.Size.z * gridSize.y); }
    [Tooltip("The amount of rows and columns on the board.")]
    [SerializeField] private Vector2Int gridSize = new Vector2Int(8, 8);
    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private ColorTheme colors;
    private Transform[,] grid;
    private Piece selectedPiece;
    private Cell cell;

    public void SelectPiece(Piece piece) {
        if (selectedPiece != null) {
            selectedPiece.Deselect();
        }
        selectedPiece = piece;
        piece.Select();
    }

    private void Awake() {
        cell = cellPrefab.GetComponent<Cell>();

        grid = new Transform[gridSize.x, gridSize.y];
        Vector3 cellSize = cell.Size;
        Vector3 leftBottomCorner = transform.position - BoardSize / 2;
        Vector3 offset = cellSize / 2;
        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                Vector3 position = new Vector3(leftBottomCorner.x + cellSize.x * x , 0, leftBottomCorner.z + cellSize.z * y) + offset;
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                GameObject piece = Instantiate(piecePrefab, cell.transform, false);
                cell.name = $"Cell ({x}, {y})";
                piece.name = $"Piece ({x}, {y})";
                grid[x, y] = cell.transform;

                cell.GetComponent<Cell>().renderer.material.color = (x + y) % 2 == 1 ? colors.lightCell : colors.darkCell;
            }
        }
    }

    private void OnDrawGizmosSelected() {
        cell = cell ?? cellPrefab.GetComponent<Cell>();
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
