using UnityEngine;

namespace Accession.Models {
    public class Piece {
        public ColorType color { get; private set; }
        public Vector2Int position => cell.position;
        private Cell _cell;
        /// <summary>
        /// Set the cell that this piece belongs to. Automatically updates the cells' pieces.
        /// </summary>
        public Cell cell {             
            get => _cell;
            set {
                if (_cell != null) _cell.piece = null;
                _cell = value;
                if (value != null && value.piece != this) value.piece = this;
            } 
        }

        public Piece(ColorType color, Cell cell) {
            this.color = color;
            this.cell = cell;
        }
    }
}