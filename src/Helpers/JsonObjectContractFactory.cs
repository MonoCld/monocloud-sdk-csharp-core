using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json.Serialization;

namespace MonoCloud.Core.Helpers;

public class JsonObjectContractFactory<TBase>
{
  private ConcurrentDictionary<Type, ReadOnlyDictionary<string, JsonPropertyContract<TBase>>> Properties { get; } =
    new();

  protected virtual Expression CreateSetterCastExpression(Expression e, Type t) => Expression.Convert(e, t);

  private ReadOnlyDictionary<string, JsonPropertyContract<TBase>> CreateProperties(Type type)
  {
    if (!typeof(TBase).IsAssignableFrom(type))
    {
      throw new ArgumentException();
    }

    var dictionary = type
      .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
      .Where(p => p.GetIndexParameters().Length == 0 && p.GetGetMethod() != null &&
                  !Attribute.IsDefined(p, typeof(JsonIgnoreAttribute)))
      .ToDictionary(p => p.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? p.Name.ToSnakeCase(),
        p => new JsonPropertyContract<TBase>(p, (e, t) => CreateSetterCastExpression(e, t)),
        StringComparer.OrdinalIgnoreCase);

    return new ReadOnlyDictionary<string, JsonPropertyContract<TBase>>(dictionary);
  }

  public IReadOnlyDictionary<string, JsonPropertyContract<TBase>> GetProperties(Type type) => Properties.GetOrAdd(type, t => CreateProperties(t));
}
