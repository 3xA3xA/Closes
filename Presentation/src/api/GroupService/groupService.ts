import api from "../interceptors";

const API_URL = 'https://localhost:7201/api/Group';

export interface GroupInfo {
    name: string;
    type: number;
    ownerId: string
}

export const createGroup = async (groupInfo: GroupInfo) => {
  const response = await api.post(`${API_URL}`, groupInfo);
  console.log('инфа по группе', response.data)
  return response.data
}

export const getGroupsByUserId = async (userId: string) => {
  const response = await api.get(`${API_URL}/user/${userId}`);
  return response.data
}

export const joinToGroupByCode = async (code: string, userId: string) => {
  const response = await api.post(`${API_URL}/join/${code}?userId=${userId}`);
  console.log('join data', response.data);
  return response.data
}

