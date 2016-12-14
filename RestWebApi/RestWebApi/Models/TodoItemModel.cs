using System;

namespace RestWebApi.Models
{
    public class TodoItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
    }
}