using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel
{
    public interface ISource : IModel
    {
        ISource Root<T>(string alias)
            where T : IModel;
        
        ISource LeftInnerJoin<T>(string alias)
            where T : IModel;
        
        ISource OuterJoin<T>(string alias)
            where T : IModel;

        ISource CrossJoin<T>(string alias)
            where T : IModel;

        ISource On { get; }
        ISource And { get; }
        ISource Or { get; }
        ISource OpenParentheses { get; }
        ISource CloseParentheses { get; }

        #region Predicates

        ISource IsEqualTo<TParent, TChild>(Expression<Func<TParent, object>> parentProperty, Expression<Func<TChild>> childProperty)
            where TParent : IModel
            where TChild : IModel;
        ISource IsNotEqualTo<TParent, TChild>(Expression<Func<TParent, object>> parentProperty, Expression<Func<TChild, object>> childProperty)
            where TParent : IModel 
            where TChild : IModel;
        ISource IsGreaterThan<TParent, TChild>(Expression<Func<TParent, object>> parentProperty, Expression<Func<TChild, object>> childProperty) 
            where TParent : IModel 
            where TChild : IModel;
        ISource IsGreaterThanOrEqualTo<TParent, TChild>(Expression<Func<TParent, object>> parentProperty, Expression<Func<TChild, object>> childProperty)
            where TParent : IModel
            where TChild : IModel;
        ISource IsLessThan<TParent, TChild>(Expression<Func<TParent, object>> parentProperty, Expression<Func<TChild, object>> childProperty)
            where TParent : IModel
            where TChild : IModel;
        ISource IsLessThanOrEqualTo<TParent, TChild>(Expression<Func<TParent, object>> parentProperty, Expression<Func<TChild, object>> childProperty)
            where TParent : IModel
            where TChild : IModel;

        #endregion

        #region Properties

        //Expression<Func<T, object>> Property<T>(string alias, string name) where T : ISource;

        #endregion
    }
}
