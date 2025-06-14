import { AiFillHeart } from "react-icons/ai";
import styles from './login.module.css'
import { LoginForm } from "./LoginForm/LoginForm";
import { Link } from "react-router-dom";

export const Login = () => {
    return (
        <div className={styles.root}>
            <AiFillHeart className={styles.icon}/>

            <h1 className={styles.heading}>Добро пожаловать в Closes!</h1>
            <p className={styles.subtitle}>Откройте новые грани вашей любви через уникальные возможности приложения</p>

            <LoginForm />

            <p className={styles.condition}>Нет аккаунта? <Link to="/register" style={{ color: '#FFF', textDecoration: 'underline'}}>Зарегистрироваться</Link></p>
        </div>
    )
}