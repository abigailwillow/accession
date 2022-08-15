using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using UnityEngine;
using Accession.Converters;

namespace Accession.Models {
    [JsonConverter(typeof(BoardConverter))]
    public class Board {
        public Vector2Int size { get; private set; } = new Vector2Int(8, 8);
        public List<Cell> cells { get; private set; } = new List<Cell>();
        public List<Piece> pieces { get; private set; } = new List<Piece>();
        public bool completed => cells.TrueForAll(cell => cell.completed);
        
        public Board(Vector2Int size, List<Cell> cells, List<Piece> pieces) {
            this.size = size;
            this.cells = cells;
            this.pieces = pieces;
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
        public string Serialize() => JsonSerializer.Serialize(this);

        /// <summary>
        /// Creates a new board from the given json string.
        /// </summary>
        /// <param name="json">The json string to deserialize from.</param>
        /// <returns>A new board object created from the json string.</returns>
        public static Board Deserialize(string json) => JsonSerializer.Deserialize<Board>(json);

        /// <summary>
        /// Write this board to a file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void Write(string path) => File.WriteAllText(path, this.Serialize());

        /// <summary>
        /// Read a board from a file.
        /// </summary>
        /// <param name="path">The file to read from.</param>
        /// <returns>A new board object created from the file.</returns>
        public static Board Read(string path) => Board.Deserialize(Resources.Load<TextAsset>(path).text);
    }
}