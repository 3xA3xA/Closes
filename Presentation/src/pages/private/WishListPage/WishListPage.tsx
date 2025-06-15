import { Header } from '../../../components/semantic/Header/Header'
import { NavBar } from '../../../components/semantic/NavBar/NavBar'
import styles from './WishListPage.module.css'

export const WishListPage = () => {
    return (
        <div className={styles.root}>
            <Header />

            <main>
                WishListPage
            </main>

            <NavBar />
        </div>
    )
}