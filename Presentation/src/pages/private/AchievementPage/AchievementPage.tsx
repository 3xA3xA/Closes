import { useEffect, useState } from 'react'
import { Header } from '../../../components/semantic/Header/Header'
import { NavBar } from '../../../components/semantic/NavBar/NavBar'
import styles from './AchievementPage.module.css'
import type { Group } from '../UserAccountPage/types'
import { getGroupById } from '../../../api/GroupService/groupService'
import { useParams } from 'react-router-dom'
import { achievementsMap, type GroupType } from './constants'
import { AchievementItem } from './AchievementItem/AchievementItem'

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
            <Header title='Достижения'/>

            <main className={styles.main}>
                <ul className={styles.achieveList}> 
                    {finalAchievements.map((achieve) => (
                        <AchievementItem icon={achieve.icon} name={achieve.name} passed={achieve.passed} priority={achieve.priority}/>
                    ))}
                </ul>
            </main>

            <NavBar />
        </div>
    )
}