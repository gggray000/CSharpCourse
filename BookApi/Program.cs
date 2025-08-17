namespace BookApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // add services
            builder.Services.AddControllers();

            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            // add mapping
            app.MapControllers();

            app.MapGet("/", () =>
            {
                return Results.Redirect("/api/books");
            });


            app.Run();
        }
    }
}
