using System.ComponentModel.DataAnnotations;

namespace server.Models
{
	public class ReceitaAi
	{
		[Required(ErrorMessage = "Insira os ingredientes na lista")]
		public required string[] Ingredientes { get; set; }
	}
}
