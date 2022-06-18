using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour {
    private PlayerInput input;

    private void Awake() {
        input = GetComponent<PlayerInput>();
    }

    private void OnClick() {
        // TODO: Implement raycasting to find the piece that the player is trying to select
        Debug.Log($"Clicked {input.actions.FindAction("Point").ReadValue<Vector2>()}");
    }
}
