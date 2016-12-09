using System;
using System.Collections.Generic;
using System.Threading;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Infrastructure;

namespace MyServiceLibrary.Services
{
    public class ConcurrentUserService : IService<User>
    {
        private IService<User> decoratedService;
        private ReaderWriterLockSlim lockSlim;

        public ConcurrentUserService(IService<User> service)
        {
            if (service == null)
            {
                throw new ArgumentNullException($"{nameof(service)} argument is null");
            }

            this.decoratedService = service;
            this.lockSlim = new ReaderWriterLockSlim();
        }

        public User Add(User user)
        {
            this.lockSlim.EnterWriteLock();
            try
            {
                return this.decoratedService.Add(user);
            }
            finally
            {
                this.lockSlim.ExitWriteLock();
            }
        }

        public bool Delete(User user)
        {
            this.lockSlim.EnterWriteLock();
            try
            {
                return this.decoratedService.Delete(user);
            }
            finally
            {
                this.lockSlim.ExitWriteLock();
            }
        }

        public IList<User> GetAll()
        {
            this.lockSlim.EnterReadLock();
            try
            {
                return this.decoratedService.GetAll();
            }
            finally
            {
                this.lockSlim.ExitReadLock();
            }
        }

        public IList<User> GetByPredicate(ISearchCriteria<User> predicate)
        {
            this.lockSlim.EnterReadLock();
            try
            {
                return this.decoratedService.GetByPredicate(predicate);
            }
            finally
            {
                this.lockSlim.ExitReadLock();
            }
        }

        public bool Save()
        {
            this.lockSlim.EnterWriteLock();
            try
            {
                return this.decoratedService.Save();
            }
            finally
            {
                this.lockSlim.ExitWriteLock();
            }
        }

        public bool Load()
        {
            this.lockSlim.EnterWriteLock();
            try
            {
                return this.decoratedService.Load();
            }
            finally
            {
                this.lockSlim.ExitWriteLock();
            }
        }
    }
}
