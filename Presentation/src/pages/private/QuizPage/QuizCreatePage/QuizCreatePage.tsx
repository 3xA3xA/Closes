import styles from './QuizCreatePage.module.css'
import { type QuizFormValues } from '../types'
import { useState } from 'react';
import { Header } from '../../../../components/semantic/Header/Header';
import { NavBar } from '../../../../components/semantic/NavBar/NavBar';
import { useAuth } from '../../../../auth/AuthContext/AuthContext';
import { createQuiz } from '../../../../api/QuizService/quizService';
import { GrFormPreviousLink } from "react-icons/gr";
import { Link, useParams } from 'react-router-dom';


export const QuizCreatePage = () => {

    const { groupId } = useParams()
    const { user } = useAuth();

    const [formData, setFormData] = useState<QuizFormValues>({
        name: '',
        description: '',
        category: '0',
        questions: [{ text: '' }]
    });

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleQuestionChange = (index: number, value: string) => {
        const updatedQuestions = [...formData.questions];
        updatedQuestions[index].text = value;
        setFormData(prev => ({
        ...prev,
        questions: updatedQuestions
        }));
    };

    const addQuestion = () => {
        setFormData(prev => ({
        ...prev,
        questions: [
            ...prev.questions,
            { text: '' }
        ]
        }));
    };

    const removeQuestion = (index: number) => {
        if (formData.questions.length <= 1) return;
        
        const updatedQuestions = formData.questions
        .filter((_, i) => i !== index)
        .map((q) => ({ ...q }));
        setFormData(prev => ({
            ...prev,
            questions: updatedQuestions
        }));
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        createQuiz({
            userId: user?.id ?? '',
            ...formData
        })
        console.log('Квиз создан с такими данными:', 
            {
                userId: user?.id ?? '',
                ...formData
            }
        )
    };

    return (
        <div className={styles.root}>
            <Header title='Создание квиза'/>

            <main className={styles.main}>
                <div className={styles.head}>
                    <Link style={{color: '#000', width: '20px'}} to={groupId ? `/groupQuizPage/${groupId}` : '#'}>
                        <GrFormPreviousLink style={{width: '100%'}}/>
                    </Link>
                    <h2>Создать новый квиз</h2>
                </div>
                
                <form onSubmit={handleSubmit} className={styles.form}>
                    <div className={styles.questionList}>
                        <div>
                            <label htmlFor="name" className={styles.label}>Название квиза</label>
                            <input
                                type="text"
                                id="name"
                                name="name"
                                value={formData.name}
                                onChange={handleInputChange}
                                className={styles.quizName}
                                required
                            />
                        </div>

                        <div>
                            <label htmlFor="quizDescription" className={styles.label}>
                                Описание квиза
                            </label>
                            <textarea
                                id="description"
                                name="description"
                                value={formData.description}
                                onChange={handleInputChange}
                                rows={3}
                                className={styles.textarea}
                                required
                            />
                        </div>

                        <div>
                            <label htmlFor="quizCategory" className={styles.label}>
                                Категории квиза
                            </label>
                            <select
                                id="category"
                                name="category"
                                value={formData.category}
                                onChange={handleInputChange}
                                className={styles.questionField}
                                required
                            >
                                <option value={0}>
                                    Общие
                                </option>

                                <option value={1}>
                                    Любовь
                                </option>

                                <option value={2}>
                                    Совместимость
                                </option>

                                <option value={3}>
                                    Факты
                                </option>

                                <option value={4}>
                                    Планы
                                </option>
                            </select>
                        </div>
                    </div>

                    <div className={styles.questionsContainer}>
                        <h2 className={styles.questions}>Вопросы</h2>
                
                        {formData.questions.map((question, index) => (
                            <div key={index} className={styles.questionItem}>
                                <div className={styles.questionInfo}>
                                    <label htmlFor={`question-${index + 1}`} className={styles.questionLabel}>
                                        Вопрос #{index + 1}
                                    </label>
                                </div>
                                <input
                                    type="text"
                                    id={`question-${index}`}
                                    value={question.text}
                                    onChange={(e) => handleQuestionChange(index, e.target.value)}
                                    className={styles.questionField}
                                    required
                                />

                                <div className={styles.btnGroup}>
                                    {index === formData.questions.length - 1 && (
                                        <button
                                            type="button"
                                            onClick={addQuestion}
                                            className={styles.addBtn}
                                            title="Add question"
                                        >
                                            <span>+</span>
                                        </button>
                                    )}

                                    {index !== 0 && index === formData.questions.length - 1 && (
                                        <button
                                            type="button"
                                            onClick={() => removeQuestion(index)}
                                            className={styles.deleteBtn}
                                            title="Delete question"
                                        >
                                            <span>-</span>
                                        </button>
                                    )}
                                </div>
                                
                            </div>
                        ))}
                    </div>

                    <div className={styles.createBtnContainer}>
                        <button
                            type="submit"
                            className={styles.createBtn}
                        >
                            Создать квиз
                        </button>
                    </div>
                </form>
            </main>

            <NavBar />
        </div>
    )
}