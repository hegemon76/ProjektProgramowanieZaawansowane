using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
        public List<Category> Categories { get; set; } = new List<Category>();

    }
}
