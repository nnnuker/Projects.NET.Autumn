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
        private readonly TodoItemsContext db = new TodoItemsContext();

        public async Task<BllTodoItem> Add(BllTodoItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var result = db.TodoItems.Add(item);
            await db.SaveChangesAsync();

            return result;
        }

        public async Task<BllTodoItem> Get(int id)
        {
            return await db.TodoItems.FindAsync(id);
        }

        public async Task<IList<BllTodoItem>> GetAll()
        {
            return await db.TodoItems.ToListAsync();
        }

        public IList<BllTodoItem> GetAll_()
        {
            return db.TodoItems.ToList();
        }

        public async Task<BllTodoItem> Remove(int id)
        {
            BllTodoItem todoItem = await db.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return null;
            }

            var result = db.TodoItems.Remove(todoItem);
            await db.SaveChangesAsync();

            return result;
        }

        public async Task<bool> Update(BllTodoItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

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

                throw new ArgumentException(ex.Message, ex);
            }
        }

        private bool TodoItemExists(int id)
        {
            return db.TodoItems.Count(e => e.Id == id) > 0;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
