using UnityEngine;
using UnityEngine.AddressableAssets;
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

        public Color color {
            get => renderer.material.color;
            set => renderer.material.color = value;
        }
        private new Renderer renderer;
        private Outline outline;

        private void Awake() {
            renderer = GetComponentInChildren<Renderer>();
            outline = GetComponentInChildren<Outline>();
        }

        public static PieceController Instantiate(Piece piece, Transform parent, bool instantiateInWorldSpace) {
            PieceController pieceController = Addressables.InstantiateAsync("Prefabs/Piece", parent, instantiateInWorldSpace).WaitForCompletion().GetComponent<PieceController>();
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