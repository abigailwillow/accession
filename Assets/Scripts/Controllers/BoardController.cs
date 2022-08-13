using System.Collections.Generic;
using UnityEngine;
using Accession.Models;
using Accession.Extensions;

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
        public ColorTheme colors;
        public List<Cell> cells { get; private set; } = new List<Cell>();
        private Piece selectedPiece;
        private CellController prefabCellComponent;

        private void Awake() {
            instance ??= this;
            if (instance != null && instance != this) Destroy(this);

            prefabCellComponent = cellPrefab.GetComponent<CellController>();

            board = new Board(gridSize);

            Vector3 cellSize = prefabCellComponent.size;
            Vector3 leftBottomCorner = transform.position - boardSize / 2;
            for (int x = 0; x < board.size.x; x++) {
                for (int y = 0; y < board.size.y; y++) {
                    Vector3 position = new Vector3(leftBottomCorner.x + cellSize.x * x, 0, leftBottomCorner.z + cellSize.z * y) + cellSize / 2;

                    Cell cell = new Cell(new Vector2Int(x, y), ColorType.None);
                    CellController cellController = CellController.Instantiate(cell, position, transform);

                    if ((x + y) % 7 == 0) {
                        // TODO: REMOVE AFTER DEBUGGING!
                        ColorType color = Random.Range(0, 3) switch {
                            0 => ColorType.Red,
                            1 => ColorType.Blue,
                            2 => ColorType.Green,
                            _ => ColorType.None
                        };

                        Piece piece = new Piece(cell, color);
                        PieceController pieceController = PieceController.Instantiate(piece, cellController.transform);
                        board.pieces.Add(pieceController);
                    }

                    cells.Add(cell);
                }
            }
        }

        /// <summary>
        /// Select the given cell.
        /// </summary>
        /// <param name="cellController">The cell to select.</param>
        public void OnCellClicked(CellController cellController) {
            Cell cell = cellController.cell;
            // Select piece if this cell contains one.
            if (cell.occupied) {
                if (selectedPiece != null) {
                    DeselectPiece();
                    cells.ForEach(c => c.controller.SetOutline(false));
                }

                board.GetValidMoves(cell.piece).ForEach(move => {
                    Color color = move.isJump ? move.instigator.color.Add(move.target.color).ToColor() : colors.validMove;
                    move.cell.controller.SetOutline(true, color);
                });

                SelectPiece(cellController.cell.piece);
            } else {
                // If a piece is selected already, move it to this cell.
                if (selectedPiece != null) {
                    // If the list of valid moves contains this cell, then move it and deselect this piece.
                    Move move = board.GetValidMoves(selectedPiece).Find(m => m.cell == cellController);
                    if (move != null) {
                        move.Execute();
                        move.instigator.color = move.isJump ? move.instigator.color.Add(move.target.color) : move.instigator.color;
                        DeselectPiece();
                        cells.ForEach(c => c.controller.SetOutline(false));
                    }
                }
            }

        }

        public Cell GetCell(Vector2Int coordinates) => cells.Find(cell => cell.position == coordinates);

        private void OnDrawGizmosSelected() {
            prefabCellComponent = prefabCellComponent ?? cellPrefab.GetComponent<CellController>();
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(transform.position, boardSize);

            cells.ForEach(cell => {
                Gizmos.color = cell.occupied ? Color.red : Color.green;
                Gizmos.DrawWireCube(cell.controller.transform.position, prefabCellComponent.size * 0.99f + Vector3.up * 0.02f);
            });
        }

        private void SelectPiece(Piece piece) {
            DeselectPiece();
            piece.controller.Select();
            selectedPiece = piece;
        }

        private void DeselectPiece() {
            selectedPiece.Deselect();
            selectedPiece = null;
        }
    }
}