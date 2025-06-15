import styles from './UserGroupChanger.module.css'
import { MdFamilyRestroom } from "react-icons/md";
import { GiLovers } from "react-icons/gi";
import { GiThreeFriends } from "react-icons/gi";

export const UserGroupChanger = () => {
    const groups =[
        // {
        //     type: 'Family',
        //     name: 'Керчики',
        //     members: ['Martin', 'Gloria', 'Gleb']
        // },
        // {
        //     type: 'Pair',
        //     name: 'Вавиловы',
        //     members: ['Gloria', 'Gleb']
        // },
        // {
        //     type: 'Friends',
        //     name: 'SWAG',
        //     members: ['Peter', 'Gloria', 'Gleb', 'Vasya']
        // }
    ]

    return (
        <section className={styles.root}>
            <p className={styles.title}>{groups.length ? 'Выберите группу' : 'Вы не присоединились ни к одной из групп'}</p>

            <ul className={styles.groupsList}>
                {
                    groups.length ? (
                        groups.map((groupItem) => (
                            <li
                                className={styles.existedGroup} 
                                style={{ 
                                    backgroundColor: 
                                        groupItem.type === 'Family' ? '#34D399' :
                                        groupItem.type === 'Pair' ? '#FBCFE8' :
                                        groupItem.type === 'Friends' ? '#82A6DD' : 
                                        'transparent' // цвет по умолчанию, если тип не совпадает
                            }}>
                                <div className={styles.iconGroup}>
                                    <div className={styles.icon}>
                                        {
                                            groupItem.type === 'Family' ? <MdFamilyRestroom /> :
                                            groupItem.type === 'Pair' ? <GiLovers /> :
                                            groupItem.type === 'Friends' ? <GiThreeFriends /> : 
                                            ''
                                        }
                                    </div>
                                    
                                </div>

                                <div className={styles.infoGroup}>
                                    <p className={styles.name}>Название: {groupItem.name}</p>
                                
                                    <div className={styles.headOfListMembers}>
                                        Список участников:
                                    </div>

                                    <ul className={styles.listOfMembers}>
                                        {groupItem.members.map((member) => (
                                            <>
                                                <li className={styles.member}>{member}</li>
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
                            <li className={styles.family}>
                                Создать группу
                            </li>
                        </>
                    )
                }
            </ul>
        </section>
    )
}