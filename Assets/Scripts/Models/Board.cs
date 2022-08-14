using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Accession.Models {
    public class Board {
        public Vector2Int size { get; private set; } = new Vector2Int(8, 8);
        public List<Piece> pieces { get; private set; } = new List<Piece>();
        public List<Cell> cells { get; private set; } = new List<Cell>();
        
        public Board(Vector2Int size, List<Piece> pieces) {
            this.size = size;
            this.pieces = pieces;
        }

        public Board(Vector2Int size) {
            this.size = size;
        }

        /// <summary>
        /// Get all cells that this piece can move to.
        /// </summary>
        /// <param name="piece">The piece to check valid moves for.</param>
        /// <returns>A list of moves that this piece can execute.</returns>
        public List<Move> GetValidMoves(Piece piece) {
            List<Move> moves = new List<Move>();
            this.cells.ForEach(cell => {
                Vector2Int difference = cell.position - piece.position;
                Vector2Int absoluteDifference = new Vector2Int(Mathf.Abs(difference.x), Mathf.Abs(difference.y));

                if (absoluteDifference.x == 1 && difference.y == 1 && !cell.occupied) {
                    moves.Add(new Move(cell, piece));
                }

                Cell target = this.GetCell(piece.position + difference / 2);
                if (absoluteDifference.x == 2 && difference.y == 2 && !cell.occupied && target.occupied) {
                    moves.Add(new Move(cell, piece, target.piece));
                }
            });

            return moves;
        }

        /// <summary>
        /// Get the cell at the given coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates to find the cell for.</param>
        /// <returns>The cell corresponding to the given coordinates.</returns>
        public Cell GetCell(Vector2Int coordinates) => this.cells.Find(cell => cell.position == coordinates);

        /// <summary>
        /// Serializes this board to a string.
        /// </summary>
        /// <returns>This board as a serialized string.</returns>
        public string Serialize() => JsonUtility.ToJson(this, true);

        /// <summary>
        /// Creates a new board from the given file.
        /// </summary>
        /// <param name="path">The path to the file to deserialize.</param>
        /// <returns>A new board object.</returns>
        public static Board Deserialize(string path) => JsonUtility.FromJson<Board>(File.ReadAllText(path));
    }
}