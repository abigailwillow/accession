using UnityEngine;

public class Piece : MonoBehaviour {
    public Color defaultColor { get; private set; }
    public Vector2Int coordinates { get; private set; }
    private new Renderer renderer;
    private bool initialized = false;

    public Piece Initialize(Vector2Int coordinates, Color color) {
        // TODO: Initialize everything needed for the piece.

        this.name = $"Piece ({coordinates.x}, {coordinates.y})";
        this.coordinates = coordinates;
        this.defaultColor = color;

        initialized = true;
        return this;
    }

    private void Awake() {
        renderer = GetComponentInChildren<Renderer>();
    }

    private void Start() {
        if (!initialized) {
            Debug.LogError("Piece not initialized!", this);
        }
    }

    public void MoveTo(Cell cell) {
        // TODO: Move the piece to the given cell.
        throw new System.NotImplementedException();
    }

    public void SetColor(Color color) {
        renderer.material.color = color;
    }

    public void ResetColor() {
        renderer.material.color = this.defaultColor;
    }
}
