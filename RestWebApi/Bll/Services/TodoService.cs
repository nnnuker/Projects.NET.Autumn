using Bll.Context;
using Bll.Entities;
using Bll.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Bll.Services
{
    public class TodoService : IAsyncService<BllTodoItem>
    {
        TodoItemsContext db = new TodoItemsContext();

        public Task<BllTodoItem> Add(BllTodoItem item)
        {
            throw new NotImplementedException();

            //service.TodoItems.Add(todoItem);
            //await service.SaveChangesAsync();
        }

        public async Task<BllTodoItem> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<BllTodoItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<BllTodoItem> Remove(int id)
        {
            throw new NotImplementedException();

            //TodoItemModel todoItem = await service.TodoItems.FindAsync(id);
            //if (todoItem == null)
            //{
            //    return NotFound();
            //}

            //service.TodoItems.Remove(todoItem);
            //await service.SaveChangesAsync();
        }

        public async Task<bool> Update(BllTodoItem item)
        {
            db.Entry(item).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TodoItemExists(item.Id))
                {
                    return false;
                }
                else
                {
                    throw new ArgumentException(ex.Message, ex);
                }
            }
        }

        private bool TodoItemExists(int id)
        {
            return db.TodoItems.Count(e => e.Id == id) > 0;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                db.Dispose();

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~TodoService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
