using UnityEngine;

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
    public Piece piece { get; private set; }
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
    public new Renderer renderer { get; private set; }

    public void Awake() {
        renderer = GetComponentInChildren<Renderer>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="coordinates">The coordinates of this cell.</param>
    /// <param name="piece">The piece that occupies this cell, or null if empty.</param>
    public void Initialize(Vector2Int coordinates, Piece piece) {
        // TODO: Initialize everything needed for the cell.
        this.coordinates = coordinates;
        this.piece = piece;
    }

    public void SetColor(Color color) {
        renderer.material.color = color;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
