import { Header } from '../../../components/semantic/Header/Header'
import { NavBar } from '../../../components/semantic/NavBar/NavBar'
import styles from './GroupHomePage.module.css'

export const GroupHomePage = () => {
    return (
        <div className={styles.root}>
            <Header />

            <NavBar />
        </div>
    )
}