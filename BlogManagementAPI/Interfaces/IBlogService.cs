using BlogManagementApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagementApp_BE.Interfaces
{
    public interface IBlogService
    {
        List<BlogPost> Get(string searchTerm = null, string sortBy = "date");
        BlogPost Get(int id);
        BlogPost AddOrUpdate(BlogPost blog);
        void Remove(int id);
    }

}
