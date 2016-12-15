using System;
using WebApiClient.Infrastructure;

namespace WebApiClient.Models
{
    public class TodoItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [MaxLength]
        public string Body { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
    }
}