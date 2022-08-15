using UnityEngine;
using Accession.Controllers;

namespace Accession.Models {
    public class Cell {
        private CellController _controller;
        public CellController controller {
            get => _controller;
            set {
                _controller = value;
                if (value != null && value.cell != this) value.cell = this;
            } 
        }
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
        
        public Vector2Int position { get; private set; }

        /// <summary>
        /// Sets the color of this cell.
        /// </summary>
        public ColorType color;

        /// <summary>
        /// Whether or not this cell is currently occupied.
        /// </summary>
        public bool occupied => piece != null;

        /// <summary>
        /// Whether or not this cell contains a correctly placed piece.
        /// </summary>
        public bool completed => (occupied && this.color == piece.color) || (!occupied && this.color == ColorType.None);

        /// <summary>
        /// Whether this cell is dark or light.
        /// </summary>
        public bool dark => (position.x + position.y) % 2 == 0;

        public Cell(Vector2Int position, ColorType color) {
            this.controller = controller;
            this.position = position;
            this.color = color;
        }
    }
}