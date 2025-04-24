
using Microsoft.Extensions.AI;
using server.Models;

namespace server.Repositorios
{
	public class ChefAiRepositorio : IChefAiRepositorio
	{
		private readonly IChatClient _chatClient;
		const string prompt = $$"""
			Você é um assistente que sugere uma receita baseada na lista de ingredientes que o usuário possui.
			A receita pode incluir alguns ingredientes adicionais, mas tente não adicionar muitos ingredientes novos à receita.
			Formate a resposta em Markdown para ser mais fácil renderizar no site web.
			""";
		public ChefAiRepositorio(IChatClient chatClient)
		{
			_chatClient = chatClient;
		}
		public async Task<string> PegarReceita(ReceitaAi lista)
		{
			if (lista.Ingredientes.Length < 3) 
				throw new ArgumentException("A lista de ingredientes deve conter pelo menos três itens");
			string ingredientes = "";
			foreach(string item in lista.Ingredientes)
			{
				ingredientes = ingredientes + "" + item;
			}
			var resposta = await _chatClient.GetResponseAsync([
				new ChatMessage(ChatRole.System, prompt),
				new ChatMessage(ChatRole.User, ingredientes)
			]);
			return resposta.Text;
		}
	}
}
