import { useEffect, useState, type Dispatch, type SetStateAction } from 'react'
import { GrCheckmark } from "react-icons/gr";
import { Header } from '../../../components/semantic/Header/Header'
import { NavBar } from '../../../components/semantic/NavBar/NavBar'
import styles from './WishListPage.module.css'
import { deleteWish, getWishList } from '../../../api/WishlistService/wishListService'
import { useParams } from 'react-router-dom'
import type { WishList } from './types'
import { WishCreateForm } from './WishCreateForm/WishCreateForm'

interface WishListPageProps {
    isModalOpen: boolean;
    setIsModalOpen: Dispatch<SetStateAction<boolean>>;
}

export const WishListPage: React.FC<WishListPageProps> = ({ isModalOpen, setIsModalOpen }) => {
    const [wishlist, setWishlist] = useState<WishList | null>(null);   
    const groupId = useParams();
    const { groupMemberId } = useParams();

    useEffect(() => {
        const fetchWishlist = async () => {
            try {
                const responcedWishlist = await getWishList(groupId);
                setWishlist(responcedWishlist);
                console.log(responcedWishlist);
            } catch (err) {
                console.error('Ошибка при загрузке вишлистов:', err);
            }
        };

        fetchWishlist();
    }, [groupId]);

    console.log('wishlist', wishlist)

    return (
        <div className={styles.root}>
            <Header title='Карта желаний'/>

            <main className={styles.main}>
                <ul className={styles.wishList}>
                    {
                        typeof wishlist?.items !== "undefined" && wishlist.items.length > 0 ? (
                            wishlist.items.map((present) => (
                                <li className={styles.wish} style={{border: `2px solid ${present.groupMember.uniqueColor}`}}>
                                    <div className={styles.image} />

                                    <div className={styles.info}>
                                        <div>Название: {present.name}</div>
                                        <div className={styles.subtext}>Описание: {present.description}</div>
                                        <div className={styles.subtext}>Кто хочет: {present.groupMember.user.name}</div>
                                    </div>

                                    {present.groupMemberId === groupMemberId &&

                                        <GrCheckmark 
                                            className={styles.checkMark}
                                            onClick={() => deleteWish(present.id)}
                                        />}
                                </li>

                            ))
                        ) : (
                            <li>К сожалению, не добавлено ни одного желания</li>
                        )
                    }
                </ul>
                
                <div className={styles.createButton}
                  onClick={() => {
                    setIsModalOpen(true)
                  }}
                >
                    Добавить
                </div>

            </main>

            <NavBar />

            {wishlist && groupMemberId && <WishCreateForm
              memberId={groupMemberId}
              wishlistId={wishlist.id}
              isOpen={isModalOpen}
              onClose={() => setIsModalOpen(false)}
            />}
        </div>
    )
}