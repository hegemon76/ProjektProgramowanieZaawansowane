using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace API.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        public List<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
    }
}
