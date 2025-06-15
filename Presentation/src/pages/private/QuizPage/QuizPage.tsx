import { Header } from '../../../components/semantic/Header/Header'
import { NavBar } from '../../../components/semantic/NavBar/NavBar'
import styles from './QuizPage.module.css'

export const QuizPage = () => {
    return (
        <div className={styles.root}>
            <Header />

            <main style={{color: '#000'}}>
                Quiz Page
            </main>

            <NavBar />
        </div>
    )
}