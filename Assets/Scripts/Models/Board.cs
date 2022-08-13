using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Accession.Controllers;

namespace Accession.Models {
    public class Board {
        public Vector2Int size { get; private set; } = new Vector2Int(8, 8);
        public List<PieceController> pieces { get; private set; } = new List<PieceController>();
        
        public Board(Vector2Int size, List<PieceController> pieces) {
            this.size = size;
            this.pieces = pieces;
        }

        public Board(Vector2Int size) {
            this.size = size;
        }

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