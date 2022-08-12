using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Accession.Models {
    public class Board {
        public Vector2Int size { get; private set; }
        public List<Piece> pieces { get; private set; } = new List<Piece>();

        
        public Board(Vector2Int size, List<Piece> pieces = null) {
            this.size = size; 
            this.pieces = pieces;        
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