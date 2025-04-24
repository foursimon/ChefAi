using server.Models;

namespace server.Repositorios
{
	public interface IChefAiRepositorio
	{
		public Task<string> PegarReceita(ReceitaAi lista);
	}
}
