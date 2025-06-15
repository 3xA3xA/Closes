import { jwtDecode } from 'jwt-decode';
import type { LoginCredentials, RegisterCredentials, User } from '../../auth/types';
import api from '../interceptors';


const API_URL = 'https://localhost:7201/api/Auth';

export const login = async (credentials: LoginCredentials): Promise<User> => {
  const response = await api.post(`${API_URL}/login`, credentials);
  const { token } = response.data;
  const user = jwtDecode<User>(token);
  console.log('login data', response)
  return { ...user, token };
};

export const register = async (credentials: RegisterCredentials): Promise<User> => {
  console.log('КРЕДЫ', credentials)
  const response = await api.post(`${API_URL}/register`, credentials);
  console.log('register data', response)
  return response.data;
};

export const refreshToken = async (): Promise<string> => {
  const response = await api.post(`${API_URL}/refresh-token`);
  return response.data.accessToken;
};