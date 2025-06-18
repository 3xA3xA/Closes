import { AiFillHeart } from "react-icons/ai";
import { FaRegUserCircle } from "react-icons/fa";
import styles from './Header.module.css'

interface HeaderProps {
    title?: string
}

export const Header: React.FC<HeaderProps> = ({ title }) => {
    return (
        <header className={styles.root}>
            <div className={styles.iconGroup}>
                <AiFillHeart className={styles.icon}/>
                <div>{title ? title : 'Closes'}</div>
            </div>

            <FaRegUserCircle className={styles.userAccountLink}/>
        </header>
    )
}