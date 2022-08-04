using UnityEngine;

public class Piece : MonoBehaviour {
    public Color defaultColor { get; private set; }
    public Vector2Int coordinates { get; private set; }
    private new Renderer renderer;
    private Outline outline;
    private bool initialized = false;

    public Piece Initialize(Vector2Int coordinates, Color color) {
        // TODO: Initialize everything needed for the piece.

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
