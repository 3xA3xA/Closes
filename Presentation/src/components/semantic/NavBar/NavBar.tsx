import styles from './NavBar.module.css'
import { GrHomeRounded } from "react-icons/gr";
import { GrCircleQuestion } from "react-icons/gr";
import { GrCalendar } from "react-icons/gr";
import { GrStarOutline } from "react-icons/gr";
import { GrAchievement } from "react-icons/gr";
import { Link, useParams } from 'react-router-dom';

export const NavBar = () => {

    const { groupId } = useParams();
    const { groupMemberId } = useParams();

    return (
        <nav className={styles.root}>
            <ul className={styles.navList}>
                <li className={styles.navItem}>
                    <Link style={{ textDecoration: "none", color: "inherit" }} to={groupId && groupMemberId ? `/groupHomePage/${groupId}/${groupMemberId}` : "/userAccount"}>
                        <GrHomeRounded className={styles.icon}/>
                    </Link>
                </li>
                <li className={styles.navItem}>
                    <Link style={{ textDecoration: "none", color: "inherit" }} to={groupId && groupMemberId ? `/groupQuizPage/${groupId}/${groupMemberId}` : "#"}>
                        <GrCircleQuestion className={styles.icon}/>
                    </Link>
                </li>
                <li className={styles.navItem}>
                    <Link style={{ textDecoration: "none", color: "inherit" }} to={groupId && groupMemberId ? `/groupCalendarPage/${groupId}/${groupMemberId}` : "#"}>
                        <GrCalendar className={styles.icon}/>
                    </Link>
                </li>
                <li className={styles.navItem}>
                    <Link style={{ textDecoration: "none", color: "inherit" }} to={groupId && groupMemberId ? `/groupAchievementPage/${groupId}/${groupMemberId}` : "#"}>
                        <GrAchievement className={styles.icon}/>
                    </Link>
                </li>
                <li className={styles.navItem}>
                    <Link style={{ textDecoration: "none", color: "inherit" }} to={groupId && groupMemberId ? `/groupWishListPage/${groupId}/${groupMemberId}` : "#"}>
                        <GrStarOutline className={styles.icon}/>
                    </Link>
                </li>
            </ul>
        </nav>   
    )
}