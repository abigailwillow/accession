using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CallerMemberNameAttribute))]
public class PlayerController : MonoBehaviour {
    private PlayerInput input;
    new private Camera camera;
    private Piece selectedPiece;

    private void Awake() {
        input = GetComponent<PlayerInput>();
        camera = GetComponent<Camera>();
    }

    private void OnClick() {
        Vector2 pointerPosition = input.actions.FindAction("Point").ReadValue<Vector2>();
        Ray ray = camera.ScreenPointToRay(pointerPosition);
        Physics.Raycast(ray, out RaycastHit hit);
        if (hit.collider != null && hit.collider.TryGetComponent(out Piece piece)) {
            if (selectedPiece == null) {
                selectedPiece = piece.Select();
            } else if (selectedPiece == piece) {
                selectedPiece = selectedPiece.Deselect();
            } else {
                selectedPiece.Deselect();
                selectedPiece = piece.Select();
            }
        } else {
            if (selectedPiece != null) {
                selectedPiece = selectedPiece.Deselect();
            }
        }
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 1f);
    }
}
