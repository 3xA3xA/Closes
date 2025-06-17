
import type { Dispatch, SetStateAction } from 'react'
import { Header } from '../../../components/semantic/Header/Header'
import { NavBar } from '../../../components/semantic/NavBar/NavBar'
import styles from './QuizPage.module.css'
import type { Group } from '../UserAccountPage/types'
import { Link, useParams } from 'react-router-dom'

interface QuizPageProps {
    isModalOpen: boolean;
    selectedGroup: Group | null;
    setIsModalOpen: Dispatch<SetStateAction<boolean>>;
}

export const QuizPage: React.FC<QuizPageProps> = () => {

    const { groupId}  = useParams();
    console.log('groupId', groupId);

    return (
        <div className={styles.root}>
            <Header />

            <main className={styles.main}>
                <div className={styles.addBtn}>
                    <Link style={{color: '#000'}} to={groupId ? `/groupCreateQuiz/${groupId}` : '#'}>+</Link>
                </div>

                
            </main>

            <NavBar />
        </div>
    )
}