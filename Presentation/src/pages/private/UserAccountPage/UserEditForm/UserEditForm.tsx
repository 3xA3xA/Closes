import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';

import { userUpdateSchema, type UserUpdateFormData } from '../../../../schemas';
import { useAuth } from '../../../../auth/AuthContext/AuthContext';
import styles from './UserEditForm.module.css'
import { updateUser } from '../../../../api/AccountService/accountService';
import { objectToFormData } from '../../../../utils/objectToFormdata';

export const UserEditForm = ({ 
  onClose 
}: { 
  onClose: () => void 
}) => {
  const { user } = useAuth();

  const {
    register,
    handleSubmit,
    formState: { isSubmitting, errors },
    setError,
  } = useForm<UserUpdateFormData>({
    defaultValues: {
      name: user?.name ?? '',
      email: user?.email ?? '',
      newPassword: null,
      oldPassword: null,
    },
    resolver: yupResolver(userUpdateSchema)
  })

  const onSubmit = async (data: UserUpdateFormData) => {
    try {
      const formData = objectToFormData(data)
      await updateUser(formData)
    } catch {
      setError('root', {
        type: 'manual',
        message: 'Некорректно переданы параметры'
      })
    }
  }

  return (
    <form onSubmit={handleSubmit(onSubmit)} className={styles.form}>
      <div className={styles.formGroup}>
        <input
          placeholder='Ник'
          {...register('name')}
          className={errors.name ? styles.errorInput : ''}
        />
        {errors.name && (
          <span className={styles.error}>{errors.name.message}</span>
        )}
      </div>

      <div className={styles.formGroup}>
        <input
          placeholder='Электронная почта'
          type="email"
          {...register('email')}
          className={errors.email ? styles.errorInput : ''}
        />
        {errors.email && (
          <span className={styles.error}>{errors.email.message}</span>
        )}
      </div>

      <div className={styles.formGroup}>
        <input
          placeholder='Старый пароль'
          type="password"
          {...register('oldPassword')}
          className={errors.oldPassword ? styles.errorInput : ''}
        />
        {errors.oldPassword && (
          <span className={styles.error}>{errors.oldPassword.message}</span>
        )}
      </div>

      <div className={styles.formGroup}>
        <input
          placeholder='Новый пароль'
          type="password"
          {...register('newPassword')}
          className={errors.newPassword ? styles.errorInput : ''}
        />
        {errors.newPassword && (
          <span className={styles.error}>{errors.newPassword.message}</span>
        )}
      </div>

      <div className={styles.buttons}>
        <button type="submit" disabled={isSubmitting}>
          {isSubmitting ? 'Сохранение...' : 'Сохранить'}
        </button>
        
        <button type="button" onClick={onClose}>
          Отмена
        </button>
      </div>
    </form>
  );
}