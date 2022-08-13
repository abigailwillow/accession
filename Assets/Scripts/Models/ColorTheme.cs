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