
//Método responsável para realizar a requisiçâo ao backend e pegar a receita gerada.
export async function gerarReceita(lista){
    try{
        //realizo a requisição usando o método fetch
        const resposta = await fetch("https://localhost:7287/api/ChefAi",{
            //método da requisição HTTP
            method: 'POST',
            //cabeçalhos para o backend verificar o tipo de conteúdo recebido
            headers: {
                'Content-Type': 'application/json'
            },
            //utilizo o modo cors para o backend validar a requisição
            mode: "cors",
            //enviando os ingredientes no corpo da requisição HTTP
            body: JSON.stringify({
                ingredientes: [...lista]
            })
        })
        //se a resposta não for válida, o backend irá retornar um JSON explicando o erro.
        if(!resposta.ok){
            const corpo = await resposta.json()
            throw new Error(JSON.stringify(corpo, null, 4))
        }
        //se a resposta for válida, é retornado um texto
        return await resposta.text()
    }catch(error){
        return error
    }
}