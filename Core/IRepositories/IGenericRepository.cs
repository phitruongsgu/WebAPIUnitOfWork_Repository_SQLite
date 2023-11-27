﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UOWPocketBookAPI.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<bool> Add(T entity);
        Task<bool> Delete(Guid id);
        Task<bool> Update(T entity);
    }
}
