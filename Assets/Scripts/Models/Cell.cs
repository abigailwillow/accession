using UnityEngine;

public class Cell : MonoBehaviour {
    /// <summary>
    /// The piece that is currently occupying this cell, null if empty.
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

    public void Initialize() {
        // TODO: Initialize everything needed for the cell.
    }

    public void SetColor(Color color) {
        renderer.material.color = color;
    }
}
