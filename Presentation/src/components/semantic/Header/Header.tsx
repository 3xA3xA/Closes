import { AiFillHeart } from "react-icons/ai";
import { FaRegUserCircle } from "react-icons/fa";
import styles from './Header.module.css'

export const Header = () => {
    return (
        <header className={styles.root}>
            <div className={styles.iconGroup}>
                <AiFillHeart className={styles.icon}/>
                <div>Closes</div>
            </div>

            <FaRegUserCircle className={styles.userAccountLink}/>
        </header>
    )
}