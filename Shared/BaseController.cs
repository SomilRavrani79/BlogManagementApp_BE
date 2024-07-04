using BlogManagementApp_BE.models;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagementApp_BE.Shared
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ActionResult GenericResponse<T>(T data, string statusMessage, int statusCode)
        {
            return Ok(new GenericResponse<T>
            {
                StatusMessage = statusMessage,
                Data = data,
                StatusCode = statusCode
            });
        }
    }
}
