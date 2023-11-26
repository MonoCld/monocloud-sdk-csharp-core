using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MonoCloud.SDK.Core.Helpers;

/// <summary>
///
/// </summary>
/// <typeparam name="TBase"></typeparam>
public class JsonPropertyContract<TBase>
{
  internal JsonPropertyContract(PropertyInfo property, Func<Expression, Type, Expression> setterCastExpression)
  {
    GetValue = ExpressionExtensions.GetPropertyFunc<TBase>(property).Compile();

    if (property.GetSetMethod() is not null)
    {
      SetValue = ExpressionExtensions.SetPropertyFunc<TBase>(property, setterCastExpression).Compile();
    }

    PropertyType = property.PropertyType;
  }

  /// <summary>
  ///
  /// </summary>
  public Func<TBase, object?> GetValue { get; }

  /// <summary>
  ///
  /// </summary>
  public Action<TBase, object>? SetValue { get; }

  /// <summary>
  ///
  /// </summary>
  public Type PropertyType { get; }
}
