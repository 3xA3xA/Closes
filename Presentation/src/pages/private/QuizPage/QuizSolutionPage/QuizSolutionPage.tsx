import { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';

import styles from './QuizSolutionPage.module.css';
import { getQuestionsQuizById, submitQuestionsQuiz } from '../../../../api/QuizService/quizService';
import type { QuizItem as QuizItemType } from '../types';
import { QuizItem } from '../QuizItem/QuizItem';
import { Header } from '../../../../components/semantic/Header/Header';
import { NavBar } from '../../../../components/semantic/NavBar/NavBar';

export const QuizSolutionPage: React.FC = () => {
  const { quizId } = useParams<{ quizId: string }>();
  const { groupMemberId } = useParams<{ groupMemberId: string }>();
  const navigate = useNavigate();
  
  const [quiz, setQuiz] = useState<QuizItemType | null>(null);
  const [answers, setAnswers] = useState<string[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchQuiz = async () => {
      try {
        const quizData = await getQuestionsQuizById(quizId!);
        setQuiz(quizData);
        setAnswers(new Array(quizData.quizItems.length).fill(''));
      } catch (err) {
        setError('Не удалось загрузить квиз');
        console.error(err);
      } finally {
        setIsLoading(false);
      }
    };

    fetchQuiz();
  }, [quizId]);

  const handleAnswerChange = (index: number, value: string) => {
    const newAnswers = [...answers];
    newAnswers[index] = value;
    setAnswers(newAnswers);
  };

  const handleSubmit = async () => {
    if (!groupMemberId) {
      setError('Необходимо быть участником группы');
      return;
    }

    try {
      await submitQuestionsQuiz({
        groupMemberId,
        quizId: quizId!,
        answers
      });
      navigate(-1); // Возвращаемся назад после успешной отправки
    } catch (err) {
      setError('Ошибка при отправке ответов');
      console.error(err);
    }
  };

  if (isLoading) return <div>Загрузка...</div>;
  if (error) return <div>{error}</div>;
  if (!quiz) return <div>Квиз не найден</div>;

  return (
    <div className={styles.root}>
      <Header />

      <div className={styles.main}>
        <QuizItem quiz={quiz} />

        <div className={styles.questionsList}>
            {quiz.quizItems.map((item, index) => (
                <div key={item.id} className={styles.questionItem}>
                    <h3>Вопрос {item.order}: {item.text}</h3>
                    <input
                    type="text"
                    value={answers[index]}
                    onChange={(e) => handleAnswerChange(index, e.target.value)}
                    placeholder="Ваш ответ"
                    />
                </div>
            ))}
        </div>

        <button 
            onClick={handleSubmit}
            className={styles.submitButton}
        >
            Отправить ответы
        </button>
      </div>  

      <NavBar />
    </div>
  );
};