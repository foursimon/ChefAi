
using Microsoft.Extensions.AI;
using server.Models;

namespace server.Repositorios
{
	public class ChefAiRepositorio : IChefAiRepositorio
	{
		//Dessa forma, terei acesso a IA para utilizar em meus métodos.
		private readonly IChatClient _chatClient;
		//Especificando uma função para a IA saber o que deve fazer. Neste caso, gerar receitas com base
		//nos ingredientes dos usuários.
		const string prompt = $$"""
			Você é um assistente que sugere uma receita baseada na lista de ingredientes que o usuário possui.
			A receita pode incluir alguns ingredientes adicionais, mas tente não adicionar muitos ingredientes novos à receita.
			Formate a resposta em Markdown para ser mais fácil renderizar no site web.
			""";

		//injentando a IA definida em DependeciesConfig.cs no construtor desta classe.
		public ChefAiRepositorio(IChatClient chatClient)
		{
			_chatClient = chatClient;
		}
		//Método criado para gerar a receita com base na lista recebida
		public async Task<string> PegarReceita(ReceitaAi lista)
		{
			//verificando se a lista de ingredientes possui pelo menos três ingredientes para ter 
			//melhores resultados.
			if (lista.Ingredientes.Length < 3) 
				throw new ArgumentException("A lista de ingredientes deve conter pelo menos três itens");
			//definindo uma variável para concatenar a lista de ingredientes em uma única string
			string ingredientes = "";
			foreach(string item in lista.Ingredientes)
			{
				ingredientes = ingredientes + ", " + item;
			}
			//Esperando e armazenando a resposta da IA
			var resposta = await _chatClient.GetResponseAsync([
				//Definindo o comportamento da IA usando o prompt que defini mais cedo.
				new ChatMessage(ChatRole.System, prompt),
				//Realizando a requisição para a IA
				new ChatMessage(ChatRole.User, $"Consegue recomendar uma receita para mim usando esses ingredientes: {ingredientes}?")
			]);
			//Retornando a resposta como texto
			return resposta.Text;
		}
	}
}
