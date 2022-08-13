using UnityEngine;
using Accession.Controllers;
using Accession.Models;

namespace Accession.Extensions {
    public static class ColorExtension {
        /// <summary>
        /// Converts this Color to a ColorType.
        /// </summary>
        /// <param name="color">The Color to convert.</param>
        /// <returns>The given Color's respective ColorType, or ColorType.None if unknown.</returns>
        public static ColorType ToColorType(this Color color) => color switch {
            _ when color == BoardController.instance.colors.piece.red => ColorType.Red,
            _ when color == BoardController.instance.colors.piece.green => ColorType.Green,
            _ when color == BoardController.instance.colors.piece.blue => ColorType.Blue,
            _ when color == BoardController.instance.colors.piece.magenta => ColorType.Magenta,
            _ when color == BoardController.instance.colors.piece.yellow => ColorType.Yellow,
            _ when color == BoardController.instance.colors.piece.cyan => ColorType.Cyan,
            _ => ColorType.None
        };

        /// <summary>
        /// Converts this ColorType to a Color.
        /// </summary>
        /// <param name="colorType">The ColorType to convert.</param>
        /// <returns>The given ColorType's respective Color.</returns>
        public static Color ToColor(this ColorType colorType) => colorType switch {
            ColorType.Red => BoardController.instance.colors.piece.red,
            ColorType.Green => BoardController.instance.colors.piece.green,
            ColorType.Blue => BoardController.instance.colors.piece.blue,
            ColorType.Magenta => BoardController.instance.colors.piece.magenta,
            ColorType.Yellow => BoardController.instance.colors.piece.yellow,
            ColorType.Cyan => BoardController.instance.colors.piece.cyan,
            _ => Color.white
        };

            /// <summary>
            /// Statically adds the two colors together and returns the result as a color.
            /// </summary>
            /// <param name="b">The color to add to this color.</param>
            /// <returns>The combined color.</returns>
        public static ColorType Add(ColorType a, ColorType b) => (a, b) switch {
                _ when a == b => a,
                _ when a == ColorType.Red && b == ColorType.Green => ColorType.Yellow,
                _ when a == ColorType.Red && b == ColorType.Blue => ColorType.Magenta,
                _ when a == ColorType.Green && b == ColorType.Red => ColorType.Yellow,
                _ when a == ColorType.Green && b == ColorType.Blue => ColorType.Cyan,
                _ when a == ColorType.Blue && b == ColorType.Red => ColorType.Magenta,
                _ when a == ColorType.Blue && b == ColorType.Green => ColorType.Cyan,
                _ => a
            };
    }
}