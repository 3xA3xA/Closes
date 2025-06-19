
import { useEffect, useState, type Dispatch, type SetStateAction } from 'react'
import { Header } from '../../../components/semantic/Header/Header'
import { NavBar } from '../../../components/semantic/NavBar/NavBar'
import styles from './QuizPage.module.css'
import type { Group } from '../UserAccountPage/types'
import { Link, useParams } from 'react-router-dom'
import { QuizItem } from './QuizItem/QuizItem'
import { getPassedQuizesByIds, getQuizes } from '../../../api/QuizService/quizService'
import type { QuizItem as QuizItemType } from './types'

interface QuizPageProps {
    isModalOpen: boolean;
    selectedGroup: Group | null;
    setIsModalOpen: Dispatch<SetStateAction<boolean>>;
}

export const QuizPage: React.FC<QuizPageProps> = () => {

    const { groupId}  = useParams();
    const { groupMemberId } = useParams()

    const [quizes, setQuizes] = useState<QuizItemType[]>([]);
    const [showSolved, setShowSolved] = useState(false);

    useEffect(() => {
        const getAllQuizes = async () => {
            try {
                const quizesRes = await getQuizes();
                setQuizes(quizesRes);
            } catch (err) {
                console.error('Ошибка запроса: ', err)
            }
        }

        const getPassedQuizes = async () => {
            try {
                const passedRes = await getPassedQuizesByIds({ groupMemberId: groupMemberId! })
                setQuizes(passedRes)
            } catch (err) {
                console.error('Ошибка запроса пройденных тестов: ', err)
            }
        }

        if(showSolved) {
            getPassedQuizes()
        } else {
            getAllQuizes()
        }
        
    }, [groupId, groupMemberId, showSolved])

    return (
        <div className={styles.root}>
            <Header title='Квизы'/>

            <main className={styles.main}>
                <div className={styles.addBtn}>
                    <Link style={{color: '#FFF'}} to={groupId && groupMemberId ? `/groupCreateQuiz/${groupId}/${groupMemberId}` : '#'}>+</Link>
                </div>       

                <div className={styles.toggle}>
                    <span>Нерешённые</span>
                    <label className={styles.toggleSwitch}>
                        <input 
                            type="checkbox" 
                            checked={showSolved}
                            onChange={() => setShowSolved(!showSolved)}
                        />
                        <span className={styles.toggleSlider}></span>
                    </label>
                    <span>Решённые</span>
                </div>

                <ul className={styles.quizList}>
                    {quizes.map((quiz) => (
                        <Link 
                            to={showSolved ? `/quizSolution/${groupId}/${groupMemberId}/${quiz.id}/passed` : `/quizSolution/${groupId}/${groupMemberId}/${quiz.id}`}
                            state={{ groupId, groupMemberId }}
                        >
                            <QuizItem quiz={quiz}/>
                        </Link>
                    ))}
                </ul>         
            </main>

            <NavBar />
        </div>
    )
}