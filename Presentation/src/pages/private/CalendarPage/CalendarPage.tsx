import { Header } from '../../../components/semantic/Header/Header'
import { NavBar } from '../../../components/semantic/NavBar/NavBar'
import styles from './CalendarPage.module.css'

export const CalendarPage = () => {
    return (
        <div className={styles.root}>
            <Header />

            <main style={{color: '#000'}}>
                Calendar Page
            </main>

            <NavBar />
        </div>
    )
}