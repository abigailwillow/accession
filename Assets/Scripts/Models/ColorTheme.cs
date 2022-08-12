using UnityEngine;

namespace Accession.Models {
    [CreateAssetMenu(fileName = "Color Theme", menuName = "Accession/Color Theme", order = 0)]
    public class ColorTheme : ScriptableObject {
        [System.Serializable]
        public struct Piece {
            public Color red;
            public Color green;
            public Color blue;
            public Color magenta;
            public Color yellow;
            public Color cyan;

            /// <summary>
            /// Converts a PieceColor to a Color.
            /// </summary>
            /// <param name="color">The PieceColor</param>
            /// <returns></returns>
            public Color GetColor(PieceColor color) => color switch {
                PieceColor.Red => red,
                PieceColor.Green => green,
                PieceColor.Blue => blue,
                PieceColor.Magenta => magenta,
                PieceColor.Yellow => yellow,
                PieceColor.Cyan => cyan,
                _ => Color.white
            };

            /// <summary>
            /// Statically adds the two colors together and returns the result as a color.
            /// </summary>
            /// <param name="a">The instigating piece color.</param>
            /// <param name="b">The target piece color.</param>
            /// <returns>The combined color.</returns>
            public Color Add(Color a, Color b) => (a, b) switch {
                _ when a == b => a,
                _ when a == red && b == green => yellow,
                _ when a == red && b == blue => magenta,
                _ when a == green && b == red => yellow,
                _ when a == green && b == blue => cyan,
                _ when a == blue && b == red => magenta,
                _ when a == blue && b == green => cyan,
                _ => a
            };
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
}