import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';


import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../../../auth/AuthContext/AuthContext';
import { registerSchema, type RegisterFormData } from '../../../../schemas';
import styles from './RegisterForm.module.css'

export const RegistrationForm = () => {
  const { registration } = useAuth();
  const navigate = useNavigate();
  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
    setError,
  } = useForm<RegisterFormData>({
    resolver: yupResolver(registerSchema),
  });

  const onSubmit = async (data: RegisterFormData) => {
    try {
      await registration({
        name: data.username,
        email: data.email,
        password: data.password,
      });
    } catch {
      setError('root', {
        type: 'manual',
        message: 'Неверный email или пароль',
      });
    }
    navigate('/userAccount');
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
          placeholder='Никнейм'
          type="text"
          {...register('username')}
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

      <div className={styles.formGroup}>
        <input
          placeholder='Подтвердить пароль'
          type="password"
          {...register('confirmPassword')}
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
        {isSubmitting ? 'Вход...' : 'Начать путешествие'}
      </button>
    </form>
  );
}