using UnityEngine;
using Accession.Controllers;

namespace Accession.Models {
    public class Piece {
        private PieceController _controller;
        public PieceController controller {
            get => _controller;
            set {
                _controller = value;
                if (value != null && value.piece != this) value.piece = this;
            } 
        }
        public ColorType color { get; private set; }
        /// <summary>
        /// This piece's position on the board in top-down 2D space.
        /// </summary>
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

        public Piece(Cell cell, ColorType color) {
            this.cell = cell;
            this.color = color;
        }
    }
}