import { useState } from 'react';
import styles from './Calendar.module.css';

interface CalendarProps {
  onDateSelect: (date: Date) => void;
}

export const Calendar = ({ onDateSelect }: CalendarProps) => {
  const [currentMonth, setCurrentMonth] = useState(new Date());

  const daysInMonth = new Date(
    currentMonth.getFullYear(),
    currentMonth.getMonth() + 1,
    0
  ).getDate();

  const handleDateClick = (day: number) => {
    const selectedDate = new Date(
      currentMonth.getFullYear(),
      currentMonth.getMonth(),
      day
    );
    onDateSelect(selectedDate);
  };

  return (
    <div className={styles.calendar}>
      <div className={styles.header}>
        <button className={styles.cursorBtn} onClick={() => setCurrentMonth(
          new Date(currentMonth.getFullYear(), currentMonth.getMonth() - 1)
        )}>
          &lt;
        </button>
        <h3>
          {currentMonth.toLocaleString('default', { month: 'long', year: 'numeric' })}
        </h3>
        <button className={styles.cursorBtn} onClick={() => setCurrentMonth(
          new Date(currentMonth.getFullYear(), currentMonth.getMonth() + 1)
        )}>
          &gt;
        </button>
      </div>
      <div className={styles.daysGrid}>
        {Array.from({ length: daysInMonth }, (_, i) => i + 1).map((day) => (
          <div 
            key={day} 
            className={styles.day} 
            onClick={() => handleDateClick(day)}
          >
            {day}
          </div>
        ))}
      </div>
    </div>
  );
};