import { useForm } from "react-hook-form";
import React from 'react';
import ReactDOM from 'react-dom';
import styles from './WishCreateForm.module.css'
import type { CreateWishFormData } from "../../../../schemas";
import { addNewWish } from "../../../../api/WishlistService/wishListService";

interface WishCreateFormProps {
    isOpen: boolean;
    memberId: string;
    wishlistId: string;
    onClose: () => void;
}

export const WishCreateForm: React.FC<WishCreateFormProps> = ({ isOpen, memberId, wishlistId, onClose }) => {
  const {
    register,
    handleSubmit,
    formState: { errors },
    setError,
  } = useForm<CreateWishFormData>();

  if (!isOpen) return null;

  const onSubmit = async (data: CreateWishFormData) => {
      try {
        console.log('Новый wish', {
            wishlistId: wishlistId,
            groupMemberId: memberId,
            imageUrl: '',
            completed: false,
            ...data
        })
        addNewWish({
            wishlistId: wishlistId,
            groupMemberId: memberId,
            imageUrl: '',
            completed: false,
            ...data
        })
        onClose()
      } catch {
        setError('root', {
          type: 'manual',
          message: 'Некорректно переданы параметры'
        })
      }
    }

  return ReactDOM.createPortal(
    <div className={styles.modalOverlay}>
      <div className={styles.modalContent}>
        <button className={styles.closeButton} onClick={onClose} aria-label="Закрыть">
          ×
        </button>
        <h2>Создать новое желание</h2>
        <form onSubmit={handleSubmit(onSubmit)} noValidate>
          <div className={styles.formGroup}>
            <label htmlFor="name">Название желания</label>
            <input
              id="name"
              type="text"
              {...register('name', {
                required: 'Введите название желаемого подарка',
                minLength: {
                  value: 3,
                  message: 'Название должно быть не менее 3 символов',
                },
                maxLength: {
                  value: 30,
                  message: 'Название должно быть не более 30 символов',
                },
              })}
              className={errors.name ? styles.errorInput : ''}
            />
            {errors.name && (
              <span className={styles.errorMessage}>{errors.name.message}</span>
            )}
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="description">Описание желания</label>
            <input
              id="description"
              type="text"
              {...register('description', {
                required: 'Введите описание желаемого подарка',
                minLength: {
                  value: 3,
                  message: 'Описание должно быть не менее 3 символов',
                },
                maxLength: {
                  value: 30,
                  message: 'Описание должно быть не более 50 символов',
                },
              })}
              className={errors.description ? styles.errorInput : ''}
            />
            {errors.description && (
              <span className={styles.errorMessage}>{errors.description.message}</span>
            )}
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="priority">Приоритет желания</label>
            <select
              id="priority"
              {...register('priority', { required: 'Выберите приоритет подарка' })}
              className={errors.priority ? styles.errorInput : ''}
            >
              <option value="">Выберите тип</option>
              <option value="0">Низкий</option>
              <option value="1">Средний</option>
              <option value="2">Высокий</option>
            </select>
            {errors.priority && (
              <span className={styles.errorMessage}>{errors.priority.message}</span>
            )}
          </div>

          <button type="submit" className={styles.submitButton}>
            Создать группу
          </button>
        </form>
      </div>
    </div>,
    document.getElementById('modal-root')!
  );
};