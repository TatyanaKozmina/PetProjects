using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CustomLog
{
    public class CustomLog : ICustomLog
    {
        private readonly RequestDelegate _next;
        private CustomLogOptions _options { get; }

        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public CustomLog(RequestDelegate next, IOptions<CustomLogOptions> options)
        {
            _next = next;
            _options = options.Value;
        }        

        /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            string logFilePath = Path.Combine(_options.LogDirPath, _options.LogFileName);

            // Используем асинхронную запись в файл
            await File.AppendAllTextAsync(logFilePath, 
                                          $"\n{DateTime.Now}" +
                                          $"\t{context.User.Identity.Name}" +
                                          $"\t{context.Request.Method}" +
                                          $"\t{context.Request.Path}");

            // Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }
    }
}
