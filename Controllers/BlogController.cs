using Microsoft.AspNetCore.Mvc;
using BlogManagementApp.Services;
using BlogManagementApp.Models;
using System.Collections.Generic;
using BlogManagementApp_BE.models;
using BlogManagementApp_BE.Shared;
using BlogManagementApp_BE.Interfaces;

namespace BlogManagementApp.Controllers
{
    [Route("api/[controller]")]
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public ActionResult<GenericResponse<List<BlogPost>>> Get()
        {
            var blogs = _blogService.Get();
            return GenericResponse(blogs, "Blogs retrieved successfully", 200);
        }

        [HttpPost]
        public ActionResult<GenericResponse<BlogPost>> Create(BlogPost blog)
        {
            _blogService.Create(blog);
            return GenericResponse(blog, "Blog created successfully", 201);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var blog = _blogService.Get(id);

            if (blog == null)
            {
                return GenericResponse<BlogPost>(null, "Blog not found", 404);
            }

            _blogService.Remove(blog.Id);
            return GenericResponse<BlogPost>(blog, "Blog removed successfully", 200);
        }
    }
}
