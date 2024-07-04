using BlogManagementApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagementApp_BE.Interfaces
{
    public interface IBlogService
    {
        List<BlogPost> Get();
        BlogPost Get(int id);
        void Create(BlogPost blog);
        void Remove(int id);
    }

}
