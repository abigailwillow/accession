using UnityEngine;

public class Piece : MonoBehaviour {
    public Color color { get; private set; }
    public Vector2Int coordinates { get; private set; }

    public void Initialize(Vector2Int coordinates, Color color) {
        // TODO: Initialize everything needed for the piece.
        this.name = $"Piece ({coordinates.x}, {coordinates.y})";
        this.coordinates = coordinates;
        this.color = color;
    }

    public void MoveTo(Vector2Int coordinates) {
        // TODO: Move the piece to the given coordinates.
        throw new System.NotImplementedException();
    }

    public void SetColor(Color color) {
        GetComponentInChildren<Renderer>().material.color = color;
    }
}
