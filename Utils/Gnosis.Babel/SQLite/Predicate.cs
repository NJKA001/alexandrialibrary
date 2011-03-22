using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite
{
    public class Predicate<TInterface, TConcrete> : Statement, IPredicate<TInterface>
        where TInterface : IStatement
        where TConcrete : Statement, TInterface, new()
    {
        #region Private Constants

        private const string OpEqualTo = "=";
        private const string OpNotEqualTo = "<>";
        private const string OpLessThan = "<";
        private const string OpLessThanOrEqualTo = "<=";
        private const string OpGreaterThan = ">";
        private const string OpGreaterThanOrEqualTo = ">=";
        private const string OpInPrefix = "in (";
        private const string OpNotInPrefix = "not in (";
        private const string OpInSuffix = ")";
        private const string OpNull = "isnull";
        private const string OpNotNull = "not null";
        private const string OpLike = "like";
        private const string OpNotLike = "not like";

        #endregion

        public TInterface IsEqualTo(string expression)
        {
            AppendWord(OpEqualTo);
            return AppendWord<TInterface, TConcrete>(expression);
        }

        public TInterface IsEqualTo(string name, object value)
        {
            AppendWord(OpEqualTo);
            return AppendSimpleParameter<TInterface, TConcrete>(name, value);
        }

        public TInterface IsEqualTo<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(OpEqualTo);
            return AppendWord<TInterface, TConcrete>(expression.ToName());
        }

        public TInterface IsEqualTo<T>(Expression<Func<T, object>> expression, object value)
        {
            AppendWord(OpEqualTo);
            return AppendSimpleParameter<TInterface, TConcrete>(expression.ToName(), value);
        }

        public TInterface IsNotEqualTo(string expression)
        {
            AppendWord(OpNotEqualTo);
            return AppendWord<TInterface, TConcrete>(expression);
        }

        public TInterface IsNotEqualTo(string name, object value)
        {
            AppendWord(OpNotEqualTo);
            return AppendSimpleParameter<TInterface, TConcrete>(name, value);
        }

        public TInterface IsNotEqualTo<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(OpNotEqualTo);
            return AppendWord<TInterface, TConcrete>(expression.ToString());
        }

        public TInterface IsNotEqualTo<T>(Expression<Func<T, object>> expression, object value)
        {
            AppendWord(OpNotEqualTo);
            return AppendSimpleParameter<TInterface, TConcrete>(expression.ToName(), value);
        }

        public TInterface IsLessThan(string expression)
        {
            AppendWord(OpLessThan);
            return AppendWord<TInterface, TConcrete>(expression);
        }

        public TInterface IsLessThan(string name, object value)
        {
            AppendWord(OpLessThan);
            return AppendSimpleParameter<TInterface, TConcrete>(name, value);
        }

        public TInterface IsLessThan<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(OpLessThan);
            return AppendWord<TInterface, TConcrete>(expression.ToName());
        }

        public TInterface IsLessThan<T>(Expression<Func<T, object>> expression, object value)
        {
            AppendWord(OpLessThan);
            return AppendSimpleParameter<TInterface, TConcrete>(expression.ToName(), value);
        }

        public TInterface IsLessThanOrEqualTo(string expression)
        {
            AppendWord(OpLessThanOrEqualTo);
            return AppendWord<TInterface, TConcrete>(expression);
        }

        public TInterface IsLessThanOrEqualTo(string name, object value)
        {
            AppendWord(OpLessThanOrEqualTo);
            return AppendSimpleParameter<TInterface, TConcrete>(name, value);
        }

        public TInterface IsLessThanOrEqualTo<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(OpLessThanOrEqualTo);
            return AppendWord<TInterface, TConcrete>(expression.ToName());
        }

        public TInterface IsLessThanOrEqualTo<T>(Expression<Func<T, object>> expression, object value)
        {
            AppendWord(OpLessThanOrEqualTo);
            return AppendSimpleParameter<TInterface, TConcrete>(expression.ToName(), value);
        }

        public TInterface IsGreaterThan(string expression)
        {
            AppendWord(OpGreaterThan);
            return AppendWord<TInterface, TConcrete>(expression);
        }

        public TInterface IsGreaterThan(string name, object value)
        {
            AppendWord(OpGreaterThan);
            return AppendSimpleParameter<TInterface, TConcrete>(name, value);
        }

        public TInterface IsGreaterThan<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(OpGreaterThan);
            return AppendWord<TInterface, TConcrete>(expression.ToName());
        }

        public TInterface IsGreaterThan<T>(Expression<Func<T, object>> expression, object value)
        {
            AppendWord(OpGreaterThan);
            return AppendSimpleParameter<TInterface, TConcrete>(expression.ToName(), value);
        }

        public TInterface IsGreaterThanOrEqualTo(string expression)
        {
            AppendWord(OpGreaterThanOrEqualTo);
            return AppendWord<TInterface, TConcrete>(expression);
        }

        public TInterface IsGreaterThanOrEqualTo(string name, object value)
        {
            AppendWord(OpGreaterThanOrEqualTo);
            return AppendSimpleParameter<TInterface, TConcrete>(name, value);
        }

        public TInterface IsGreaterThanOrEqualTo<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(OpGreaterThanOrEqualTo);
            return AppendWord<TInterface, TConcrete>(expression.ToName());
        }

        public TInterface IsGreaterThanOrEqualTo<T>(Expression<Func<T, object>> expression, object value)
        {
            AppendWord(OpGreaterThanOrEqualTo);
            return AppendSimpleParameter<TInterface, TConcrete>(expression.ToName(), value);
        }

        public TInterface IsLike(string expression)
        {
            AppendWord(OpLike);
            return AppendWord<TInterface, TConcrete>(expression);
        }

        public TInterface IsLike(string name, object value)
        {
            AppendWord(OpLike);
            return AppendSimpleParameter<TInterface, TConcrete>(name, value);
        }

        public TInterface IsLike<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(OpLike);
            return AppendWord<TInterface, TConcrete>(expression.ToName());
        }

        public TInterface IsLike<T>(Expression<Func<T, object>> expression, object value)
        {
            AppendWord(OpLike);
            return AppendSimpleParameter<TInterface, TConcrete>(expression.ToName(), value);
        }

        public TInterface IsNotLike(string expression)
        {
            AppendWord(OpNotLike);
            return AppendWord<TInterface, TConcrete>(expression);
        }

        public TInterface IsNotLike(string name, object value)
        {
            AppendWord(OpNotLike);
            return AppendSimpleParameter<TInterface, TConcrete>(name, value);
        }

        public TInterface IsNotLike<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(OpNotLike);
            return AppendWord<TInterface, TConcrete>(expression.ToName());
        }

        public TInterface IsNotLike<T>(Expression<Func<T, object>> expression, object value)
        {
            AppendWord(OpNotLike);
            return AppendSimpleParameter<TInterface, TConcrete>(expression.ToName(), value);
        }

        public TInterface IsIn(string expression)
        {
            return AppendWord<TInterface, TConcrete>(OpInPrefix + expression + OpInSuffix);
        }

        public TInterface IsIn(string name, object value)
        {
            AppendWord(OpInPrefix);
            AppendSimpleParameter(name, value);
            return AppendWord<TInterface, TConcrete>(OpInSuffix);
        }

        public TInterface IsNotIn(string expression)
        {
            return AppendWord<TInterface, TConcrete>(OpNotInPrefix + expression + OpInSuffix);
        }

        public TInterface IsNotIn(string name, object value)
        {
            AppendWord(OpNotInPrefix);
            AppendSimpleParameter(name, value);
            return AppendWord<TInterface, TConcrete>(OpInSuffix);
        }

        public TInterface IsNull
        {
            get { return AppendWord<TInterface, TConcrete>(OpNull); }
        }

        public TInterface IsNotNull
        {
            get { return AppendWord<TInterface, TConcrete>(OpNotNull); }
        }
    }
}
