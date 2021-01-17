using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? CompletedAt { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        
    }
}
