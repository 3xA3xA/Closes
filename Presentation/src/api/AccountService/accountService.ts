import api from "../interceptors";

const API_URL = 'https://localhost:7201/api/Account';

export const updateUser = async (newUserData: FormData) => {
  const response = await api.put(`${API_URL}/updateAccount`, newUserData);
  return response.data
};

export const updateAvatar = async (newAvatar: FormData) => {
  const responce = await api.post(`${API_URL}/avatar`, newAvatar);
  return responce.data
}