using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace Gnosis.Core
{
    public static class ReflectionHelper
    {
        #region DummyPropertyInfo

        [Serializable]
        private sealed class DummyPropertyInfo : PropertyInfo
        {
            private readonly string name;
            private readonly Type type;

            public DummyPropertyInfo(string name, Type type)
            {
                if (name == null) throw new ArgumentNullException("name");
                if (type == null) throw new ArgumentNullException("type");

                this.name = name;
                this.type = type;
            }

            public override Module Module
            {
                get { return null; }
            }

            public override int MetadataToken
            {
                get { return name.GetHashCode(); }
            }

            public override object[] GetCustomAttributes(bool inherit)
            {
                throw new NotImplementedException();
            }

            public override bool IsDefined(Type attributeType, bool inherit)
            {
                throw new NotImplementedException();
            }

            public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            public override MethodInfo[] GetAccessors(bool nonPublic)
            {
                throw new NotImplementedException();
            }

            public override MethodInfo GetGetMethod(bool nonPublic)
            {
                return null;
            }

            public override MethodInfo GetSetMethod(bool nonPublic)
            {
                return null;
            }

            public override ParameterInfo[] GetIndexParameters()
            {
                throw new NotImplementedException();
            }

            public override string Name
            {
                get { return name; }
            }

            public override Type DeclaringType
            {
                get { throw new NotImplementedException(); }
            }

            public override Type ReflectedType
            {
                get { throw new NotImplementedException(); }
            }

            public override Type PropertyType
            {
                get { return type; }
            }

            public override PropertyAttributes Attributes
            {
                get { throw new NotImplementedException(); }
            }

            public override bool CanRead
            {
                get { throw new NotImplementedException(); }
            }

            public override bool CanWrite
            {
                get { throw new NotImplementedException(); }
            }

            public override object[] GetCustomAttributes(Type attributeType, bool inherit)
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        public static PropertyInfo AsProperty<T>(this Expression<Func<T, object>> expression)
        {
            return GetMember<T>(expression) as PropertyInfo;
        }

        public static PropertyInfo AsProperty<TEntity, TValue>(this Expression<Func<TEntity, IEnumerable<TValue>>> expression)
            where TEntity : IEntity
            where TValue : IValue
        {
            return GetMember<TEntity, IEnumerable<TValue>>(expression) as PropertyInfo;
        }

        public static MemberInfo GetMember<TModel, TReturn>(Expression<Func<TModel, TReturn>> expression)
        {
            return GetMemberInfo(expression.Body);
        }

        public static MemberInfo GetMember<TModel>(Expression<Func<TModel, object>> expression)
        {
            return GetMemberInfo(expression.Body);
        }

        /*
        public static Accessor GetAccessor<MODEL>(Expression<Func<MODEL, object>> expression)
        {
            MemberExpression memberExpression = GetMemberExpression(expression.Body);

            return getAccessor(memberExpression);
        }

        public static Accessor GetAccessor<MODEL, T>(Expression<Func<MODEL, T>> expression)
        {
            MemberExpression memberExpression = GetMemberExpression(expression.Body);

            return getAccessor(memberExpression);
        }
        */

        private static bool IsIndexedPropertyAccess(Expression expression)
        {
            return IsMethodExpression(expression) && expression.ToString().Contains("get_Item");
        }

        private static bool IsMethodExpression(Expression expression)
        {
            return expression is MethodCallExpression || (expression is UnaryExpression && IsMethodExpression((expression as UnaryExpression).Operand));
        }

        private static MemberInfo GetMemberInfo(Expression expression)
        {
            if (IsIndexedPropertyAccess(expression))
                return GetDynamicComponentProperty(expression);
            if (IsMethodExpression(expression))
                return ((MethodCallExpression)expression).Method;

            var memberExpression = GetMemberExpression(expression);

            return memberExpression.Member;
        }

        private static PropertyInfo GetDynamicComponentProperty(Expression expression)
        {
            Type desiredConversionType = null;
            MethodCallExpression methodCallExpression = null;
            var nextOperand = expression;

            while (nextOperand != null)
            {
                if (nextOperand.NodeType == ExpressionType.Call)
                {
                    methodCallExpression = nextOperand as MethodCallExpression;
                    desiredConversionType = desiredConversionType ?? methodCallExpression.Method.ReturnType;
                    break;
                }

                if (nextOperand.NodeType != ExpressionType.Convert)
                    throw new ArgumentException("Expression not supported", "expression");

                var unaryExpression = (UnaryExpression)nextOperand;
                desiredConversionType = unaryExpression.Type;
                nextOperand = unaryExpression.Operand;
            }

            var constExpression = methodCallExpression.Arguments[0] as ConstantExpression;

            return new DummyPropertyInfo((string)constExpression.Value, desiredConversionType);
        }

        private static MemberExpression GetMemberExpression(Expression expression)
        {
            return GetMemberExpression(expression, true);
        }

        private static MemberExpression GetMemberExpression(Expression expression, bool enforceCheck)
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

            if (enforceCheck && memberExpression == null)
            {
                throw new ArgumentException("Not a member access", "expression");
            }

            return memberExpression;
        }

        /*
        private static Accessor getAccessor(MemberExpression memberExpression)
        {
            var list = new List<Member>();

            while (memberExpression != null)
            {
                list.Add(memberExpression.Member.ToMember());
                memberExpression = memberExpression.Expression as MemberExpression;
            }

            if (list.Count == 1)
            {
                return new SingleMember(list[0]);
            }

            list.Reverse();
            return new PropertyChain(list.ToArray());
        }
        */
    }
}
