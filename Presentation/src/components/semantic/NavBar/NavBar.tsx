import styles from './NavBar.module.css'
import { GrHomeRounded } from "react-icons/gr";
import { GrCircleQuestion } from "react-icons/gr";
import { GrCalendar } from "react-icons/gr";
import { GrStarOutline } from "react-icons/gr";
import { GrAchievement } from "react-icons/gr";

export const NavBar = () => {
    return (
        <nav className={styles.root}>
            <ul className={styles.navList}>
                <li className={styles.navItem}>
                    <GrHomeRounded className={styles.icon}/>
                </li>
                <li className={styles.navItem}>
                    <GrCircleQuestion className={styles.icon}/>
                </li>
                <li className={styles.navItem}>
                    <GrCalendar className={styles.icon}/>
                </li>
                <li className={styles.navItem}>
                    <GrAchievement className={styles.icon}/>
                </li>
                <li className={styles.navItem}>
                    <GrStarOutline className={styles.icon}/>
                </li>
            </ul>
        </nav>  
    )
}