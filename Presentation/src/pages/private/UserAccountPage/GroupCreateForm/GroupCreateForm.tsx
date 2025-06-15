import { useForm } from "react-hook-form";
import React from 'react';
import ReactDOM from 'react-dom';
import type { User } from "../../../../auth/types";
import styles from './GroupCreateForm.module.css'
import type { CreateGroupFormData } from "../../../../schemas";

interface GroupModalFormProps {
    isOpen: boolean;
    onClose: () => void
    user: User
}

const GroupModalForm: React.FC<GroupModalFormProps> = ({ isOpen, onClose, user}) => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<CreateGroupFormData>();

  const handleFormSubmit = (data: CreateGroupFormData) => {
   console.log(data, user);
   onClose();
  };

  if (!isOpen) return null;

  return ReactDOM.createPortal(
    <div className={styles.modalOverlay}>
      <div className={styles.modalContent}>
        <button className={styles.closeButton} onClick={onClose} aria-label="Закрыть">
          ×
        </button>
        <h2>Создать новую группу</h2>
        <form onSubmit={handleSubmit(handleFormSubmit)} noValidate>
          <div className={styles.formGroup}>
            <label htmlFor="groupType">Тип группы*</label>
            <select
              id="groupType"
              {...register('type', { required: 'Выберите тип группы' })}
              className={errors.type ? styles.errorInput : ''}
            >
              <option value="">Выберите тип</option>
              <option value="Семья">Семья</option>
              <option value="Пара">Пара</option>
              <option value="Друзья">Друзья</option>
            </select>
            {errors.type && (
              <span className={styles.errorMessage}>{errors.type.message}</span>
            )}
          </div>

          <div className={styles.formGroup}>
            <label htmlFor="groupName">Название группы*</label>
            <input
              id="groupName"
              type="text"
              {...register('name', {
                required: 'Введите название группы',
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

          <button type="submit" className={styles.submitButton}>
            Создать группу
          </button>
        </form>
      </div>
    </div>,
    document.getElementById('modal-root')!
  );
};

export default GroupModalForm;