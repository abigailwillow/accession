using UnityEngine;
using UnityEngine.AddressableAssets;
using Accession.Extensions;
using Accession.Models;

namespace Accession.Controllers  {
    [RequireComponent(typeof(Outline))]
    public class CellController : MonoBehaviour {
        private Cell _cell;
        public Cell cell { get; private set; }
        [Tooltip("The physical dimensions of this piece."), SerializeField]
        private Vector2 _size;
        /// <summary>
        /// The size of this cell.
        /// </summary>
        public Vector3 size => _size.XZ();
        /// <summary>
        /// Whether or not this cell contains a correctly placed piece.
        /// </summary>
        public bool completed => throw new System.NotImplementedException();
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

        public void Awake() {
            renderer = GetComponentInChildren<Renderer>();
            outline = GetComponent<Outline>();

            this.name = $"Cell ({cell.position.x}, {cell.position.y})";
        }

        public static CellController Instantiate(Cell cell, Transform parent) => Instantiate(cell, Vector3.zero, parent);

        public static CellController Instantiate(Cell cell, Vector3 position, Transform parent) => Instantiate(cell, position, Quaternion.identity, parent);

        public static CellController Instantiate(Cell cell, Vector3 position, Quaternion rotation, Transform parent) {
            CellController cellController = Addressables.InstantiateAsync("Prefabs/Cell", position, rotation, parent).Result.GetComponent<CellController>();
            cellController.cell = cell;
            return cellController;
        }

        public void SetOutline(bool enabled) => outline.enabled = enabled;
        
        public void SetOutline(bool enabled, Color color) {
            outline.OutlineColor = color;
            outline.enabled = enabled;
        }

        // public void ResetColor() => renderer.material.color = this.cell.defaultColor;

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, size);
        }
    }
}