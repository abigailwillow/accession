using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using UnityEngine;
using Accession.Models;

namespace Accession.Converters {
    public class BoardConverter : JsonConverter<Board> {
        public override Board Read(ref Utf8JsonReader reader, Type _, JsonSerializerOptions options) {
            Vector2Int boardSize = new Vector2Int();
            List<Cell> cells = new List<Cell>();
            List<Piece> pieces = new List<Piece>();

            while (reader.Read()) {
                switch (reader.TokenType) {
                    case JsonTokenType.PropertyName:
                        string propertyName = reader.GetString();
                        switch (propertyName) {
                            case "size":
                                reader.Read();
                                if (reader.TokenType != JsonTokenType.StartObject) break;

                                while (reader.Read()) {
                                    if (reader.TokenType == JsonTokenType.EndObject) break;

                                    string sizePropertyName = reader.GetString();
                                    reader.Read();

                                    switch (sizePropertyName) {
                                        case "x":
                                            boardSize.x = reader.GetInt32();
                                            break;
                                        case "y":
                                            boardSize.y = reader.GetInt32();
                                            break;
                                    }
                                }
                                break;
                            case "cells":
                                reader.Read();
                                if (reader.TokenType != JsonTokenType.StartArray) break;

                                while (reader.Read()) {
                                    if (reader.TokenType == JsonTokenType.EndArray || reader.TokenType != JsonTokenType.StartObject) break;

                                    Vector2Int position = new Vector2Int();
                                    ColorType color = ColorType.None;

                                    if (reader.TokenType == JsonTokenType.StartObject) {
                                        while (reader.Read()) {
                                            if (reader.TokenType == JsonTokenType.EndObject) break;

                                            string cellPropertyName = reader.GetString();
                                            reader.Read();

                                            switch (cellPropertyName) {
                                                case "position":
                                                    if (reader.TokenType != JsonTokenType.StartObject) break;

                                                    while (reader.Read()) {
                                                        if (reader.TokenType == JsonTokenType.EndObject) break;

                                                        string positionPropertyName = reader.GetString();
                                                        reader.Read();

                                                        switch (positionPropertyName) {
                                                            case "x":
                                                                position.x = reader.GetInt32();
                                                                break;
                                                            case "y":
                                                                position.y = reader.GetInt32();
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case "color":
                                                    color = Enum.Parse<ColorType>(reader.GetString(), true);
                                                    break;
                                            }
                                        }
                                        cells.Add(new Cell(position, color));
                                    }
                                }
                                break;
                            case "pieces":
                                reader.Read();
                                if (reader.TokenType != JsonTokenType.StartArray) break;
                                while (reader.Read()) {
                                    if (reader.TokenType == JsonTokenType.EndArray || reader.TokenType != JsonTokenType.StartObject) break;

                                    ColorType color = ColorType.None;

                                    if (reader.TokenType == JsonTokenType.StartObject) {
                                        while (reader.Read()) {
                                            if (reader.TokenType == JsonTokenType.EndObject) break;


                                            string piecePropertyName = reader.GetString();
                                            reader.Read();

                                            switch (piecePropertyName) {
                                                case "position":
                                                    if (reader.TokenType != JsonTokenType.StartObject) break;

                                                    while (reader.Read()) {
                                                        if (reader.TokenType == JsonTokenType.EndObject) break;
                                                    }
                                                    break;
                                                case "color":
                                                    color = Enum.Parse<ColorType>(reader.GetString(), true);
                                                    break;
                                            }
                                        }
                                        pieces.Add(new Piece(color));
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
            return new Board(boardSize, cells, pieces);
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