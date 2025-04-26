import '../assets/styles.css'
import { Header } from './Header'
import {Main} from "./Main"
//Defino um componente responsável por renderizar os outros componentes que irão formar a página completa
export default function App() {

  return (
    <>
      <Header />
      <Main />
    </>
  )
}

