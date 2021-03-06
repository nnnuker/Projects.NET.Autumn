﻿using System;
using System.Collections.Generic;
using MyServiceLibrary.Interfaces.Entities;

namespace MyServiceLibrary.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        T Add(T entity);

        bool Delete(int id);

        IList<T> GetAll();

        IList<T> GetByPredicate(Predicate<T> predicate);

        bool Save();

        bool Load();
    }
}
