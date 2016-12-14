using Bll.Entities;
using RestWebApi.Models;

namespace RestWebApi.Infrastructure
{
    public static class TodoItemMapper
    {
        public static TodoItemModel ToViewModel(this BllTodoItem bllItem)
        {
            return new TodoItemModel
            {
                Id = bllItem.Id,
                Title = bllItem.Title,
                Body = bllItem.Body,
                Author = bllItem.Author,
                Created = bllItem.Created
            };
        }

        public static BllTodoItem ToBll(this TodoItemModel viewItem)
        {
            return new BllTodoItem
            {
                Id = viewItem.Id,
                Title = viewItem.Title,
                Body = viewItem.Body,
                Author = viewItem.Author,
                Created = viewItem.Created
            };
        }
    }
}