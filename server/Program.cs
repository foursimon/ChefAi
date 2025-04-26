
using server.Dependencias;

namespace server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            //Adicionando as depend�ncias especificadas no m�todo AddDependecies()
            //da classe DependeciesConfig.cs
            builder.AddDependecies();
            
            var app = builder.Build();

            //Adicionando a documenta��o API do m�todo AddApiDocumentation()
            //da classe DependeciesConfig.cs
            app.AddApiDocumentation();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
