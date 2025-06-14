import { AiFillHeart } from "react-icons/ai";
import styles from './Registration.module.css'

import { Link } from "react-router-dom";
import { RegistrationForm } from "./RegisterForm/RegisterForm";


export const Registration = () => {
    return (
        <div className={styles.root}>
            <AiFillHeart className={styles.icon}/>

            <h1 className={styles.heading}>Добро пожаловать в Closes!</h1>
            <p className={styles.subtitle}>Откройте новые грани вашей любви через уникальные возможности приложения</p>

            <RegistrationForm />

            <p className={styles.condition}>Уже есть аккаунт? <Link to="/login" style={{ color: '#FFF', textDecoration: 'underline'}}>Войти</Link></p>
        </div>
    )
}