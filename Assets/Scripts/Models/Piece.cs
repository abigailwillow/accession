using UnityEngine;

namespace Accession.Models {
    [RequireComponent(typeof(Outline))]
    public class Piece : MonoBehaviour {
        public Color color {
            get => renderer.material.color;
            set => renderer.material.color = value;
        }
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
        public Vector2Int position => cell.position;
        private new Renderer renderer;
        private Outline outline;
        private bool initialized = false;

        public Piece Initialize(Cell cell, Color color) {
            this.name = $"Piece ({color})";
            this.cell = cell;
            this.color = color;

            initialized = true;
            return this;
        }

        private void Awake() {
            renderer = GetComponentInChildren<Renderer>();
            outline = GetComponentInChildren<Outline>();
        }

        private void Start() {
            if (!initialized) {
                Debug.LogError("Piece not initialized!", this);
            }
        }

        /// <summary>
        /// Move this piece to the given cell.
        /// </summary>
        /// <param name="cell">The cell to move to.</param>
        public void Move(Cell cell) {
            this.cell = cell;
            transform.SetParent(cell.transform);
            transform.position = cell.transform.position;
        }

        /// <summary>
        /// Selects this piece and returns a reference to itself.
        /// </summary>
        /// <returns>The selected piece.</returns>
        public Piece Select() {
            outline.enabled = true;
            return this;
        }

        /// <summary>
        /// Deselects this piece and returns null.
        /// </summary>
        /// <returns>Null.</returns>
        public Piece Deselect() {
            outline.enabled = false;
            return null;
        }

        public void ResetColor() {
            renderer.material.color = this.color;
        }
    }
}