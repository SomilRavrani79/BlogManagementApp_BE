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

        [HttpPost("GetData")]
        public ActionResult<GenericResponse<object>> Get([FromBody] SearchParams searchParams)
        {
            if (searchParams.Id.HasValue)
            {
                var blog = _blogService.Get(searchParams.Id.Value);
                if (blog == null)
                {
                    return NotFound(new GenericResponse<object>
                    {
                        StatusMessage = "Blog not found",
                        Data = null,
                        StatusCode = 404
                    });
                }

                return Ok(new GenericResponse<object>
                {
                    StatusMessage = "Blog retrieved successfully",
                    Data = blog,
                    StatusCode = 200
                });
            }
            else
            {
                var allBlogs = _blogService.Get(searchParams.SearchTerm, searchParams.SortBy);
                var paginatedBlogs = allBlogs.Skip((searchParams.PageNumber - 1) * searchParams.PageSize).Take(searchParams.PageSize).ToList();
                var totalItems = allBlogs.Count();

                var result = new PaginatedResult<BlogPost>
                {
                    Items = paginatedBlogs,
                    TotalCount = totalItems,
                    PageSize = searchParams.PageSize,
                    PageNumber = searchParams.PageNumber
                };

                return Ok(new GenericResponse<PaginatedResult<BlogPost>>
                {
                    StatusMessage = "Blogs retrieved successfully",
                    Data = result,
                    StatusCode = 200
                });
            }
        }

        [HttpPost]
        public ActionResult<GenericResponse<BlogPost>> Create(BlogPost blog)
        {
            _blogService.AddOrUpdate(blog);
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
