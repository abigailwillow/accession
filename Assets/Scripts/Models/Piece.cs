using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Piece : MonoBehaviour {
    public Color defaultColor { get; private set; }
    public Vector2Int coordinates { get; private set; }
    private new Renderer renderer;
    private Outline outline;
    private bool initialized = false;

    public Piece Initialize(Vector2Int coordinates, Color color) {
        this.name = $"Piece ({color})";
        this.coordinates = coordinates;
        this.defaultColor = color;

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
    /// <returns>Whether or not the piece was successfully moved.</returns>
    /// <param name="cell">The cell to move to.</param>
    public bool Move(Cell cell) {
        bool moved = cell.MovePieceHere(this);
        this.coordinates = moved ? cell.coordinates : this.coordinates;
        return moved;
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

    public void MoveTo(Cell cell) {
        cell.MovePieceHere(this);
    }

    public void SetColor(Color color) {
        renderer.material.color = color;
    }

    public void ResetColor() {
        renderer.material.color = this.defaultColor;
    }
}
