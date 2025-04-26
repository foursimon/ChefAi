
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
            //Adicionando as dependências especificadas no método AddDependecies()
            //da classe DependeciesConfig.cs
            builder.AddDependecies();
            
            var app = builder.Build();

            //Adicionando a documentação API do método AddApiDocumentation()
            //da classe DependeciesConfig.cs
            app.AddApiDocumentation();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
