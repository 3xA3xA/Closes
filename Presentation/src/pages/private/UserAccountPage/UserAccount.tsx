import { useRef, useState, type Dispatch, type SetStateAction } from 'react'
import { useAuth } from '../../../auth/AuthContext/AuthContext'
import styles from './UserAccountPage.module.css'
import { UserEditForm } from './UserEditForm/UserEditForm'
// import plus from '../../../assets/plus.png'
import { updateAvatar } from '../../../api/AccountService/accountService'
import image from '../../../../../API/Uploads/Avatars/730e658c-4a51-4dc6-8ec3-d9efda46073f.jpg'
import { UserGroupChanger } from './UserGroupChanger/UserGroupChanger'
import { GroupJoinForm } from './GroupJoinForm/GroupJoinForm'
import type { Group } from './types'
import male from '../../../assets/male.jpg'
import female from '../../../assets/female.jpg'

interface UserAccountPageProps {
    isModalOpen: boolean;
    selectedGroup: Group | null;
    setIsModalOpen: Dispatch<SetStateAction<boolean>>;
    setSelectedGroup: Dispatch<SetStateAction<Group | null>>;
}

export const UserAccountPage: React.FC<UserAccountPageProps> = ({ isModalOpen, selectedGroup, setIsModalOpen, setSelectedGroup }) => {
    const { user, logout } = useAuth()
    const [editingButtonStatus, setEditingButtonStatus] = useState(false);
    const fileInputRef = useRef<HTMLInputElement>(null);

    const handleAvatarClick = () => {
        fileInputRef.current?.click();
    };

    const handleFileChange = async (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];

        if (!file) return;
    
        try {
            const formData = new FormData();

            formData.append('id', user?.id ?? '')
            formData.append('avatar', file);
            
            await updateAvatar(formData);
        } catch (error) {
            console.error('Ошибка загрузки аватара:', error);
        }
    };

    return (
        <div className={styles.root}>
            <div className={styles.avatarContainer} onClick={handleAvatarClick}>
                <img 
                    className={styles.avatar} 
                    src={female} 
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

            <UserGroupChanger 
              isModalOpen={isModalOpen}  
              setIsModalOpen={setIsModalOpen} 
              user={user}       
              selectedGroup={selectedGroup}       
              setSelectedGroup={setSelectedGroup}
            />

            <GroupJoinForm user={user!}/>

            <div
              className={styles.logout}
              onClick={logout}
            >
                Выйти из учётной записи
            </div>
        </div>
    )
}