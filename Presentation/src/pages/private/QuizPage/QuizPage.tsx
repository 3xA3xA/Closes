
import type { Dispatch, SetStateAction } from 'react'
import { Header } from '../../../components/semantic/Header/Header'
import { NavBar } from '../../../components/semantic/NavBar/NavBar'
import styles from './QuizPage.module.css'
import type { Group } from '../UserAccountPage/types'
import { Link, useParams } from 'react-router-dom'
import { QuizItem } from './QuizItem/QuizItem'

interface QuizPageProps {
    isModalOpen: boolean;
    selectedGroup: Group | null;
    setIsModalOpen: Dispatch<SetStateAction<boolean>>;
}

export const QuizPage: React.FC<QuizPageProps> = () => {

    const { groupId}  = useParams();
    console.log('groupId', groupId);

    const quizes = [
        {
            name: 'Для пары',
            description: 'Для пары',
            category: 0,
            createdAt: '2025-06-18 12:29:44.6809889'
        },
        {
            name: 'Для семьи',
            description: 'Для семьи',
            category: 1,
            createdAt: '2025-06-18 12:29:44.6809889'
        },
        {
            name: 'Для друзей',
            description: 'Для друзей',
            category: 2,
            createdAt: '2025-06-18 12:29:44.6809889'
        },
        {
            name: 'Сухарики',
            description: 'Для пары',
            category: 3,
            createdAt: '2025-06-18 12:29:44.6809889'
        },
        {
            name: 'Чупапи',
            description: 'Чупепы',
            category: 4,
            createdAt: '2025-06-18 12:29:44.6809889'
        },
    ]

    return (
        <div className={styles.root}>
            <Header title='Квизы'/>

            <main className={styles.main}>
                <div className={styles.addBtn}>
                    <Link style={{color: '#000'}} to={groupId ? `/groupCreateQuiz/${groupId}` : '#'}>+</Link>
                </div>       

                <ul className={styles.quizList}>
                    {quizes.map((quiz) => (
                        <QuizItem quiz={quiz}/>
                    ))}
                </ul>         
            </main>

            <NavBar />
        </div>
    )
}