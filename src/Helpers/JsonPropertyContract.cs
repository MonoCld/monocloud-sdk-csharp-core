using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MonoCloud.Core.Helpers;

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

  public Func<TBase, object?> GetValue { get; }
  public Action<TBase, object>? SetValue { get; }
  public Type PropertyType { get; }
}
