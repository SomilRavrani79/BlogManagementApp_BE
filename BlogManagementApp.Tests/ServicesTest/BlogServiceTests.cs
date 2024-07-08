using BlogManagementApp.Controllers;
using System;
using BlogManagementApp.Controllers;
using BlogManagementApp.Models;
using BlogManagementApp.Services;
using BlogManagementApp_BE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;
using BlogManagementApp_BE.Interfaces;
using BlogManagementApp_BE.models;

namespace BlogManagementApp.Tests.ServicesTest
{
    public class BlogControllerTests
    {
        private readonly BlogController _controller;
        private readonly Mock<IBlogService> _blogServiceMock;

        public BlogControllerTests()
        {
            _blogServiceMock = new Mock<IBlogService>();
            _controller = new BlogController(_blogServiceMock.Object);
        }

        [Fact]
        public void Get_ShouldReturnAllBlogs()
        {
            // Arrange
            var blogs = new List<BlogPost>
            {
                new BlogPost { Id = 1, Username = "testuser1", Text = "test text 1", DateCreated = System.DateTime.Now },
                new BlogPost { Id = 2, Username = "testuser2", Text = "test text 2", DateCreated = System.DateTime.Now }
            };
            _blogServiceMock.Setup(s => s.Get()).Returns(blogs);

            // Act
            var result = _controller.Get().Result as OkObjectResult;
            var response = result?.Value as GenericResponse<List<BlogPost>>;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(blogs.Count, response?.Data.Count);
        }

        [Fact]
        public void Create_ShouldReturnCreatedBlog()
        {
            // Arrange
            var newBlog = new BlogPost { Id = 3, Username = "testuser", Text = "test text", DateCreated = System.DateTime.Now };
            _blogServiceMock.Setup(s => s.Create(newBlog));

            // Act
            var result = _controller.Create(newBlog).Result as OkObjectResult;
            var response = result?.Value as GenericResponse<BlogPost>;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(201, response?.StatusCode);
            Assert.Equal(newBlog.Username, response?.Data.Username);
        }

        [Fact]
        public void Delete_WithValidId_ShouldReturnSuccess()
        {
            // Arrange
            var blog = new BlogPost { Id = 1, Username = "testuser", Text = "test text", DateCreated = System.DateTime.Now };
            _blogServiceMock.Setup(s => s.Get(1)).Returns(blog);
            _blogServiceMock.Setup(s => s.Remove(1));

            // Act
            var result = _controller.Delete(1) as OkObjectResult;
            var response = result?.Value as GenericResponse<BlogPost>;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("Blog removed successfully", response?.StatusMessage);
        }

        [Fact]
        public void Delete_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _blogServiceMock.Setup(s => s.Get(999)).Returns((BlogPost)null);

            // Act
            var result = _controller.Delete(999) as OkObjectResult;
            var response = result?.Value as GenericResponse<BlogPost>;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(404, response?.StatusCode);
            Assert.Equal("Blog not found", response?.StatusMessage);
        }
    }
}
