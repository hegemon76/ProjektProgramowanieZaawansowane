using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace API.DTOs
{
    public class ToDoItemDto
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Created { get; set; }
        public string Completed { get; set; }
    }
}
