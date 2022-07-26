using UnityEngine;

public class Piece : MonoBehaviour {
    public Color color { get; private set; }

    public void Initialize() {
        // TODO: Initialize everything needed for the piece.
    }

     public void Select() {
        GetComponentInChildren<Renderer>().material.color = Color.red;
    }

    public void Deselect() {
        GetComponentInChildren<Renderer>().material.color = Color.white;
    }
}
