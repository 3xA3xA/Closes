import { yupResolver } from "@hookform/resolvers/yup";
import { useForm } from "react-hook-form";
import { joinGroupSchema, type JoinGroupFormData } from "../../../../schemas";
import styles from './GroupJoinForm.module.css'
import { joinToGroupByCode } from "../../../../api/GroupService/groupService";
import type { User } from "../../../../auth/types";

interface GroupJoinFormProps {
    user: User
}

export const GroupJoinForm: React.FC<GroupJoinFormProps> = ({ user }) => {

    const {
        register,
        handleSubmit,
        formState: { errors, isSubmitting },
        reset,
        setValue,
        watch
    } = useForm<JoinGroupFormData>({
        resolver: yupResolver(joinGroupSchema),
        mode: 'onChange'
    });

    const onSubmit = async (data: JoinGroupFormData) => {
        try {
            console.log('Отправка нового юзера группы:', user.id);
            await joinToGroupByCode(data.code, user.id);
            alert(`Вы присоединились к группе с кодом: ${data.code}`);
            reset();
        } catch (error) {
            console.error('Ошибка присоединения:', error);
            alert('Не удалось присоединиться к группе');
        }
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setValue('code', e.target.value.toUpperCase());
    };

    return (
        <div className={styles.groupSelectionForm}>
        <h3 className={styles.head}>Присоединиться к группе</h3>
        <form onSubmit={handleSubmit(onSubmit)} className={styles.form}>
            <div className={styles.formGroup}>
            <label htmlFor="groupCode" className={styles.label}>
                Введите 5-значный код группы:
            </label>
            <input
                id="groupCode"
                type="text"
                className={`${styles.input} ${errors.code ? styles.error : ''}`}
                {...register('code')}
                onChange={handleInputChange}
                value={watch('code') || ''}
                placeholder="Пример: A1B2C"
                maxLength={5}
                autoComplete="off"
            />
            {errors.code && (
                <span className={styles.errorMessage}>{errors.code.message}</span>
            )}
            </div>

            <button
                type="submit"
                className={styles.submitButton}
                disabled={isSubmitting}
            >
            {isSubmitting ? 'Подождите...' : 'Присоединиться'}
            </button>
        </form>
        </div>
    );
}