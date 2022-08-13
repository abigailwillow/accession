using UnityEngine;
using UnityEngine.AddressableAssets;
using Accession.Models;

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

            this.name = $"Piece ({color})";
        }

        public static PieceController Instantiate(Piece piece, Transform parent) => Instantiate(piece, Vector3.zero, parent);

        public static PieceController Instantiate(Piece piece, Vector3 position, Transform parent) => Instantiate(piece, position, Quaternion.identity, parent);

        public static PieceController Instantiate(Piece piece, Vector3 position, Quaternion rotation, Transform parent) {
            PieceController pieceController = Addressables.InstantiateAsync("Prefabs/Piece", position, rotation, parent).Result.GetComponent<PieceController>();
            pieceController.piece = piece;
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