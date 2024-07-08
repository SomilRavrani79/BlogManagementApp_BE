using BlogManagementApp.Models;
using BlogManagementApp_BE.Interfaces;
using BlogManagementApp_BE.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Xml;

namespace BlogManagementApp.Services
{
    public class BlogService : IBlogService
    {
        private readonly string _filePath = "blogs.json";
        private readonly List<BlogPost> _blogs = new();
        private readonly ILogger<BlogService> _logger;

        public BlogService(ILogger<BlogService> logger)
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _blogs = JsonConvert.DeserializeObject<List<BlogPost>>(json) ?? new List<BlogPost>();
            }
            else
            {
                _blogs = new List<BlogPost>();
            }
            _logger = logger;
        }
        public List<BlogPost> Get(string searchTerm = null, string sortBy = "date", string direction ="desc")
        {
            _logger.LogInformation("Retrieving all blogs from the repository.");
            var blogs = _blogs.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                blogs = blogs.Where(b => b.Username.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                         b.Text.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            switch (sortBy.ToLower())
            {
                case "username":
                    blogs = direction == "desc" ? blogs.OrderByDescending(b => b.Username) :  blogs.OrderBy(b => b.Username);
                    break;
                case "date":
                default:
                    blogs = direction == "desc" ? blogs.OrderByDescending(b => b.DateCreated)  :  blogs.OrderBy(b => b.DateCreated);
                    break;
            }

            return blogs.ToList();
        }

        public BlogPost Get(int id)
        {
            _logger.LogInformation($"Retrieving blog with id {id} from the repository.");
            return _blogs.FirstOrDefault(blog => blog.Id == id);
        }

        public void Create(BlogPost blog)
        {
            _logger.LogInformation("Adding a new blog to the repository.");
            blog.Id = _blogs.Count > 0 ? _blogs.Max(b => b.Id) + 1 : 1;
            _blogs.Add(blog);
            SaveToFile();
        }

        public BlogPost AddOrUpdate(BlogPost blog)
        {
            if (blog.Id == 0)
            {
                _logger.LogInformation("Adding a new blog to the repository.");
                blog.Id = _blogs.Count > 0 ? _blogs.Max(b => b.Id) + 1 : 1;
                _blogs.Add(blog);
            }
            else
            {
                _logger.LogInformation($"Updating blog with id {blog.Id}.");
                var index = _blogs.FindIndex(b => b.Id == blog.Id);
                if (index != -1)
                {
                    _blogs[index] = blog;
                }
            }

            SaveToFile();
            return blog;
        }


        public void Remove(int id)
        {
            _logger.LogInformation($"Removing blog with id {id} from the repository.");
            var blog = _blogs.FirstOrDefault(b => b.Id == id);
            if (blog != null)
            {
                _blogs.Remove(blog);
                SaveToFile();
            }
        }

        private void SaveToFile()
        {
            var json = JsonConvert.SerializeObject(_blogs, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}
