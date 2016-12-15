using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiClient.Infrastructure;
using WebApiClient.Models;

namespace WebApiClient.Controllers
{
    public class ItemsController : ApiController
    {
        private static readonly TodoService service = new TodoService();

        // GET: api/Items
        public async Task<IEnumerable<TodoItemModel>> GetTodoItems()
        {
            IEnumerable<TodoItemModel> result = await service.GetAll();

            return result;
        }

        // GET: api/Items/5
        [ResponseType(typeof(TodoItemModel))]
        public async Task<IHttpActionResult> GetTodoItem(int id)
        {
            TodoItemModel todoItem = await service.Get(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTodoItem(int id, TodoItemModel item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            await service.UpdateItem(id, item);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Items
        [ResponseType(typeof(TodoItemModel))]
        public async Task<IHttpActionResult> PostTodoItem(TodoItemModel item)
        {
            item.Created = DateTime.Now;

            TodoItemModel result = await service.AddItem(item);

            if (result == null)
            {
                return BadRequest(ModelState);
            }

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, result);
        }

        // DELETE: api/Items/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> DeleteTodoItem(int id)
        {
            await service.RemoveItem(id);

            return Ok();
        }
    }
}
