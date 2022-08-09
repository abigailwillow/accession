using System;
using UnityEngine;

[Obsolete("LegacyBoardController is obsolete. Use BoardController instead.")]
public class LegacyBoardController : MonoBehaviour {
    // [Tooltip("The coordinates of the bottom left corner of the board in local space.")]
    // [SerializeField] private Vector3 bottomLeftCorner;
    // /// <summary>
    // /// The coordinates of the bottom left corner of the board in local space.
    // /// </summary>
    // public Vector3 BottomLeftCorner { get => bottomLeftCorner + transform.position; }
    // [Tooltip("The coordinates of the top right corner of the board in local space.")]
    // [SerializeField] private Vector3 topRightCorner;
    // /// <summary>
    // /// The coordinates of the top right corner of the board in local space.
    // /// </summary>
    // public Vector3 TopRightCorner { get => topRightCorner + transform.position; }
    // [Tooltip("The amount of rows and columns on the board.")]
    // [SerializeField] private Vector2Int gridSize = new Vector2Int(8, 8);
    // [SerializeField] private GameObject piecePrefab;
    // [SerializeField] private GameObject cellPrefab;
    // private Transform[,] grid;
    // private Piece selectedPiece;

    // public void SelectPiece(Piece piece) {
    //     if (selectedPiece != null) {
    //         selectedPiece.Deselect();
    //     }
    //     selectedPiece = piece;
    //     piece.Select();
    // }

    // private void Awake() {
    //     grid = new Transform[gridSize.x, gridSize.y];
    //     Vector3 distance = TopRightCorner - BottomLeftCorner;
    //     Vector3 cellSize = new Vector3(distance.x / gridSize.x, 0, distance.z / gridSize.y);
    //     Vector3 cellOffset = cellSize / 2;
    //     for (int x = 0; x < gridSize.x; x++) {
    //         for (int y = 0; y < gridSize.y; y++) {
    //             Vector3 position = new Vector3(bottomLeftCorner.x + cellSize.x * x, 0, bottomLeftCorner.z + cellSize.z * y) + cellOffset;
    //             GameObject cell = new GameObject($"Cell ({x}, {y})");
    //             cell.transform.position = position;
    //             cell.transform.SetParent(transform);
    //             Instantiate(piecePrefab, cell.transform, false);
    //             grid[x, y] = cell.transform;
    //         }
    //     }
    // }

    // private void OnDrawGizmosSelected() {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawRay(BottomLeftCorner, Vector3.up * 0.1f);
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawRay(TopRightCorner, Vector3.up * 0.1f);

    //     if (grid != null) {
    //         Gizmos.color = Color.blue;
    //         for (int x = 0; x < grid.GetLength(0); x++) {
    //             for (int y = 0; y < grid.GetLength(1); y++) {
    //                 Gizmos.DrawRay(grid[x, y].position, Vector3.up * 0.05f);
    //             }
    //         }
    //     }
    // }
}
