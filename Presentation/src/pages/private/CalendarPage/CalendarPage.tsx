import { useState } from 'react';
import { Header } from '../../../components/semantic/Header/Header'
import { NavBar } from '../../../components/semantic/NavBar/NavBar'
import styles from './CalendarPage.module.css'
import { Calendar } from './Calendar/Calendar';
import type { Activity } from './types';
import ActivityList from './ActivityList/ActivityList';
import { mockActivities } from './mocks';

export const CalendarPage = () => {
    const [selectedDate, setSelectedDate] = useState<Date | null>(null);
    const [activities, setActivities] = useState<Activity[]>(mockActivities);

    const handleDateSelect = (date: Date) => {
        setSelectedDate(date);
    };

    const handleCreateActivity = (newActivity: Activity) => {
        setActivities([...activities, newActivity]);
        setSelectedDate(null);
    };

    const handleCancelCreate = () => {
        setSelectedDate(null);
    };

    return (
        <div className={styles.root}>
            <Header title='Календарь'/>

            <main className={styles.main}>
                {/* {selectedDate ? (
                    <CreateActivity 
                    date={selectedDate} 
                    onCreate={handleCreateActivity}
                    onCancel={handleCancelCreate}
                    />
                ) : (
                    
                )} */}
                <>
                    <div className={styles.calendarSection}>
                        <Calendar onDateSelect={handleDateSelect} />
                    </div>
                    <div className={styles.activitiesSection}>
                        <h2>Ближайшие активности</h2>
                        <ActivityList activities={activities} />
                    </div>
                </>
            </main>

            <NavBar />
        </div>
    )
}