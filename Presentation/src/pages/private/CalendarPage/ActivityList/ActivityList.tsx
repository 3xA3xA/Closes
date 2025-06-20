import type { Activity } from '../types';
import styles from './ActivityList.module.css';

interface ActivityListProps {
  activities: Activity[];
}

const ActivityList = ({ activities }: ActivityListProps) => {
  const now = new Date();
  const upcomingActivities = activities
    .filter(activity => new Date(activity.startAt) >= now)
    .sort((a, b) => new Date(a.startAt).getTime() - new Date(b.startAt).getTime());

  return (
    <ul className={styles.list}>
      {upcomingActivities.map(activity => (
        <li key={activity.id} className={styles.item}>
          <div className={styles.date}>
            {new Date(activity.startAt).toLocaleDateString()}
          </div>
          <div className={styles.details}>
            <h3>{activity.name}</h3>
            <p>{activity.description}</p>
          </div>
        </li>
      ))}
    </ul>
  );
};

export default ActivityList;