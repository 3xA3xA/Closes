import { useEffect, useState } from 'react';
import { Header } from '../../../../components/semantic/Header/Header'
import { NavBar } from '../../../../components/semantic/NavBar/NavBar'
import styles from './QuizCheckPage.module.css'
import { getAnswersQuizByIds, getPassedQuizMembersByIds, type ExtendedUser } from '../../../../api/QuizService/quizService';
import { useNavigate, useParams } from 'react-router-dom';
import type { AnswerQuizItem } from '../types';

export const QuizCheckPage = () => {
  const { quizId, groupId, groupMemberId } = useParams<{ 
    quizId: string;
    groupId: string;
    groupMemberId: string;
  }>();
  const navigate = useNavigate();

  const [quiz, setQuiz] = useState<AnswerQuizItem | null>(null);
  const [quizMembers, setQuizMembers] = useState<ExtendedUser[]>([]);
  const [selectedMember, setSelectedMember] = useState<ExtendedUser | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchData = async () => {
      try {
        setIsLoading(true);
        setError('');
        
        const [quizData, membersData] = await Promise.all([
          getAnswersQuizByIds({ quizId: quizId!, groupMemberId: groupMemberId! }),
          getPassedQuizMembersByIds({ quizId: quizId!, groupId: groupId!, excludeMemberId: groupMemberId! })
        ]);
        
        setQuiz(quizData);
        setQuizMembers(membersData);
      } catch (err) {
        console.error(err);
        setError('Не удалось загрузить данные');
      } finally {
        setIsLoading(false);
      }
    };

    fetchData();
  }, [quizId, groupId, groupMemberId]);

  const handleMemberSelect = async (memberId: string) => {
    try {
      const member = quizMembers.find(m => m.id === memberId);
      if (!member) return;

      const memberAnswers = await getAnswersQuizByIds({ 
        quizId: quizId!, 
        groupMemberId: memberId 
      });
      
      setSelectedMember({
        ...member,
        quizItems: memberAnswers.quizItems
      });
    } catch (err) {
      console.error(err);
      setError('Не удалось загрузить ответы участника');
    }
  };

  const compareAnswers = (questionId: string) => {
    if (!quiz || !selectedMember?.quizItems) return false;
    
    const myAnswer = quiz.quizItems.find(q => q.id === questionId)?.answer?.toLowerCase();
    const theirAnswer = selectedMember.quizItems.find(q => q.id === questionId)?.answer?.toLowerCase();
    
    return myAnswer && theirAnswer && myAnswer === theirAnswer;
  };

  if (isLoading) return <div className={styles.loading}>Загрузка...</div>;
  if (error) return <div className={styles.error}>{error}</div>;
  if (!quiz) return <div className={styles.error}>Квиз не найден</div>;

  return (
    <div className={styles.root}>
      <Header title="Сравнение ответов" />
      
      <main className={styles.main}>
        <div className={styles.quizInfo}>
          <h2>{quiz.name}</h2>
          {quiz.description && <p className={styles.quizDescription}>{quiz.description}</p>}
        </div>

        <div className={styles.selectContainer}>
          <select
            onChange={(e) => handleMemberSelect(e.target.value)}
            defaultValue=""
            className={styles.memberSelect}
            disabled={quizMembers.length === 0}
          >
            <option value="" disabled>
              {quizMembers.length === 0 ? 'Нет участников для сравнения' : 'Выберите участника'}
            </option>
            {quizMembers.map(member => (
              <option key={member.id} value={member.id}>
                {member.user.name}
              </option>
            ))}
          </select>
        </div>

        {selectedMember && (
          <>
            <h3 className={styles.comparisonTitle}>
              Сравнение с {selectedMember.user.name}
            </h3>
            
            <ul className={styles.comparisonList}>
              {quiz.quizItems.map((question) => {
                const isMatch = compareAnswers(question.id);
                const theirQuestion = selectedMember.quizItems?.find(q => q.id === question.id);
                
                return (
                  <li key={question.id} className={styles.comparisonItem}>
                    <div className={styles.questionHeader}>
                      <span className={styles.questionOrder}>Вопрос {question.order}</span>
                      {question.answer && theirQuestion?.answer && (
                        <span className={`${styles.badge} ${isMatch ? styles.match : styles.mismatch}`}>
                          {isMatch ? 'Совпадение' : 'Несовпадение'}
                        </span>
                      )}
                    </div>
                    
                    <p className={styles.questionText}>{question.text}</p>
                    
                    <div className={styles.answerSection}>
                      <div className={styles.answerBlock}>
                        <span className={styles.answerLabel}>Ваш ответ:</span>
                        <p>{question.answer || 'Нет ответа'}</p>
                      </div>
                      
                      <div className={styles.answerBlock}>
                        <span className={styles.answerLabel}>Ответ {selectedMember.user.name}:</span>
                        <p>{theirQuestion?.answer || 'Нет ответа'}</p>
                      </div>
                    </div>
                  </li>
                );
              })}
            </ul>
          </>
        )}

        <button 
          onClick={() => navigate(-1)}
          className={styles.backButton}
        >
          Вернуться к списку квизов
        </button>
      </main>

      <NavBar />
    </div>
  );
};