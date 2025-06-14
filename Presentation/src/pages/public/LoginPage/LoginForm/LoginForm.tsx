import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';


import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../../auth/AuthContext/AuthContext';
import { loginSchema, type LoginFormData } from '../../../../schemas';
import styles from './LoginForm.module.css'

export const LoginForm = () => {
  const { login } = useAuth();
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
    setError,
  } = useForm<LoginFormData>({
    resolver: yupResolver(loginSchema),
  });

  const onSubmit = async (data: LoginFormData) => {
    try {
      await login(data);
      navigate('/userAccount');
    } catch {
      setError('root', {
        type: 'manual',
        message: 'Неверный email или пароль',
      });
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className={styles.authForm}>
      <div className={styles.formGroup}>
        <input
          placeholder='Электронная почта'
          type="email"
          {...register('email')}
          className={errors.email ? 'error' : ''}
        />
        {errors.email && <span className="error-message">{errors.email.message}</span>}
      </div>

      <div className={styles.formGroup}>
        <input
          placeholder='Пароль'
          type="password"
          {...register('password')}
          className={errors.password ? 'error' : ''}
        />
        {errors.password && (
          <span className={styles.errorMessage}>{errors.password.message}</span>
        )}
      </div>

      {errors.root && (
        <div className={styles.formError}>{errors.root.message}</div>
      )}

      <button type="submit" disabled={isSubmitting}>
        {isSubmitting ? 'Вход...' : 'Продолжить путешествие'}
      </button>
    </form>
  );
}