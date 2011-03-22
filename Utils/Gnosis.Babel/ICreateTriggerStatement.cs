using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel
{
    public interface ICreateTriggerStatement<T> : IStatement
        where T : IModel
    {
        T Model { get; set; }
        ICreateTriggerStatement<T> Temporary { get; }
        ICreateTriggerStatement<T> IfNotExists { get; }
        
        ICreateTriggerStatement<T> BeforeDelete { get; }
        ICreateTriggerStatement<T> AfterDelete { get; }
        ICreateTriggerStatement<T> InsteadOfDelete { get; }
        ICreateTriggerStatement<T> BeforeInsert { get; }
        ICreateTriggerStatement<T> AfterInsert { get; }
        ICreateTriggerStatement<T> InsteadOfInsert { get; }
        ICreateTriggerStatement<T> BeforeUpdate { get; }
        ICreateTriggerStatement<T> AfterUpdate { get; }
        ICreateTriggerStatement<T> InsteadOfUpdate { get; }

        ICreateTriggerStatement<T> OfColumn(Expression<Func<T, object>> property);
        ICreateTriggerStatement<T> Do(IStatement statement);
    }
}
