using UnityEngine;

public class Piece : MonoBehaviour {
    public Piece Select() {
        GetComponentInChildren<Renderer>().material.color = Color.red;
        return this;
    }

    public Piece Deselect() {
        GetComponentInChildren<Renderer>().material.color = Color.white;
        return null;
    }
}
