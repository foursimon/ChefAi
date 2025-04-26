import { gerarReceita } from "../api.js"
import { useState } from "react"
import ReactMarkdown from 'react-markdown'
export function Receita(props){
    const [receita, setReceita] = useState()
    const [carregar, setCarregar] = useState(false)
    async function pegarReceita(){
        setCarregar(true)
        const resposta = await gerarReceita(props.ingredientes)
        setCarregar(false)
        setReceita(resposta)
    }
    return(
        <>
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
                {carregar ? <div className="carregando"></div> : <div><ReactMarkdown>{receita}</ReactMarkdown></div>}
            </section>
        </>
    )
}