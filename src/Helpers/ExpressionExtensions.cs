using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MonoCloud.Core.Helpers;

public static class ExpressionExtensions
{
  public static Expression<Func<T, object>> GetPropertyFunc<T>(PropertyInfo property)
  {
    var arg = Expression.Parameter(typeof(T), "x");
    var getter = Expression.Property(arg, property);
    var cast = Expression.Convert(getter, typeof(object));
    return Expression.Lambda<Func<T, object>>(cast, arg);
  }

  public static Expression<Action<T, object>> SetPropertyFunc<T>(PropertyInfo property,
    Func<Expression, Type, Expression> setterCastExpression)
  {
    var arg1 = Expression.Parameter(typeof(T), "x");
    var arg2 = Expression.Parameter(typeof(object), "y");
    var cast = setterCastExpression(arg2, property.PropertyType);
    var setter = Expression.Call(arg1, property.GetSetMethod()!, cast);
    return Expression.Lambda<Action<T, object>>(setter, arg1, arg2);
  }
}
