using UnityEngine;
using Accession.Models;
using Accession.Extensions;

namespace Accession.Controllers {
    [RequireComponent(typeof(Outline))]
    public class PieceController : MonoBehaviour {
        private Piece _piece;
        public Piece piece {
            get => _piece;
            set {
                _piece = value;
                if (value != null && value.controller != this) value.controller = this;
            }
        }
        private Color _color;
        public Color color {
            get => _color;
            set {
                if (renderer != null) renderer.material.color = value;
                _color = value;
            }
        }
        private new Renderer renderer;
        private Outline outline;

        private void Awake() {
            renderer = GetComponentInChildren<Renderer>();
            outline = GetComponentInChildren<Outline>();
        }

        public static PieceController Instantiate(Piece piece, Transform parent, bool instantiateInWorldSpace) {
            PieceController pieceController = Instantiate(Resources.Load<GameObject>("Prefabs/Piece"), parent, instantiateInWorldSpace).GetComponent<PieceController>();
            pieceController.piece = piece;
            pieceController.name = $"Piece ({piece.color})";
            pieceController.color = piece.color.ToColor();
            return pieceController;
        }

        /// <summary>
        /// Move this piece to the given cell.
        /// </summary>
        /// <param name="cell">The cell to move to.</param>
        public void Move(Cell cell) {
            CellController cellController = cell.controller;
            this.piece.cell = cell;
            transform.SetParent(cellController.transform);
            transform.position = cellController.transform.position;
        }

        public void Select() => outline.enabled = true;

        public void Deselect() => outline.enabled = false;
    }
}