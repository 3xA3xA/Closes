namespace API.Middlewares
{
    /// <summary>
    /// Расширения для регистрации middleware глобальной обработки исключений.
    /// </summary>
    public static class GlobalExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Регистрирует middleware для глобальной обработки исключений.
        /// </summary>
        /// <param name="builder">Построитель приложения.</param>
        /// <returns>Объект IApplicationBuilder для дальнейшей конфигурации.</returns>
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
