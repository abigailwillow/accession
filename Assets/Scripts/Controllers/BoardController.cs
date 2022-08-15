using UnityEngine;
using Accession.Models;
using Accession.Extensions;

namespace Accession.Controllers {
    public class BoardController : MonoBehaviour {
        public static BoardController instance { get; private set; }
        public Board board { get; private set; }
        /// <summary>
        /// The physical size of this board.
        /// </summary>
        public Vector3 size {
            get {
                cellController ??= GetCellController();
                return new Vector3(cellController.size.x * board.size.x, 0, cellController.size.z * board.size.y);
            }
        }
        [Tooltip("The amount of rows and columns on the board.")]
        public ColorTheme colors;
        private CellController cellController;
        private Piece selectedPiece;

        private void Awake() {
            instance ??= this;
            if (instance != null && instance != this) Destroy(this);

            cellController ??= GetCellController();

            this.LoadBoard("Levels/Sample");
        }

        /// <summary>
        /// Select the given cell.
        /// </summary>
        /// <param name="cellController">The cell to select.</param>
        public void OnCellClicked(CellController cellController) {
            Cell cell = cellController.cell;
            if (cell.occupied) {
                if (selectedPiece != null) {
                    selectedPiece = null;
                    board.cells.ForEach(c => c.controller.SetOutline(false));
                }

                board.GetValidMoves(cell.piece).ForEach(move => {
                    Color color = move.isJump ? move.instigator.color.Add(move.target.color).ToColor() : colors.validMove;
                    move.cell.controller.SetOutline(true, color);
                });

                SelectPiece(cellController.cell.piece);
            } else {
                if (selectedPiece != null) {
                    Move move = board.GetValidMoves(selectedPiece).Find(m => m.cell == cell);
                    if (move != null) {
                        move.Execute();
                        selectedPiece = null;
                        board.cells.ForEach(c => c.controller.SetOutline(false));

                        if (board.completed) Debug.Log("Board completed");
                    }
                }
            }
        }

        /// <summary>
        /// Load a json file containing the board data, and convert initialize this board using the data.
        /// </summary>
        /// <param name="path">A path to a json file containing board data.</param>
        public void LoadBoard(string path) {
            board = Board.Read(path);
            Vector3 cellSize = cellController.size;
            Vector3 bottomLeft = this.transform.position - this.size / 2;

            board.cells.ForEach(cell => {
                Vector3 position = new Vector3(bottomLeft.x + cellSize.x * cell.position.x, 0, bottomLeft.z + cellSize.z * cell.position.y) + cellSize / 2;
                CellController.Instantiate(cell, position, this.transform);

                Piece piece = board.pieces.Find(piece => piece.position == cell.position);
                if (piece != null) {
                    PieceController.Instantiate(piece, cell.controller.transform, false);
                }
            });
        }

    private void SelectPiece(Piece piece) {
            piece.controller.Select();
            selectedPiece = piece;
        }

        private CellController GetCellController() => Resources.Load<GameObject>("Prefabs/Cell").GetComponent<CellController>();

        private void OnDrawGizmosSelected() {
            if (board != null) {
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube(transform.position, size);

                board.cells.ForEach(cell => {
                    Gizmos.color = cell.occupied ? cell.piece.color.ToColor() : Color.white;
                    Gizmos.DrawWireCube(cell.controller.transform.position, cellController.size * 0.9f + Vector3.up * 0.02f);
                });
            }
        }
    }
}