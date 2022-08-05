using Microsoft.AspNetCore.Http;

namespace CustomLog
{
    public interface ICustomLog
    {
        Task InvokeAsync(HttpContext context);
    }
}