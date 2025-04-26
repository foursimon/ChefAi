export async function gerarReceita(lista){
    try{
        const resposta = await fetch("https://localhost:7287/api/ChefAi",{
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            mode: "cors",
            body: JSON.stringify({
                ingredientes: [...lista]
            })
        })
        if(!resposta.ok){
            const corpo = await resposta.json()
            throw new Error(JSON.stringify(corpo, null, 4))
        }
        return await resposta.text()
    }catch(error){
        return error
    }
}