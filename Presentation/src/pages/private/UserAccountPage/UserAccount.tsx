import { useRef, useState } from 'react'
import { useAuth } from '../../../auth/AuthContext/AuthContext'
import styles from './UserAccountPage.module.css'
import { UserEditForm } from './UserEditForm/UserEditForm'
import plus from '../../../assets/plus.png'
import { updateAvatar } from '../../../api/AccountService/accountService'

export const UserAccountPage = () => {
    const { user, logout } = useAuth()
    const [editingButtonStatus, setEditingButtonStatus] = useState(true);
    const fileInputRef = useRef<HTMLInputElement>(null);
    const [tempAvatar, setTempAvatar] = useState<string | null>(null);

    const handleAvatarClick = () => {
        fileInputRef.current?.click();
    };

    const handleFileChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (!file) return;

        const previewUrl = URL.createObjectURL(file);
        setTempAvatar(previewUrl);

        try {
            const formData = new FormData();
            formData.append('avatar', file);
            
            await updateAvatar(formData);
            
            URL.revokeObjectURL(previewUrl);
            setTempAvatar(null);
        } catch (error) {
            console.error('Ошибка загрузки аватара:', error);
            setTempAvatar(null);
        }
    };

    return (
        <div className={styles.root}>
            <div className={styles.avatarContainer} onClick={handleAvatarClick}>
                <img 
                    className={styles.avatar} 
                    src={tempAvatar || user?.avatarUrl || plus} 
                    alt="avatar" 
                />
                <input
                    type="file"
                    ref={fileInputRef}
                    onChange={handleFileChange}
                    accept="image/*"
                    style={{ display: 'none' }}
                />
            </div>

            <h1 className={styles.username}>{user?.name ?? 'Гость'}</h1>

            <div
             className={styles.editingButton}
             onClick={() => setEditingButtonStatus(prev => !prev)}
            >
                Изменить конфигурацию пользователя
            </div>
            
            {editingButtonStatus && (
                <UserEditForm onClose={() => setEditingButtonStatus(false)}/>
            )}

            <div
              className={styles.logout}
              onClick={logout}
            >
                Выйти из учётной записи
            </div>
        </div>
    )
}