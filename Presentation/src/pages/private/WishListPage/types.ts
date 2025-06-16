import type { Member } from "../UserAccountPage/types"

export interface WishList {
    id: string,
    groupId: string,
    title: string,
    createdAt: string,
    updatedAt: string | null,
    group: string | null,
    items: Array<WishListItem>
}

export interface WishListItem {
  id: string,
  wishlistId: string,
  groupMemberId: string,
  groupMember: Member,
  name: string,
  description: string,
  priority: string,
  imageUrl: string,
  completed: boolean
}