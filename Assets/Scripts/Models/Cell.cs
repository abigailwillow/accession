using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Cell : MonoBehaviour {
    [Tooltip("The size of this piece.")]
    [SerializeField] private Vector2 _size;
    /// <summary>
    /// The size of this cell.
    /// </summary>
    public Vector3 size { get => _size.XZ(); }
    /// <summary>
    /// The piece that occupies this cell, or null if empty.
    /// </summary>
    public Piece piece;
    /// <summary>
    /// The coordinates of this cell on the board.
    /// </summary>
    public Vector2Int coordinates { get; private set; }
    /// <summary>
    /// Whether or not this cell is currently occupied.
    /// </summary>
    public bool occupied => piece != null;
    /// <summary>
    /// Whether or not this cell contains a correctly placed piece.
    /// </summary>
    public bool completed => piece.color == renderer.material.color;
    public Color defaultColor { get; private set; }
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
    public Cell Initialize(Vector2Int coordinates, Color color, Piece piece) {
        this.name = $"Cell ({coordinates.x}, {coordinates.y})";
        this.coordinates = coordinates;
        this.defaultColor = color;
        SetColor(color);
        this.piece = piece;

        initialized = true;
        return this;
    }

    public void Highlight() {
        outline.enabled = true;
    }

    public void SetColor(Color color) {
        renderer.material.color = color;
    }

    public void ResetColor() {
        renderer.material.color = this.defaultColor;
    }

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
