import { icons, type IconKey } from "../constants";
import styles from './AchievementItem.module.css'

export const AchievementItem: React.FC<{ icon: IconKey, name: string, passed: boolean, priority: number}> = ({ icon, name, passed, priority }) => {
    const IconComponent = icons[icon];
    console.log(passed)
    const priorityClass = `priority${priority}`;
    const iconClass = `iconPriority${priority}`

    const shadowColors = [
        'rgba(138, 43, 226, 0.7)', 
        'rgba(255, 105, 180, 0.7)', 
        'rgba(255, 0, 0, 0.7)'     
    ];

    const itemStyle = {
        '--shadow-color': passed ? shadowColors[priority] : 'transparent',
        filter: passed ? 'none' : 'grayscale(100%)',
        opacity: passed ? 1 : 0.6
    } as React.CSSProperties;

    return (
        <li className={`${styles.achieve} ${styles[priorityClass]}`} style={itemStyle}>
            <IconComponent className={`${styles.icon} ${styles[iconClass]}`}/>

            <div className={styles.info}>
                {name}
            </div>
        </li>
    )
}