using UnityEngine;

[CreateAssetMenu(fileName = "Color Theme", menuName = "Accession/Color Theme", order = 0)]
public class ColorTheme : ScriptableObject {
    [System.Serializable]
    public struct Piece {
        public UnityEngine.Color red;
        public UnityEngine.Color green;
        public UnityEngine.Color blue;
        public UnityEngine.Color magenta;
        public UnityEngine.Color yellow;
        public UnityEngine.Color cyan;

        /// <summary>
        /// Converts a PieceColor to a Color.
        /// </summary>
        /// <param name="color">The PieceColor</param>
        /// <returns></returns>
        public UnityEngine.Color GetColor(Piece.Color color) => color switch {
            Piece.Color.Red => red,
            Piece.Color.Green => green,
            Piece.Color.Blue => blue,
            Piece.Color.Magenta => magenta,
            Piece.Color.Yellow => yellow,
            Piece.Color.Cyan => cyan,
            _ => UnityEngine.Color.white
        };

        /// <summary>
        /// Statically adds the two colors together and returns the result as a color.
        /// </summary>
        /// <param name="a">The instigating piece color.</param>
        /// <param name="b">The target piece color.</param>
        /// <returns>The combined color.</returns>
        public UnityEngine.Color Add(Piece.Color a, Piece.Color b) => (a, b) switch {
            (Piece.Color.Red, Piece.Color.Red) => red,
            (Piece.Color.Red, Piece.Color.Green) => yellow,
            (Piece.Color.Red, Piece.Color.Blue) => magenta,
            (Piece.Color.Green, Piece.Color.Red) => yellow,
            (Piece.Color.Green, Piece.Color.Green) => green,
            (Piece.Color.Green, Piece.Color.Blue) => cyan,
            (Piece.Color.Blue, Piece.Color.Red) => magenta,
            (Piece.Color.Blue, Piece.Color.Green) => cyan,
            (Piece.Color.Blue, Piece.Color.Blue) => blue,
            _ => UnityEngine.Color.white
        };

        public enum Color {
            Red,
            Green,
            Blue,
            Magenta,
            Yellow,
            Cyan
        }
    }

    [System.Serializable]
    public struct Cell {
        public Color light;
        public Color dark;
    }

    public Piece piece;
    public Cell cell;

    public Color validMove;
}