using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RestWebApi.Models;
using Bll.Entities;
using Bll.Services;
using RestWebApi.Infrastructure;
using System.Collections.Generic;
using System;

namespace RestWebApi.Controllers
{
    public class TodoItemsController : ApiController
    {
        private static readonly TodoService service = new TodoService();

        // GET: api/TodoItems
        public async Task<IEnumerable<TodoItemModel>> GetTodoItems()
        {
            //var result = service.GetAll_();

            IList<BllTodoItem> result = await service.GetAll();

            return result.Select(i => i.ToViewModel());
        }

        // GET: api/TodoItems/5
        public async Task<IHttpActionResult> GetTodoItem(int id)
        {
            BllTodoItem todoItem = await service.Get(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem.ToViewModel());
        }

        // PUT: api/TodoItems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTodoItem(int id, TodoItemModel item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            bool result = false;

            try
            {
                result = await service.Update(item.ToBll());
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            if (!result)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TodoItems
        [ResponseType(typeof(TodoItemModel))]
        public async Task<IHttpActionResult> Post(TodoItemModel item)
        {
            item.Created = DateTime.Now;

            BllTodoItem result = await service.Add(item.ToBll());

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, result.ToViewModel());
        }

        // DELETE: api/TodoItems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Delete(int id)
        {
            BllTodoItem result = await service.Remove(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}