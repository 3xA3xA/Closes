import { useEffect, useState } from 'react'
import { Header } from '../../../components/semantic/Header/Header'
import { NavBar } from '../../../components/semantic/NavBar/NavBar'
import styles from './AchievementPage.module.css'
import type { Group } from '../UserAccountPage/types'
import { getGroupById } from '../../../api/GroupService/groupService'
import { useParams } from 'react-router-dom'
import { achievementsMap, type GroupType } from './constants'

export const AchievementPage = () => {
    const groupId = useParams();
    const [group, setGroup] = useState<Group | null>(null)

    useEffect(() => {
        getGroupById(groupId.groupId!)
            .then(
                setGroup
            )
            .catch(console.error);
    }, [groupId.groupId]);

    
    const finalAchievements = achievementsMap[group?.type as GroupType] || [];

    return (
        <div className={styles.root}>
            <Header />

            <main style={{color: '#000'}}>
                <ul className={styles.achieveList}> 
                    {finalAchievements.map((achieve) => (
                        <li className={styles.achieve}>
                            <div className={styles.icon}>
                                {achieve.name}
                            </div>
                           
                        </li>
                    ))}
                </ul>
            </main>

            <NavBar />
        </div>
    )
}