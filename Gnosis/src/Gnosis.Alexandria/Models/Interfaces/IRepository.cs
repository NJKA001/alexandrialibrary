﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IRepository<T>
        where T : IModel
    {
        void Initialize();
        void Persist(T model);
        void Persist(IEnumerable<T> models);
        T GetOne(object id);
        ICollection<T> GetMany(ICommand command);
        ICollection<T> GetAll();
    }
}