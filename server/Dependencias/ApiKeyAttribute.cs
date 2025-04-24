using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace server.Dependencias
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class ApiKeyAttribute : Attribute, IAuthorizationFilter
	{
		const string nomeHeader = "Api-Key";
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (!ValidarChave(context.HttpContext))
			{
				context.Result = new UnauthorizedResult();
			}
		}

		private bool ValidarChave(HttpContext httpContext)
		{
			string? chave = httpContext.Request.Headers[nomeHeader];
			if (String.IsNullOrWhiteSpace(chave)) return false;
			string chaveCorreta = httpContext.RequestServices
				.GetRequiredService<IConfiguration>()
				.GetValue<string>("APIKEY")!;
			return chave == chaveCorreta;
		}

	}
}
