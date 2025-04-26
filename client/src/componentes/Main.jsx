import {useState} from "react"
import { Lista } from "./Lista"
import { Receita } from "./Receita"
import recipe from "../assets/receita.png"
export function Main(){
    //state responsável por armazenar os ingredientes adicionados pelo usuário
    const [ingredientes, setIngredientes] = useState([])
    //Método responsável por salvar novos ingredientes adicionados pelo usuário no state "ingredientes"
    function adicionarIngrediente(formData){
        const dados = formData.get("ingrediente")
        //Como não é permitido mutar um state, crio um novo vetor com os ingredientes antigos e com o ingrediente novo.
        setIngredientes(prev => [...prev, dados])
    }
    function removerIngrediente(chave){
        //Aqui, crio uma nova lista sem o ingrediente que foi removido pelo usuário
        setIngredientes(prev => {
            const itens = prev.filter((ing) => ing !== chave)
            return itens
        })
    }
    return (
        <main>
            <div className="title-container">
                <h1>Bem vindo ao Chef A.I.  <img src={recipe} alt="livro de receitas com um chapéu de cozinheiro chefe estampado na capa" /></h1>
                <h3>Insira pelo menos três ingredientes no campo abaixo para gerar sua receita</h3>
            </div>
            <section className="ingredientes-section">
                <form action={adicionarIngrediente}>
                    <label htmlFor="ingrediente" aria-label="Ingredientes">Ingredientes:</label>
                    <input placeholder="arroz, frango, etc..." 
                        name="ingrediente" id="ingrediente" autoFocus="true" type="text" required />
                    <button>Adicionar ingrediente</button>
                </form>
            {/* Defino que a lista só irá aparecer se o tamanho dela for maior que 0 */}
            {ingredientes.length > 0 ? <Lista ingredientes={ingredientes} removerIngrediente={removerIngrediente} /> : undefined}
            </section>
            <Receita ingredientes={ingredientes}/>
        </main>
    )
}