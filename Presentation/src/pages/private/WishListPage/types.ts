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
  wishlistId: string,
  groupMemberId: string,
  name: string,
  description: string,
  priority: string,
  imageUrl: string,
  completed: boolean
}