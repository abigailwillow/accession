using UnityEngine;

public class BoardController : MonoBehaviour {
	[SerializeField] private Vector3 bottomLeftCorner;
    public Vector3 BottomLeftCorner { get => bottomLeftCorner + transform.position; }
	[SerializeField] private Vector3 topRightCorner;
    public Vector3 TopRightCorner { get => topRightCorner + transform.position; }
	public Vector2Int gridSize = new Vector2Int(8, 8);

    private void Awake() {
		Initialize();
	}

    private void Initialize() {
        // TODO: Initialize the board
    }

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawRay(BottomLeftCorner, Vector3.up * 0.1f);
		Gizmos.color = Color.green;
		Gizmos.DrawRay(TopRightCorner, Vector3.up * 0.1f);
	}
}
