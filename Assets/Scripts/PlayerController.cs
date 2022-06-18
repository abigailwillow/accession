using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    private void OnClick() {
        // TODO: Implement raycasting to find the piece that the player is trying to select
        Debug.Log("Clicked");
    }

    private void OnPoint(InputValue value) {
        Debug.Log($"Pointed at {value.Get<Vector2>()}");
    }
}
