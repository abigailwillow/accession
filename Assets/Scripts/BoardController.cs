using System;
using UnityEngine;

public class BoardController : MonoBehaviour {
	public Corners corners;
	public Grid grid = new Grid { width = 8, height = 8 };

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
		Gizmos.DrawRay(corners.bottomLeft, Vector3.up * 0.1f);
		Gizmos.color = Color.green;
        Gizmos.DrawRay(corners.topRight, Vector3.up * 0.1f);
	}

    [Serializable]
    public struct Corners {
        public Vector3 bottomLeft;
        public Vector3 topRight;
    }

    [Serializable]
    public struct Grid {
        public int width;
        public int height;
    }
}
