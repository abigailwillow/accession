using UnityEngine;

public class Piece : MonoBehaviour {
    public void Select() {
        GetComponentInChildren<Renderer>().material.color = Color.red;
    }

    public void Deselect() {
        GetComponentInChildren<Renderer>().material.color = Color.white;
    }
}
