import { useEffect, useState } from 'react';
import { Header } from '../../../components/semantic/Header/Header'
import { NavBar } from '../../../components/semantic/NavBar/NavBar'
import styles from './CalendarPage.module.css'
import { Calendar } from './Calendar/Calendar';
import type { Activity } from './types';
import ActivityList from './ActivityList/ActivityList';
import { mockActivities } from './mocks';
import { CreateActivityModal } from './CreateActivityModal/CreateActivityModal';
import { useParams } from 'react-router-dom';
import { getGroupActivity } from '../../../api/ActivityService/activityService';

export const CalendarPage = () => {
    const { groupId } = useParams()

    const [selectedDate, setSelectedDate] = useState<Date | null>(null);
    const [activities, setActivities] = useState<Activity[]>(mockActivities);
    const [showCreateForm, setShowCreateForm] = useState(false);

    useEffect(() => {
        const fetchActivities = async () => {
            try {
                const activitieData = await getGroupActivity(groupId!);
                console.log('activitieData', activitieData)
                setActivities(activitieData)
            } catch (err) {
                console.error('Ошибка запроса активитиз: ', err)
            }
        }

        fetchActivities()
    }, [groupId])

    const handleDateSelect = (date: Date) => {
        setSelectedDate(date);
        setShowCreateForm(true);
    };

    const handleCreateActivity = (newActivityData: {
        name: string;
        description: string;
        type: number;
        status: number
        startAt: Date;
        endAt: Date | null;
        groupId: string;
    }) => {
        const newActivity = {
            id: Math.random().toString(36).substring(2, 9),
            ...newActivityData,
            status: 1
        };
        
        setActivities([...activities, newActivity]);
        setShowCreateForm(false);
        setSelectedDate(null);
    };

    const handleCancelCreate = () => {
        setShowCreateForm(false);
        setSelectedDate(null);
    };

    return (
        <div className={styles.root}>
            <Header title='Календарь'/>
            <main className={styles.main}>
                <div className={styles.calendarSection}>
                    <Calendar onDateSelect={handleDateSelect} />
                </div>
                <div className={styles.activitiesSection}>
                    <h2>Ближайшие активности</h2>
                    <ActivityList activities={activities} />
                </div>
            </main>
            <NavBar />

            {showCreateForm && (
                <CreateActivityModal
                    selectedDate={selectedDate}
                    onCancel={handleCancelCreate}
                    onCreate={handleCreateActivity}
                />
            )}
        </div>
    );
};