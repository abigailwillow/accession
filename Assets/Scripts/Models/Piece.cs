using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Piece : MonoBehaviour {
    public Color color { get; private set; }
    private Cell _cell;
    /// <summary>
    /// Set the cell that this piece belongs to. Automatically updates the cell's piece.
    /// </summary>
    public Cell cell { 
        get => _cell;
        set {
            if (_cell.piece != this) _cell.piece = this;
            _cell = value;
        }
    }
    public Vector2Int coordinates => cell.coordinates;
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

    public void SetColor(Color color) {
        renderer.material.color = color;
    }

    public void ResetColor() {
        renderer.material.color = this.color;
    }
}
