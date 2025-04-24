export function Receita(){
    function pegarReceita(){
        console.log("clicou")
    }
    return(
        <section className="receita-section">
            <div>
                <h2>Está pronto para pegar sua receita?</h2>
                <p>Clique no botão ao lado para chamar o chef:</p>
            </div>
           <button onClick={pegarReceita}>Pegar receita</button>
        </section>
    )
}