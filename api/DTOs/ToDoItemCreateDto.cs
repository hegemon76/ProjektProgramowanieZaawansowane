using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class ToDoItemCreateDto
    {
        public string UserName { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
