using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MyServiceLibrary.Services
{
    public class ConcurrentUserService : IService<User>
    {
        private IService<User> decoratedService;
        private ReaderWriterLockSlim lockSlim;

        public ConcurrentUserService(IService<User> service)
        {
            if (service == null)
                throw new ArgumentNullException($"{nameof(service)} argument is null");

            this.decoratedService = service;
            lockSlim = new ReaderWriterLockSlim();
        }

        public User Add(User user)
        {
            lockSlim.EnterWriteLock();
            try
            {
                return decoratedService.Add(user);
            }
            finally
            {
                lockSlim.ExitWriteLock();
            }
        }

        public bool Delete(User user)
        {
            lockSlim.EnterWriteLock();
            try
            {
                return decoratedService.Delete(user);
            }
            finally
            {
                lockSlim.ExitWriteLock();
            }
        }

        public IList<User> GetAll()
        {
            lockSlim.EnterReadLock();
            try
            {
                return decoratedService.GetAll();
            }
            finally
            {
                lockSlim.ExitReadLock();
            }
        }

        public IList<User> GetByPredicate(ISearchCriteria<User> predicate)
        {
            lockSlim.EnterReadLock();
            try
            {
                return decoratedService.GetByPredicate(predicate);
            }
            finally
            {
                lockSlim.ExitReadLock();
            }
        }

        public bool Save()
        {
            lockSlim.EnterWriteLock();
            try
            {
                return decoratedService.Save();
            }
            finally
            {
                lockSlim.ExitWriteLock();
            }
        }

        public bool Load()
        {
            lockSlim.EnterWriteLock();
            try
            {
                return decoratedService.Load();
            }
            finally
            {
                lockSlim.ExitWriteLock();
            }
        }
    }
}
