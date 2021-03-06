﻿using System;
using System.Collections.Generic;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Infrastructure;

namespace MyServiceLibrary.Services
{
    public class LoggableUserService : IService<User>
    {
        private readonly ILogger logger;
        private IService<User> decoratedService;

        public LoggableUserService(IService<User> service, ILogger logger)
        {
            if (service == null)
            {
                throw new ArgumentNullException($"{nameof(service)} argument is null");
            }

            if (logger == null)
            {
                throw new ArgumentNullException($"{nameof(logger)} argument is null");
            }

            this.decoratedService = service;
            this.logger = logger;
        }

        public User Add(User user)
        {
            try
            {
                var result = this.decoratedService.Add(user);
                this.logger.Trace($"User {result.Id} {result.FirstName} {result.LastName} added");
                return result;
            }
            catch (Exception exception)
            {
                this.logger.Error(exception.Message);
                throw;
            }
        }

        public bool Delete(User user)
        {
            try
            {
                var result = this.decoratedService.Delete(user);
                this.logger.Trace($"User {user.Id} {user.FirstName} {user.LastName} deleted: {result}");
                return result;
            }
            catch (Exception exception)
            {
                this.logger.Error(exception.Message);
                throw;
            }
        }

        public IList<User> GetAll()
        {
            try
            {
                var result = this.decoratedService.GetAll();
                this.logger.Trace($"{result.Count} users searched");
                return result;
            }
            catch (Exception exception)
            {
                this.logger.Error(exception.Message);
                throw;
            }
        }

        public IList<User> GetByPredicate(ISearchCriteria<User> predicate)
        {
            try
            {
                var result = this.decoratedService.GetByPredicate(predicate);
                this.logger.Trace($"{result.Count} users searched by predicate {predicate.GetType()}");
                return result;
            }
            catch (Exception exception)
            {
                this.logger.Error(exception.Message);
                throw;
            }
        }

        public bool Save()
        {
            try
            {
                var result = this.decoratedService.Save();
                this.logger.Trace($"State saved: {result}");
                return result;
            }
            catch (Exception exception)
            {
                this.logger.Error(exception.Message);
                throw;
            }
        }

        public bool Load()
        {
            try
            {
                var result = this.decoratedService.Load();
                this.logger.Trace($"State loaded: {result}");
                return result;
            }
            catch (Exception exception)
            {
                this.logger.Error(exception.Message);
                throw;
            }
        }
    }
}
