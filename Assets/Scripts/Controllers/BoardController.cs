using System.Collections.Generic;
using UnityEngine;
using Accession.Models;

namespace Accession.Controllers {
    public class BoardController : MonoBehaviour {
        public static BoardController instance { get; private set; }
        public Board board { get; private set; }
        /// <summary>
        /// The size of this board.
        /// </summary>
        public Vector3 boardSize => new Vector3(prefabCellComponent.size.x * board.size.x, 0, prefabCellComponent.size.z * board.size.y);
        [Tooltip("The amount of rows and columns on the board.")]
        [SerializeField] private Vector2Int gridSize = new Vector2Int(8, 8);
        [SerializeField] private GameObject piecePrefab;
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private ColorTheme colors;
        private List<Cell> cells = new List<Cell>();
        private Piece selectedPiece;
        private Cell prefabCellComponent;

        private void Awake() {
            instance ??= this;
            if (instance != null && instance != this) Destroy(this);

            prefabCellComponent = cellPrefab.GetComponent<Cell>();

            board = new Board(gridSize);

            Vector3 cellSize = prefabCellComponent.size;
            Vector3 leftBottomCorner = transform.position - boardSize / 2;
            for (int x = 0; x < board.size.x; x++) {
                for (int y = 0; y < board.size.y; y++) {
                    Vector3 position = new Vector3(leftBottomCorner.x + cellSize.x * x, 0, leftBottomCorner.z + cellSize.z * y) + cellSize / 2;

                    Color cellColor = (x + y) % 2 == 0 ? colors.cell.dark : colors.cell.light;
                    GameObject spawnedCell = Instantiate(cellPrefab, position, Quaternion.identity, transform);
                    Cell cell = spawnedCell.GetComponent<Cell>().Initialize(new Vector2Int(x, y), cellColor);

                    // TODO: REMOVE AFTER DEBUGGING!
                    Color pieceColor = Random.Range(0, 3) switch {
                        0 => colors.piece.red,
                        1 => colors.piece.blue,
                        2 => colors.piece.green,
                        _ => Color.white
                    };

                    if ((x + y) % 7 == 0) {
                        GameObject spawnedPiece = Instantiate(piecePrefab, spawnedCell.transform, false);
                        Piece piece = spawnedPiece.GetComponent<Piece>().Initialize(cell, pieceColor);
                        cell.piece = piece;
                        board.pieces.Add(piece);
                    }

                    cells.Add(cell);
                }
            }
        }

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
                    Color color = move.isJump ? colors.piece.Add(move.instigator.color, move.target.color) : colors.validMove;
                    move.cell.SetOutline(true, color);
                });

                selectedPiece = cell.piece.Select();
            } else {
                // If a piece is selected already, move it to this cell.
                if (selectedPiece != null) {
                    // If the list of valid moves contains this cell, then move it and deselect this piece.
                    Move move = GetValidMoves(selectedPiece).Find(m => m.cell == cell);
                    if (move != null) {
                        move.Execute();
                        move.instigator.color = move.isJump ? colors.piece.Add(move.instigator.color, move.target.color) : move.instigator.color;
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
}