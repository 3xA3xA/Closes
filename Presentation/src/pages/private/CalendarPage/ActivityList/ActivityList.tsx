import { useParams } from 'react-router-dom';
import type { Activity } from '../types';
import styles from './ActivityList.module.css';
import { addMemberInActivity } from '../../../../api/ActivityService/activityService';

interface ActivityListProps {
  activities: Activity[];
}

const ActivityList = ({ activities }: ActivityListProps) => {
  const { groupMemberId } = useParams(); 
  const now = new Date();
  const upcomingActivities = activities
    .filter(activity => new Date(activity.startAt) >= now)
    .sort((a, b) => new Date(a.startAt).getTime() - new Date(b.startAt).getTime());
  const isActivityMember = (activity: Activity) => {
        return activity.activityMembers.some(member => member.groupMemberId === groupMemberId);
    };


  return (
    <ul className={styles.list}>
      {upcomingActivities.map(activity => (
        <li key={activity.id} className={styles.item}>
          <div className={styles.date}>
            {new Date(activity.startAt).toLocaleDateString()}
          </div>
          <div className={styles.details}>
            <h3>{activity.name}</h3>
            <div>{activity.description}</div>
          </div>

          {!isActivityMember(activity) && <div 
              className={styles.doneBtn}
              onClick={() => addMemberInActivity(activity.id, groupMemberId!)}
            >
            Участвовать
          </div>}
        </li>
      ))}
    </ul>
  );
};

export default ActivityList;