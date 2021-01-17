using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> AddCategory(CategoryDto categoryDto)
        {
            var user = _context.Users
                .Include(c => c.Categories)
                .FirstOrDefault(x => x.UserName.ToLower() == categoryDto.UserName.ToLower());
            
            var newCat = user.Categories
                .Exists(x => x.Name.ToLower() == categoryDto.Name.ToLower());

            if (newCat) return Conflict();

            var category = new Category
            {
                AppUser = user,
                AppUserId = user.Id,
                Name = categoryDto.Name,
            };

            _context.Categories.Add(category);

            await AddCategoryToUser(user.Id, category);

            _context.SaveChanges();

            return new CategoryDto
            {
                UserName = categoryDto.UserName,
                Name = categoryDto.Name,
            };
        }

        private async Task<AppUser> AddCategoryToUser(int id, Category category)
        {
            var user = await _context.Users.SingleAsync(x => x.Id == id);
            user.Categories.Add(category);
            await _context.SaveChangesAsync();

            return user;
        }

        //[HttpGet("/user")]
        //public ActionResult<UserCategoriesDto> GetCategoryFromUser([FromBody]int id)
        //{
        //    var user = _context.Users.Where(x => x.Id == id).SingleOrDefault();

        //    var categories = new UserCategoriesDto
        //    {
        //        Categories = new List<Category>()
        //    };
        //    categories.Categories.AddRange(user.Categories);
        //    return categories; 
        //}

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var category = await _context.Categories.Include(t => t.ToDoItems).FirstAsync(c => c.Id == id);

            _context.Remove(category);

            _context.SaveChanges();

            return true;
        }
    }
}
