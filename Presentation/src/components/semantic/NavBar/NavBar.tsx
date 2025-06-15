import styles from './NavBar.module.css'
import { GrHomeRounded } from "react-icons/gr";
import { GrCircleQuestion } from "react-icons/gr";
import { GrCalendar } from "react-icons/gr";
import { GrStarOutline } from "react-icons/gr";
import { GrAchievement } from "react-icons/gr";
import { Link, useParams } from 'react-router-dom';

export const NavBar = () => {

    const { groupId } = useParams();

    return (
        <nav className={styles.root}>
            <ul className={styles.navList}>
                <li className={styles.navItem}>
                    <Link style={{ textDecoration: "none", color: "inherit" }} to={groupId ? `/groupHomePage/${groupId}` : "/userAccount"}>
                        <GrHomeRounded className={styles.icon}/>
                    </Link>
                </li>
                <li className={styles.navItem}>
                    <Link style={{ textDecoration: "none", color: "inherit" }} to={groupId ? `/groupQuizPage/${groupId}` : "#"}>
                        <GrCircleQuestion className={styles.icon}/>
                    </Link>
                </li>
                <li className={styles.navItem}>
                    <Link style={{ textDecoration: "none", color: "inherit" }} to={groupId ? `/groupCalendarPage/${groupId}` : "#"}>
                        <GrCalendar className={styles.icon}/>
                    </Link>
                </li>
                <li className={styles.navItem}>
                    <Link style={{ textDecoration: "none", color: "inherit" }} to={groupId ? `/groupAchievementPage/${groupId}` : "#"}>
                        <GrAchievement className={styles.icon}/>
                    </Link>
                </li>
                <li className={styles.navItem}>
                    <Link style={{ textDecoration: "none", color: "inherit" }} to={groupId ? `/groupWishListPage/${groupId}` : "#"}>
                        <GrStarOutline className={styles.icon}/>
                    </Link>
                </li>
            </ul>
        </nav>   
    )
}