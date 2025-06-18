import { CATEGORIES, icons, type Quiz, type QuizCategory } from "../constants";
import styles from './QuizItem.module.css'

export const QuizItem: React.FC<{ quiz: Quiz }> = ({ quiz }) => {

  const category = CATEGORIES[quiz.category as keyof typeof CATEGORIES];
  const IconComponent = icons[category.icon as QuizCategory];

  return (
    <div 
      className={styles.quizItem}
      style={{
        backgroundColor: category.color + '20', // Добавляем прозрачность
      }}
    >
      <div className={styles.quizInfo}>
        <h3 className={styles.quizName}>{quiz.name}</h3>
        <p className={styles.quizDescription}>{quiz.description}</p>
        <time className={styles.quizDate}>
         дата создания:  {new Date(quiz.createdAt).toLocaleDateString()}
        </time>
      </div>
      
      <IconComponent className={styles.quizIcon} style={{ color: category.color }}/>
    </div>
  );
};