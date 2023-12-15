using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class EpochDateTimeConverter : JsonConverter<DateTime>
{
  private static readonly DateTime epochDateTime = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

  /// Converts Unix Epoch to DateTime
  private static DateTime FromUnixTimestamp(long timestamp)
  {
    return epochDateTime.AddSeconds(timestamp);
  }

  /// Converts DateTime to Unix Epoch
  private static long ToUnixTimestamp(DateTime date)
  {
    return Convert.ToInt64((date.ToUniversalTime() - epochDateTime).TotalSeconds);
  }

  public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    var unixTimestamp = reader.GetInt64();
    return FromUnixTimestamp(unixTimestamp);
  }

  public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
  {
    var unixTimestamp = ToUnixTimestamp(value);
    writer.WriteNumberValue(unixTimestamp);
  }}
