using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class UserCategoriesDto
    {
        public string Category { get; set; }
        public int ToDos { get; set; }
        public int Id { get; set; }
    }
}
