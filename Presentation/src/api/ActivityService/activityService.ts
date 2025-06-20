import api from "../interceptors";

const API_URL = 'https://localhost:7201/api/Activity';

export interface CreateGroupActivityParams {
    name: string,
    description: string,
    type: number,
    status: number,
    startAt: Date,
    endAt: Date | null,
    groupId: string
}

export const createGroupActivity = async (params: CreateGroupActivityParams) => {
  const response = await api.post(`${API_URL}`, params);
  console.log('инфа по активности', response.data)
  return response.data
}

export const getGroupActivity = async (groupId: string) => {
    const response = await api.get(`${API_URL}/by-group/${groupId}`);
    return response.data
}