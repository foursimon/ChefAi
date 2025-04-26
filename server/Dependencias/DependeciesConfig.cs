using Azure;
using Azure.AI.Inference;
using Microsoft.Extensions.AI;
using Scalar.AspNetCore;
using server.Repositorios;
using System.Runtime.CompilerServices;
using System.Threading.RateLimiting;

namespace server.Dependencias
{
	//Classe dedicada a configurar dependências deste projeto.
	//Faço dessa forma para reduzir a quantidade de código no arquivo Program.cs
	public static class DependeciesConfig
	{
		public static void AddDependecies(this WebApplicationBuilder builder)
		{
			//Adicionando uma limite de taxa de requisições
			builder.Services.AddRateLimiter(opt =>
			{
				//Definindo o código de rejeição para 429 ao ultrapassar o limite definido
				opt.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
				//Adicionando uma política de limite de taxa de requisição com o nome "fixoPorIp"
				//O limite de taxa é aplicado pelo endereço IP do dispositivo do usuário, pois
				//não quero que todos os usuários que não chegaram no limite de requisição sejam afetados.
				opt.AddPolicy("fixoPorIp", httpContext =>
					//Utilizo o método GetFixedWindowLimiter da classe RateLimitPartition para utilizar o 
					//algoritmo de janela fixa através do endereço Ip remoto.
					RateLimitPartition.GetFixedWindowLimiter(
						partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
						factory: _ => new FixedWindowRateLimiterOptions
						{
							//PermitLimit define a quantidade de requisições permitidas em uma determinada
							//janela de tempo.
							PermitLimit = 3,
							//Window é a janela de tempo que, neste caso, é de dois minutos.
							Window = TimeSpan.FromMinutes(2)
						}
					)
				);
			});
			//Adicionado Política Cors para definir qual endereço web pode realizar requisições a esta API
			builder.Services.AddCors(policy =>
			{
				//Adiciono uma política padrão para a API
				policy.AddDefaultPolicy(opt =>
				{
					//Permiti qualquer cabeçalho e qualquer método por se tratar de um projeto pessoal.
					//No entanto, caso eu fosse colocá-lo em produção (disponibilizar essa API para uso),
					//eu especificaria os métodos e os cabeçalhos permitidos
					opt.AllowAnyHeader();
					opt.AllowAnyMethod();
					opt.WithOrigins("http://localhost:5173");
				});
			});
			//Adicionando um serviço com escopo definido das minhas classes em Repositorios para serem inseridas
			//como dependência do controlador.
			builder.Services.AddScoped<IChefAiRepositorio, ChefAiRepositorio>();
			//Definindo o modelo da IA disponibilizada em GitHub Models
			var model = "deepseek/DeepSeek-V3-0324";
			//Definindo a Uri do modelo da IA
			var uri = new Uri("https://models.github.ai/inference");
			//Utilizando meu Token do GitHub para autorizarem o uso da IA
			var credencial = new AzureKeyCredential(builder.Configuration["GITHUBTOKEN"]!);
			//Adicionado um serviço de IA com as dependências Microsoft.Extensions.AI e Azure.AI.Inference
			builder.Services.AddSingleton(
				new Azure.AI.Inference.ChatCompletionsClient(uri, credencial));
			//Adicionado a IA para ser utilizada como dependência em meu repositório
			builder.Services.AddChatClient(services => 
				services.GetRequiredService<ChatCompletionsClient>().AsIChatClient(model));
		}
		//Adicionando minha documnetação API
		public static void AddApiDocumentation(this WebApplication app)
		{
			app.MapOpenApi();
			//Mapeando e estilizando a documentação Scalar
			app.MapScalarApiReference(opt =>
			{
				opt.Title = "AI API";
				opt.Theme = ScalarTheme.BluePlanet;
				opt.WithDefaultHttpClient(ScalarTarget.Node, ScalarClient.Fetch);
			});
			//Habilitando o limite de taxa de requisição e a política CORS
			app.UseRateLimiter();
			app.UseCors();
		}
	}
}
