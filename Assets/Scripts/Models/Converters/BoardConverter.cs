using System.Drawing;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Accession.Models;

namespace Accession.Converters {
    public class BoardConverter : JsonConverter<Board> {
        public override Board Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            return JsonSerializer.Deserialize<Board>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, Board board, JsonSerializerOptions options) {
            writer.WriteStartObject();
            writer.WriteStartObject("size");
            writer.WriteNumber("x", board.size.x);
            writer.WriteNumber("y", board.size.y);
            writer.WriteEndObject();
            writer.WriteStartArray("cells");
            board.cells.ForEach(cell => {
                if (cell.color != ColorType.None) {
                    writer.WriteStartObject();
                    writer.WriteString("color", cell.color.ToString().ToLower());
                    writer.WriteStartObject("position");
                    writer.WriteNumber("x", cell.position.x);
                    writer.WriteNumber("y", cell.position.y);
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                }
            });
            writer.WriteEndArray();
            writer.WriteStartArray("pieces");
            board.pieces.ForEach(piece => {
                if (piece.color != ColorType.None) {
                    writer.WriteStartObject();
                    writer.WriteString("color", piece.color.ToString().ToLower());
                    writer.WriteStartObject("position");
                    writer.WriteNumber("x", piece.position.x);
                    writer.WriteNumber("y", piece.position.y);
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                }
            });
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}