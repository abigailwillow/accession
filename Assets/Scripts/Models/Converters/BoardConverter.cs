using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UnityEngine;
using Accession.Models;

namespace Accession.Converters {
    public class BoardConverter : JsonConverter<Board> {
        public override Board Read(ref Utf8JsonReader reader, Type _, JsonSerializerOptions options) {
            while (reader.Read()) {
                if (reader.TokenType == JsonTokenType.PropertyName) {
                    Debug.Log($"{reader.TokenType}: {reader.GetString()}");
                }
                Debug.Log($"{reader.TokenType}: ()");
            }
            Vector2Int size = new Vector2Int(reader.GetInt32(), reader.GetInt32());

            return new Board(size, new List<Cell>(), new List<Piece>());
        }

        public override void Write(Utf8JsonWriter writer, Board board, JsonSerializerOptions options) {
            writer.WriteStartObject(); // {
            writer.WriteStartObject("size"); // "size": {
            writer.WriteNumber("x", board.size.x); // "x": board.size.x,                                
            writer.WriteNumber("y", board.size.y); // "y": board.size.y
            writer.WriteEndObject(); // },
            writer.WriteStartArray("cells"); // "cells": [
            board.cells.ForEach(cell => {
                if (cell.color != ColorType.None) {
                    writer.WriteStartObject(); // {
                    writer.WriteString("color", cell.color.ToString().ToLower()); // "color": cell.color,
                    writer.WriteStartObject("position"); // "position": {
                    writer.WriteNumber("x", cell.position.x); // "x": cell.position.x,
                    writer.WriteNumber("y", cell.position.y); // "y": cell.position.y
                    writer.WriteEndObject(); // }
                    writer.WriteEndObject(); // }
                }
            });
            writer.WriteEndArray(); // ],
            writer.WriteStartArray("pieces"); // "pieces": [
            board.pieces.ForEach(piece => {
                if (piece.color != ColorType.None) {
                    writer.WriteStartObject(); // {
                    writer.WriteString("color", piece.color.ToString().ToLower()); // "color": piece.color,
                    writer.WriteStartObject("position"); // "position": {
                    writer.WriteNumber("x", piece.position.x); // "x": piece.position.x,
                    writer.WriteNumber("y", piece.position.y); // "y": piece.position.y
                    writer.WriteEndObject(); // }
                    writer.WriteEndObject(); // }
                }
            });
            writer.WriteEndArray(); // ]
            writer.WriteEndObject(); // }
        }
    }
}