import type { Params } from "react-router-dom";
import api from "../interceptors";
import type { WishList } from "../../pages/private/WishListPage/types";

export interface AddNewWishParams {
  wishlistId: string,
  groupMemberId: string,
  name: string,
  description: string,
  priority: string,
  imageUrl: string,
  completed: boolean
}


const API_URL = 'https://localhost:7201/api/Wishlist';

export const addNewWishlist = async (groupId: string) => {
  const response = await api.post(`${API_URL}`, { groupId: groupId, title: 'Wishlist' });
  console.log('New WishList', response.data)
  return response.data as WishList
}

export const getWishList = async (groupId: Readonly<Params<string>>) => {
  const response = await api.get(`${API_URL}/group/${groupId.groupId}`);
  console.log('WishList', response.data)
  return response.data as WishList
}

export const addNewWish = async (params: AddNewWishParams) => {
  const response = await api.post(`${API_URL}/item`, params);
  return response.data
}

export const deleteWish = async (wishListItemId: string) => {
  const response = await api.delete(`${API_URL}/item/${wishListItemId}`);
  return response.data
}