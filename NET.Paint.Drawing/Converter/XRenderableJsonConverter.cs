using System.Text.Json;
using System.Text.Json.Serialization;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Constant;

public class XRenderableJsonConverter : JsonConverter<XRenderable>
{
    public override XRenderable? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        if (!root.TryGetProperty("Type", out var typeProp))
            throw new JsonException("Missing Type discriminator");

        ToolType type = (ToolType)typeProp.GetInt32();

        XRenderable? result = type switch
        {
            ToolType.Line => JsonSerializer.Deserialize<XLine>(root.GetRawText(), options),
            ToolType.Circle => JsonSerializer.Deserialize<XCircle>(root.GetRawText(), options),
            ToolType.Rectangle => JsonSerializer.Deserialize<XRectangle>(root.GetRawText(), options),
            ToolType.Ellipse => JsonSerializer.Deserialize<XEllipse>(root.GetRawText(), options),
            ToolType.Pencil => JsonSerializer.Deserialize<XPencil>(root.GetRawText(), options),
            ToolType.Bezier => JsonSerializer.Deserialize<XBezier>(root.GetRawText(), options),
            ToolType.Triangle => JsonSerializer.Deserialize<XTriangle>(root.GetRawText(), options),
            ToolType.Pentagon => JsonSerializer.Deserialize<XPentagon>(root.GetRawText(), options),
            ToolType.Hexagon => JsonSerializer.Deserialize<XHexagon>(root.GetRawText(), options),
            ToolType.Octagon => JsonSerializer.Deserialize<XOctagon>(root.GetRawText(), options),
            ToolType.Heart => JsonSerializer.Deserialize<XHeart>(root.GetRawText(), options),
            ToolType.Spiral => JsonSerializer.Deserialize<XSpiral>(root.GetRawText(), options),
            ToolType.Star => JsonSerializer.Deserialize<XStar>(root.GetRawText(), options),
            ToolType.Arrow => JsonSerializer.Deserialize<XArrow>(root.GetRawText(), options),
            ToolType.Text => JsonSerializer.Deserialize<XText>(root.GetRawText(), options),
            ToolType.Bitmap => JsonSerializer.Deserialize<XBitmap>(root.GetRawText(), options),
            _ => throw new NotSupportedException($"Unknown ToolType: {type}")
        };

        return result;
    }

    public override void Write(Utf8JsonWriter writer, XRenderable value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
    }
}