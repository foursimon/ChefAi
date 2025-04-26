using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using server.Models;
using server.Repositorios;
using System.Net;

namespace server.Controllers
{
	[Route("api/[controller]")]
	[EnableRateLimiting("fixoPorIp")]
	[ApiController]
	public class ChefAiController(IChefAiRepositorio _chefAi) : ControllerBase
	{
		[HttpPost]
		[ProducesResponseType(200)]
		public async Task<ActionResult<string>> PegarReceita([FromBody]ReceitaAi ingredientes)
		{
			try
			{
				var resposta = await _chefAi.PegarReceita(ingredientes);
				return Ok(resposta);
			}
			catch(ArgumentException ex)
			{
				return BadRequest(new ProblemDetails
				{
					Title = "Lista de ingredientes pequena",
					Status = StatusCodes.Status400BadRequest,
					Type = "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Reference/Status/400",
					Detail = ex.Message
				});
			}
			catch (Exception ex)
			{
				return BadRequest(new ProblemDetails
				{
					Title = "Algo deu errado",
					Status = StatusCodes.Status400BadRequest,
					Type = "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Reference/Status/400",
					Detail = ex.Message
				});
			}
		}
	}
}
