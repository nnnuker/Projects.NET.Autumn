using Bll.Interfaces;
using System;

namespace Bll.Entities
{
    public class BllTodoItem : IBllEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
    }
}
