using System;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MonoCloud.Core.Helpers;

public class PatchConverter<T> : JsonConverter<T> where T : class, new()
{
  private static readonly PatchConverterContractFactory ContractFactory = new();

  public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    var properties = ContractFactory.GetProperties(typeToConvert);

    if (reader.TokenType == JsonTokenType.Null)
    {
      return null;
    }

    if (reader.TokenType != JsonTokenType.StartObject)
    {
      throw new JsonException();
    }

    var value = new T();

    while (reader.Read())
    {
      if (reader.TokenType == JsonTokenType.EndObject)
      {
        return value;
      }

      if (reader.TokenType != JsonTokenType.PropertyName)
      {
        throw new JsonException();
      }

      var propertyName = reader.GetString()!;

      if (!properties.TryGetValue(propertyName, out var property) || property.SetValue == null)
      {
        reader.Skip();
      }
      else
      {
        var type = property.PropertyType.IsGenericType &&
                   property.PropertyType.GetGenericTypeDefinition() == typeof(Optional<>)
          ? property.PropertyType.GetGenericArguments()[0]
          : property.PropertyType;

        try
        {
          var item = JsonSerializer.Deserialize(ref reader, type, options);
          property.SetValue(value, item!);
        }
        catch (JsonException e)
        {
          var keyName = e.Path?.Replace("$", "");
          var newKey = keyName == string.Empty ? propertyName : $"{propertyName}.{keyName}";

          throw new JsonException(e.Message, newKey.Replace(".[", "["), e.LineNumber, e.BytePositionInLine,
            e.InnerException);
        }
      }
    }

    throw new JsonException();
  }

  public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
  {
    writer.WriteStartObject();

    foreach (var property in ContractFactory.GetProperties(value.GetType()))
    {
      if (options.IgnoreReadOnlyProperties && property.Value.SetValue == null)
      {
        continue;
      }

      var item = property.Value.GetValue(value);

      if (item is IOptional hasValue)
      {
        if (!hasValue.HasValue)
        {
          continue;
        }

        writer.WritePropertyName(property.Key);
        JsonSerializer.Serialize(writer, hasValue.GetValue(), options);
      }
      else
      {
        if (options.DefaultIgnoreCondition == JsonIgnoreCondition.WhenWritingNull && item is null)
        {
          continue;
        }

        writer.WritePropertyName(property.Key);
        JsonSerializer.Serialize(writer, item, property.Value.PropertyType, options);
      }
    }

    writer.WriteEndObject();
  }

  private class PatchConverterContractFactory : JsonObjectContractFactory<T>
  {
    protected override Expression CreateSetterCastExpression(Expression e, Type t)
    {
      if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Optional<>))
      {
        return Expression.Convert(Expression.Convert(e, t.GetGenericArguments()[0]), t);
      }

      return base.CreateSetterCastExpression(e, t);
    }
  }
}
