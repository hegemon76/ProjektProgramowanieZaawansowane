using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _contex;

        public UsersController(DataContext contex)
        {
            _contex = contex;
        }


        // api/users
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _contex.Users.ToListAsync();
        }

        // api/users/2
        // [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            //var user = _contex.Users.Where(u => u.UserName.ToLower() == username.ToLower())
            //    .Include(t => t.ToDoItems)
            //    .ThenInclude(c => c.Category)
            //    .Include(x => x.Categories)
            //    .FirstOrDefaultAsync();

            //return await user;
            var user = _contex.Users.First(x => x.Id == id);

            return await _contex.Users.FindAsync(id);

        }

        //[Authorize]
        [HttpGet("{username}/categories")]
        public ActionResult<List<UserCategoriesDto>> GetUserCategories(string username)
        {
            var user = _contex.Users.Where(u => u.UserName.ToLower() == username.ToLower())
                .Include(c => c.Categories)
                .ThenInclude(t => t.ToDoItems)
                .FirstOrDefault();

            if (user.Categories.Count == 0) return Ok();

            var cats = user.Categories.Select(item =>
            new UserCategoriesDto
            {
                Category = item.Name,
                ToDos = item.ToDoItems.Count,
                Id = item.Id
            }).ToList();

            return cats;
        }

        [HttpGet("{username}/items")]
        public ActionResult<List<ToDoItemDto>> GetUserToDoItems(string username)
        {
            var user = _contex.Users.Where(u => u.UserName.ToLower() == username.ToLower())
                .Include(c => c.ToDoItems)
                .ThenInclude(x => x.Category)
                .FirstOrDefault();

            var now = DateTime.Now.AddMinutes(-2);

            var todos = user.ToDoItems
                .Where(c => c.IsDone == false || DateTime.Compare(now, c.CompletedAt.GetValueOrDefault()) < 0)
                .Select(item =>
                        new ToDoItemDto
                        {
                            IsCompleted = item.IsDone,
                            Id = item.Id,
                            Description = item.Description,
                            Category = item.Category.Name,
                            Created = item.CreatedAt.ToString("f"),
                            Completed = item.CompletedAt.ToString()
                        }).OrderBy(completed => completed.IsCompleted)
            .ThenBy(x => x.Category)
            .ThenBy(y => y.Created)
                .ToList();

            return todos;
        }

        [HttpGet("{username}/completed/items")]
        public ActionResult<List<ToDoItemDto>> GetUserCompletedToDoItems(string username)
        {
            var user = _contex.Users.Where(u => u.UserName.ToLower() == username.ToLower())
                .Include(c => c.ToDoItems)
                .ThenInclude(x => x.Category)
                .FirstOrDefault();

            var now = DateTime.Now.AddMinutes(-2);

            var todos = user.ToDoItems
                .Where(c => c.IsDone == true && DateTime.Compare(now,c.CompletedAt.GetValueOrDefault()) > 0 )
                .Select(item =>
                new ToDoItemDto
                {
                    IsCompleted = item.IsDone,
                    Id = item.Id,
                    Description = item.Description,
                    Category = item.Category.Name,
                    Created = item.CreatedAt.ToString("f"),
                    Completed = item.CompletedAt.ToString()
                })//.OrderBy(completed => completed.IsCompleted)
                .OrderByDescending(date => date.Completed)
            .ThenBy(x => x.Category)
            .ThenBy(y => y.Created)
                .ToList();



            return todos;
        }

        [HttpGet("user/{category}")]
        public async Task<ActionResult<List<ToDoItemFromCategoryDto>>> GetCategoryItems(string category)
        {
            var _category = await _contex.Categories
                .Where(x => x.Name.ToLower() == category.ToLower())
                .Include(t => t.ToDoItems)
                .FirstOrDefaultAsync();
            var todos = _category.ToDoItems.Select(item =>
            new ToDoItemFromCategoryDto
            {
                Description = item.Description,
                CreatedAt = item.CreatedAt.ToString("f")
            }).ToList();

            return todos;
        }
    }
}
