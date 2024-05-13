namespace Talabat.APIs.Extensions
{
    public static class AddSwaggerExtensions
    {
        public static WebApplication AddSwaggerExtension(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
