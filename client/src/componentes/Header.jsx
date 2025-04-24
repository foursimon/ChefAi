import chef from "../assets/chef.png"

export function Header(){
    return (
        <header>
            <img src={chef} alt="chef de cozinha em frente a um fundo azul circular" />
            <div>
                <h1>Chef A.I.</h1>
                <p>Consiga receitas usando seus ingredientes</p>
            </div>
        </header>
    )
}