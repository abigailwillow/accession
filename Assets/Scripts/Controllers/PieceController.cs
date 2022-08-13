using UnityEngine;
using Accession.Models;

namespace Accession.Controllers {
    [RequireComponent(typeof(Outline))]
    public class PieceController : MonoBehaviour {
        public Piece piece { get; private set; }
        public Color color {
            get => renderer.material.color;
            set => renderer.material.color = value;
        }
        private new Renderer renderer;
        private Outline outline;

        private void Awake() {
            renderer = GetComponentInChildren<Renderer>();
            outline = GetComponentInChildren<Outline>();

            this.name = $"Piece ({color})";
        }

        /// <summary>
        /// Move this piece to the given cell.
        /// </summary>
        /// <param name="cellController">The cell to move to.</param>
        public void Move(CellController cellController) {
            this.piece.cell = cellController.cell;
            transform.SetParent(cellController.transform);
            transform.position = cellController.transform.position;
        }

        public void Select() => outline.enabled = true;
        
        public void Deselect() => outline.enabled = false;
    }
}