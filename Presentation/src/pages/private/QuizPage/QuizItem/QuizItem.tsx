import { CATEGORIES, icons, type QuizCategory } from "../constants";
import type { AnswerQuizItem, QuizItem as QuizItemType} from "../types";
import styles from './QuizItem.module.css'

export const QuizItem: React.FC<{ quiz: QuizItemType | AnswerQuizItem}> = ({ quiz }) => {

  const category = CATEGORIES[parseInt(quiz.category as keyof typeof CATEGORIES)];
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