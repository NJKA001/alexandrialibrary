using System;
using System.Linq.Expressions;

namespace Gnosis.Alexandria.Utilities
{
    public static class ReflectionExtensions
    {
        public static string ToName<T>(this Expression<Func<T, object>> getter)
        {
            return getter.Body.ToName();
        }

        public static object GetValue<T>(this Expression<Func<T, object>> getter, T instance)
        {
            var function = getter.Compile();
            return function(instance);
        }

        public static string ToName(this Expression expression)
        {
            MemberExpression memberExpression = null;
            if (expression.NodeType == ExpressionType.Convert)
            {
                var body = (UnaryExpression)expression;
                memberExpression = body.Operand as MemberExpression;
            }
            else if (expression.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = expression as MemberExpression;
            }

            if (memberExpression == null)
                throw new ArgumentException("Getter is not a valid member expression");

            return memberExpression.Member.Name;
        }
    }
}
