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
            writer.WriteStringValue("");
        }
    }
}