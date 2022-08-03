using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour {
    private PlayerInput input;
    new private Camera camera;

    private void Awake() {
        input = GetComponent<PlayerInput>();
        camera = GetComponent<Camera>();
    }

    private void OnClick() {
        Vector2 pointerPosition = input.actions.FindAction("Point").ReadValue<Vector2>();
        Ray ray = camera.ScreenPointToRay(pointerPosition);
        Physics.Raycast(ray, out RaycastHit hit);
        if (hit.collider != null) {
            hit.transform.GetComponentInParent<BoardController>().TrySelect(hit.transform.gameObject);
        }
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 0.25f);
    }
}
