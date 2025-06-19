import styles from './UserGroupChanger.module.css'
import { MdFamilyRestroom } from "react-icons/md";
import { GiLovers } from "react-icons/gi";
import { GiThreeFriends } from "react-icons/gi";
import { useEffect, useState, type Dispatch, type SetStateAction } from 'react';
import { GroupCreateForm } from '../GroupCreateForm/GroupCreateForm';
import type { User } from '../../../../auth/types';
import { getGroupMemberByUserAndGroupIds, getGroupsByUserId } from '../../../../api/GroupService/groupService';
import type { Group, Member } from '../types';
import { useNavigate } from 'react-router-dom';

interface UserGroupChangerProps {
    isModalOpen: boolean;
    setIsModalOpen: Dispatch<SetStateAction<boolean>>;
    user: User | null;
    selectedGroup: Group | null;
    setSelectedGroup: Dispatch<SetStateAction<Group | null>>;
}

export const UserGroupChanger: React.FC<UserGroupChangerProps> = ({ isModalOpen, setIsModalOpen, user, selectedGroup, setSelectedGroup }) => {
    const [groups, setGroups] = useState<Group[]>([]);
    const [groupMember, setGroupMember] = useState<Partial<Member>>({})

    const navigate = useNavigate();

    useEffect(() => {
        const fetchGroups = async () => {
            try {
                const userGroups = await getGroupsByUserId(user?.id ?? '');
                setGroups(userGroups);
            } catch (err) {
                console.error('Ошибка при загрузке групп:', err);
            }
        };

        fetchGroups();
    }, [user]);

    useEffect(() => {
        const fetchGroupMember = async () => {
            try {
                const groupMemberRes = await getGroupMemberByUserAndGroupIds(user?.id ?? '', selectedGroup?.id ?? '');
                setGroupMember(groupMemberRes)
            } catch (err) {
                console.error('Ошибка при загрузке участника группы:', err);
            }
        } 
        
        fetchGroupMember()
    })

    useEffect(() => {
        if (selectedGroup && groupMember.id) {
            navigate(`/groupHomePage/${selectedGroup.id}/${groupMember.id}`);
        }
    }, [selectedGroup, groupMember, navigate]);

    return (
        <>
            <section className={styles.root}>
                <p className={styles.title}>{groups.length ? 'Выберите группу' : 'Вы не присоединились ни к одной из групп'}</p>

                <ul className={styles.groupsList}>
                    {
                        groups.length ? (
                            groups.map((groupItem) => (
                                <li
                                    onClick={() => {
                                        setSelectedGroup(groupItem);
                                    }}
                                    className={styles.existedGroup} 
                                    style={{ 
                                        backgroundColor: 
                                            groupItem.type === 0 ? '#34D399' :
                                            groupItem.type === 1 ? '#FBCFE8' :
                                            groupItem.type === 2 ? '#82A6DD' : 
                                            'transparent' 
                                }}>
                                    <div className={styles.iconGroup}>
                                        {
                                            groupItem.type === 0 ? <MdFamilyRestroom className={styles.icon}/> :
                                            groupItem.type === 1 ? <GiLovers  className={styles.icon}/> :
                                            groupItem.type === 2 ? <GiThreeFriends  className={styles.icon}/> : 
                                            ''
                                        }
                                    </div>

                                    <div className={styles.infoGroup}>
                                        <p className={styles.name}>Название: {groupItem.name}</p>
                                    
                                        <div className={styles.headOfListMembers}>
                                            Список участников:
                                        </div>

                                        <ul className={styles.listOfMembers}>
                                            {groupItem.members.map((member) => (
                                                <>
                                                    <li className={styles.member}>{member.userName}</li>
                                                </>
                                            ))}
                                            
                                        </ul>
                                    </div>
                                    <p>
                                        
                                    </p>
                                </li>
                            ))
                        ) : (
                            <>
                                
                            </>
                        )
                    }
                </ul>
            </section>

            <div className={styles.family} 
                onClick={() => {
                    setIsModalOpen(true)
                }
            }>
                Создать группу
            </div>

            <GroupCreateForm
                user={user}
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
            />
        </>
    )
}

