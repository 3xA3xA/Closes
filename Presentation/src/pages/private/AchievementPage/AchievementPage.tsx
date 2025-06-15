import { Header } from '../../../components/semantic/Header/Header'
import { NavBar } from '../../../components/semantic/NavBar/NavBar'
import styles from './AchievementPage.module.css'

export const AchievementPage = () => {
    return (
        <div className={styles.root}>
            <Header />

            <main style={{color: '#000'}}>
                Achievement Page
            </main>

            <NavBar />
        </div>
    )
}