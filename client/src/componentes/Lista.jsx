import remove from "../assets/remove.png"

//Componente responsável por mostrar a lista de ingredientes adicionado pelo usuário
export function Lista(props){
    const lista = props.ingredientes.map(ing => (
        <li key={ing}>{ing} <button onClick={() => props.removerIngrediente(ing)}><img src={remove} alt="X em um botão vermelho"/></button></li>
    ))

    return(
        <>
            <h3>Ingredientes adicioandos:</h3>
            <ul className="lista-ingredientes">
                {lista}
            </ul>
        </>
    )
}