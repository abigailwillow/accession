using UnityEngine;

public static class VectorExtension {
    /// <summary>
    /// Casts this Vector2 into a Vector3 where Y is Z.
    /// </summary>
    /// <param name="vector">The Vector2 to convert into a Vector3.</param>
    /// <returns>A Vector3 using X/Z coordinates, Y is left empty.</returns>
    public static Vector3 XZ(this Vector2 vector) => new Vector3(vector.x, 0f, vector.y);
    /// <summary>
    /// Casts this Vector2Int into a Vector3 where Y is Z.
    /// </summary>
    /// <param name="vector">The Vector2Int to convert into a Vector3Int.</param>
    /// <returns>A Vector3Int using X/Z coordinates, Y is left empty.</returns>
    public static Vector3Int XZ(this Vector2Int vector) => new Vector3Int(vector.x, 0, vector.y);
}