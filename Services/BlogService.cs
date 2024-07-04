using BlogManagementApp.Models;
using BlogManagementApp_BE.Interfaces;
using Newtonsoft.Json;
using System.Xml;

namespace BlogManagementApp.Services
{
    public class BlogService : IBlogService
    {
        private readonly string _filePath = "blogs.json";
        private List<BlogPost> _blogs;
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

        public List<BlogPost> Get()
        {
            _logger.LogInformation("Retrieving all blogs from the repository.");
            return _blogs;
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
