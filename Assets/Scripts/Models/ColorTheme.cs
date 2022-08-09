using UnityEngine;

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

    public Color AddColors(PieceColor a, PieceColor b) => (a, b) switch {
        (PieceColor.Red, PieceColor.Red) => piece.red,
        (PieceColor.Red, PieceColor.Green) => piece.yellow,
        (PieceColor.Red, PieceColor.Blue) => piece.magenta,
        (PieceColor.Green, PieceColor.Red) => piece.yellow,
        (PieceColor.Green, PieceColor.Green) => piece.green,
        (PieceColor.Green, PieceColor.Blue) => piece.cyan,
        (PieceColor.Blue, PieceColor.Red) => piece.magenta,
        (PieceColor.Blue, PieceColor.Green) => piece.cyan,
        (PieceColor.Blue, PieceColor.Blue) => piece.blue,
        _ => Color.white
    };

    public Color GetPieceColor(PieceColor color) => color switch {
        PieceColor.Red => piece.red,
        PieceColor.Green => piece.green,
        PieceColor.Blue => piece.blue,
        PieceColor.Magenta => piece.magenta,
        PieceColor.Yellow => piece.yellow,
        PieceColor.Cyan => piece.cyan,
        _ => Color.white
    };

    public enum PieceColor {
        Red,
        Green,
        Blue,
        Magenta,
        Yellow,
        Cyan
    }
}