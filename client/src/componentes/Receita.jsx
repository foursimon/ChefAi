import { gerarReceita } from "../api.js"
import { useState } from "react"
import ReactMarkdown from 'react-markdown'
export function Receita(props){
    //State responsável por armazenar a receita gerada pela IA no backend
    const [receita, setReceita] = useState()
    //State responsável por definir se a animação de carregamento deverá aparecer
    const [carregar, setCarregar] = useState(false)
    async function pegarReceita(){
        //mostro a animação de carregando para indicar ao usuário que sua receita está sendo processada
        setCarregar(true)
        //pego a receita gerada pela IA do backend
        const resposta = await gerarReceita(props.ingredientes)
        //removo a animação de carregando, pois a receita foi gerada
        setCarregar(false)
        //mostro a receita na tela para o usuário
        setReceita(resposta)
    }
    return(
        <>
            {/* Só mostro a opção de pegar uma receita se a lista de ingredientes tiver pelo menos três itens */}
            {props.ingredientes.length >= 3 ?
            <section className="receita-section">  
                {carregar ? 
                    <div>
                        <h2>O chef está preparando sua receita. Aguarde um momento</h2>
                    </div> :
                <>
                    <div>
                        <h2>Está pronto para pegar sua receita?</h2>
                        <p>Clique no botão ao lado para chamar o chef:</p>
                    </div> 
                    <button onClick={pegarReceita}>Pegar receita</button>
                </>}
            </section> : undefined}
            <section className="receita-gerada">
                {carregar ? <div className="carregando"></div> : 
                <div className={receita ? "border": undefined}><ReactMarkdown>{receita}</ReactMarkdown></div>}
            </section>
        </>
    )
}