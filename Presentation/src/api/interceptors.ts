import axios from 'axios';

export const api = axios.create({
  baseURL: 'https://localhost:7201/api',
});

export const setupInterceptors = (logout: () => void) => {
  api.interceptors.request.use(
    (config) => {
      const token = localStorage.getItem('accessToken');
      console.log('Я в интерсепторе токенов!')
      if (token) {
        console.log('Подставляю токен!')
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    },
    (error) => Promise.reject(error)
  );

  api.interceptors.response.use(
    (response) => response,
    async (error) => {
      if (error.response?.status === 401) {
        logout();
      }
      return Promise.reject(error);
    }
  );
};

export default api;