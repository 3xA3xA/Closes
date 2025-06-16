import { useEffect, useState, type Dispatch, type SetStateAction } from 'react'
import { Header } from '../../../components/semantic/Header/Header'
import { NavBar } from '../../../components/semantic/NavBar/NavBar'
import styles from './WishListPage.module.css'
import { getWishList } from '../../../api/WishlistService/wishListService'
import { useParams } from 'react-router-dom'
import type { WishList } from './types'
import { WishCreateForm } from './WishCreateForm/WishCreateForm'
import { getGroupMemberByUserAndGroupIds } from '../../../api/GroupService/groupService'
import { useAuth } from '../../../auth/AuthContext/AuthContext'
import type { Member } from '../UserAccountPage/types'

interface WishListPageProps {
    isModalOpen: boolean;
    setIsModalOpen: Dispatch<SetStateAction<boolean>>;
}

export const WishListPage: React.FC<WishListPageProps> = ({ isModalOpen, setIsModalOpen }) => {
    const [wishlist, setWishlist] = useState<WishList | null>(null);   
    const { user } = useAuth()
    const groupId = useParams();
    const [member, setMember] = useState<Member | null>(null);

    useEffect(() => {
        getGroupMemberByUserAndGroupIds(user!.id, groupId.groupId!)
            .then(
                setMember
            )
            .catch(console.error);
    }, [user, groupId.groupId]);

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
            <Header />

            <main className={styles.main}>
                <ul className={styles.wishList}>
                    {
                        typeof wishlist?.items !== "undefined" && wishlist.items.length > 0 ? (
                            wishlist.items.map((present) => (
                                <li>
                                    {present.name}
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

            {wishlist && member && <WishCreateForm
              memberId={member.id}
              wishlistId={wishlist.id}
              isOpen={isModalOpen}
              onClose={() => setIsModalOpen(false)}
            />}
        </div>
    )
}