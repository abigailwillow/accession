using UnityEngine;

namespace Accession.Models {
    public class Cell {
        public Vector2Int position { get; private set; }
        private Piece _piece;
        /// <summary>
        /// The piece that occupies this cell, or null if empty. Automatically updates the piece's cell.
        /// </summary>
        public Piece piece {
            get => _piece;
            set {
                _piece = value;
                if (value != null && value.cell != this) value.cell = this;
            }
        }

        public ColorType defaultColor { get; private set; }
        /// <summary>
        /// Sets the color of this cell, saving the default color for later.
        /// </summary>
        private ColorType _color;
        public ColorType color {
            get => _color;
            set {
                if (defaultColor == ColorType.None) defaultColor = value;
                _color = value;
            }
        }

        /// <summary>
        /// Whether or not this cell is currently occupied.
        /// </summary>
        public bool occupied => piece != null;

        public Cell(Vector2Int position, Piece piece = null) {
            this.position = position;
            this.piece = piece;
        }
    }
}