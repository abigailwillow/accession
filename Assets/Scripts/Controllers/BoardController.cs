using UnityEngine;
using UnityEngine.AddressableAssets;
using Accession.Models;
using Accession.Extensions;

namespace Accession.Controllers {
    public class BoardController : MonoBehaviour {
        public static BoardController instance { get; private set; }
        public Board board { get; private set; }
        /// <summary>
        /// The size of this board.
        /// </summary>
        public Vector3 boardSize {
            get {
                cellController ??= GetCellController();
                return new Vector3(cellController.size.x * gridSize.x, 0, cellController.size.z * gridSize.y);
            }
        }
        [Tooltip("The amount of rows and columns on the board.")]
        [SerializeField] private Vector2Int gridSize = new Vector2Int(8, 8);
        public ColorTheme colors;
        private CellController cellController;
        private Piece selectedPiece;

        private void Awake() {
            instance ??= this;
            if (instance != null && instance != this) Destroy(this);

            cellController ??= GetCellController();

            board = new Board(gridSize);

            Vector3 cellSize = cellController.size;
            Vector3 bottomLeft = this.transform.position - boardSize / 2;
            for (int x = 0; x < board.size.x; x++) {
                for (int y = 0; y < board.size.y; y++) {
                    Vector3 position = new Vector3(bottomLeft.x + cellSize.x * x, 0, bottomLeft.z + cellSize.z * y) + cellSize / 2;

                    Cell cell = new Cell(new Vector2Int(x, y), ColorType.None);
                    CellController cellController = CellController.Instantiate(cell, position, this.transform);

                    if ((x + y) % 7 == 0) {
                        // TODO: REMOVE AFTER DEBUGGING!
                        ColorType color = Random.Range(0, 3) switch {
                            0 => ColorType.Red,
                            1 => ColorType.Blue,
                            2 => ColorType.Green,
                            _ => ColorType.None
                        };

                        Piece piece = new Piece(cell, color);
                        PieceController pieceController = PieceController.Instantiate(piece, cellController.transform, false);
                        board.pieces.Add(piece);
                    }

                    board.cells.Add(cell);
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
                    board.cells.ForEach(c => c.controller.SetOutline(false));
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
                    Move move = board.GetValidMoves(selectedPiece).Find(m => m.cell == cell);
                    if (move != null) {
                        move.Execute();
                        DeselectPiece();
                        board.cells.ForEach(c => c.controller.SetOutline(false));
                    }
                }
            }

        }

        private void OnDrawGizmosSelected() {
            cellController ??= this.GetCellController();
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(transform.position, boardSize);

            if (board != null) {
                board.cells.ForEach(cell => {
                    Gizmos.color = cell.occupied ? Color.red : Color.green;
                    Gizmos.DrawWireCube(cell.controller.transform.position, cellController.size * 0.99f + Vector3.up * 0.02f);
                });
            }
        }

        private void SelectPiece(Piece piece) {
            piece.controller.Select();
            selectedPiece = piece;
        }

        private void DeselectPiece() {
            selectedPiece.controller.Deselect();
            selectedPiece = null;
        }

        private CellController GetCellController() {
            if (Application.isPlaying) {
                return Addressables.LoadAssetAsync<GameObject>("Prefabs/Cell").WaitForCompletion().GetComponent<CellController>();
            } else {
                return UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Cell.prefab").GetComponent<CellController>();
            }
        }
    }
}