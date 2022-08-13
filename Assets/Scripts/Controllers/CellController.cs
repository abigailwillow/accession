using UnityEngine;
using Accession.Extensions;

namespace Accession.Controllers {
    // TODO: Separate Cell model from CelController class.
    [RequireComponent(typeof(Outline))]
    public class CellController : MonoBehaviour {
        [Tooltip("The size of this piece."), SerializeField]
        private Vector2 _size;
        /// <summary>
        /// The size of this cell.
        /// </summary>
        public Vector3 size => _size.XZ();
        private PieceController _piece;
        /// <summary>
        /// The piece that occupies this cell, or null if empty. Automatically updates the piece's cell.
        /// </summary>
        public PieceController piece {
            get => _piece;
            set {
                _piece = value;
                if (value != null && value.cell != this) value.cell = this;
            }
        }
        /// <summary>
        /// The coordinates of this cell on the board.
        /// </summary>
        public Vector2Int position { get; private set; }
        /// <summary>
        /// Whether or not this cell is currently occupied.
        /// </summary>
        public bool occupied => piece != null;
        /// <summary>
        /// Whether or not this cell contains a correctly placed piece.
        /// </summary>
        public bool completed => piece.color == renderer.material.color;
        private Color _defaultColor;
        /// <summary>
        /// Sets the color of this cell, saving the default color for later.
        /// </summary>
        public Color color {
            get => renderer.material.color;
            set {
                if (_defaultColor == null) _defaultColor = value;
                renderer.material.color = value;
            }
        }
        private Color _defaultOutlineColor;
        private Color _outlineColor;
        /// <summary>
        /// Sets the color of this cell's outline, saving the default color for later.
        /// </summary>
        public Color outlineColor {
            get => outline.OutlineColor;
            set {
                if (_defaultOutlineColor == null) _defaultOutlineColor = value;
                outline.OutlineColor = value;
            }
        }
        private new Renderer renderer;
        private Outline outline;
        private bool initialized = false;

        public void Awake() {
            renderer = GetComponentInChildren<Renderer>();
            outline = GetComponent<Outline>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinates">The coordinates of this cell.</param>
        /// <param name="piece">The piece that occupies this cell, or null if empty.</param>
        public CellController Initialize(Vector2Int coordinates, Color color) {
            this.name = $"Cell ({coordinates.x}, {coordinates.y})";
            this.position = coordinates;
            this.color = color;

            initialized = true;
            return this;
        }

        public void SetOutline(bool enabled) => outline.enabled = enabled;
        public void SetOutline(bool enabled, Color color) {
            outline.OutlineColor = color;
            outline.enabled = enabled;
        }

        public void ResetColor() => renderer.material.color = this._defaultColor;

        private void Start() {
            if (!initialized) {
                Debug.LogError("Cell not initialized!", this);
            }
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, size);
        }
    }
}