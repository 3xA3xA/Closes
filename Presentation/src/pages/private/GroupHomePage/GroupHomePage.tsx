import { useParams } from 'react-router-dom';
import { Header } from '../../../components/semantic/Header/Header'
import { NavBar } from '../../../components/semantic/NavBar/NavBar'
import styles from './GroupHomePage.module.css'
import type { Group } from '../UserAccountPage/types';
import { useEffect, useState } from 'react';
import type { Activity } from '../CalendarPage/types';
import { getGroupById } from '../../../api/GroupService/groupService';
import { getGroupActivity } from '../../../api/ActivityService/activityService';
import { format, differenceInDays, parseISO } from 'date-fns';
import { ru } from 'date-fns/locale';

export const GroupHomePage = () => {
    const { groupId } = useParams<{ groupId: string }>();
    const [group, setGroup] = useState<Group | null>(null);
    const [activities, setActivities] = useState<Activity[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const groupData = await getGroupById(groupId!);
                setGroup(groupData);
                const activitieData = await getGroupActivity(groupId!);
                setActivities(activitieData);
            } catch (err) {
                console.error('Ошибка подгрузки группы', err)
            } finally {
                setLoading(false);
            }
        }

        fetchData()
    }, [groupId])

    if (loading) return <div className={styles.loading}>Загрузка...</div>;
    if (!group) return <div className={styles.error}>Группа не найдена</div>;

    const daysTogether = differenceInDays(new Date(), parseISO(group.createdAt));
    const formattedDate = format(parseISO(group.createdAt), 'd MMMM yyyy', { locale: ru });

    const getDayWord = (days: number) => {
        if (days % 10 === 1 && days % 100 !== 11) return 'день';
        if (days % 10 >= 2 && days % 10 <= 4 && (days % 100 < 10 || days % 100 >= 20)) return 'дня';
        return 'дней';
    };

    return (
        <div className={styles.root}>
            <Header />

            <main className={styles.main}>
                {/* Заголовок с количеством дней */}
                <h1 className={styles.header}>
                    Вместе {daysTogether} {getDayWord(daysTogether)}
                </h1>
                
                {/* Кружки участников */}
                <div className={styles.membersContainer}>
                    <div className={styles.membersCircle}>
                    {group.members.map((member, index) => (
                        <div 
                        key={member.id}
                        className={styles.memberAvatar}
                        style={{ 
                            backgroundColor: member.uniqueColor,
                            zIndex: group.members.length - index
                        }}
                        title={member.userName}
                        />
                    ))}
                    </div>
                </div>

                <div className={styles.membersList}>
                {group.members.map((member) => (
                    <div key={member.id} className={styles.memberItem}>
                    <span 
                        className={styles.memberColorIndicator}
                        style={{ backgroundColor: member.uniqueColor }}
                    />
                    <span className={styles.memberName}>{member.userName}</span>
                    </div>
                ))}
                </div>
                
                {/* Дата создания */}
                <p className={styles.dateText}>
                    Вы вместе с {formattedDate}
                </p>
                
                {/* Ближайшие события */}
                <div className={styles.eventsSection}>
                    <h2 className={styles.eventsHeader}>Ближайшие события</h2>
                    <div className={styles.eventsList}>
                    {activities.length > 0 ? (
                        activities.map(activity => (
                        <div key={activity.id} className={styles.eventCard}>
                            <h3 className={styles.eventTitle}>{activity.name}</h3>
                            <p className={styles.eventDate}>
                            {format(activity.startAt, 'd MMMM, HH:mm', { locale: ru })}
                            </p>
                        </div>
                        ))
                    ) : (
                        <p className={styles.noEvents}>Нет предстоящих событий</p>
                    )}
                    </div>
                </div>
            </main>

            <NavBar />
        </div>
    )
}