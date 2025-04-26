using Azure;
using Azure.AI.Inference;
using Microsoft.Extensions.AI;
using Scalar.AspNetCore;
using server.Repositorios;
using System.Runtime.CompilerServices;
using System.Threading.RateLimiting;

namespace server.Dependencias
{
	public static class DependeciesConfig
	{
		public static void AddDependecies(this WebApplicationBuilder builder)
		{
			builder.Services.AddRateLimiter(opt =>
			{
				opt.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
				opt.AddPolicy("fixoPorIp", httpContext =>
					RateLimitPartition.GetFixedWindowLimiter(
						partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
						factory: _ => new FixedWindowRateLimiterOptions
						{
							PermitLimit = 3,
							Window = TimeSpan.FromMinutes(2)
						}
					)
				);
			});
			builder.Services.AddCors(policy =>
			{
				policy.AddDefaultPolicy(opt =>
				{
					opt.AllowAnyHeader();
					opt.AllowAnyMethod();
					opt.WithOrigins("http://localhost:5173");
				});
			});
			builder.Services.AddScoped<IChefAiRepositorio, ChefAiRepositorio>();
			var model = "deepseek/DeepSeek-V3-0324";
			var uri = new Uri("https://models.github.ai/inference");
			var credencial = new AzureKeyCredential(builder.Configuration["GITHUBTOKEN"]!);
			builder.Services.AddSingleton(
				new Azure.AI.Inference.ChatCompletionsClient(uri, credencial));
			builder.Services.AddChatClient(services => 
				services.GetRequiredService<ChatCompletionsClient>().AsIChatClient(model));
		}
		public static void AddApiDocumentation(this WebApplication app)
		{
			app.MapOpenApi();
			app.MapScalarApiReference(opt =>
			{
				opt.Title = "AI API";
				opt.Theme = ScalarTheme.BluePlanet;
				opt.WithDefaultHttpClient(ScalarTarget.Node, ScalarClient.Fetch);
			});
			app.UseRateLimiter();
			app.UseCors();
		}
	}
}
