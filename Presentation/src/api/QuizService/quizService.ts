import api from "../interceptors";

interface QuizParams  {
  name: string,
  description: string,
  category: string,
  userId: string,
  questions: {text: string}[]
}

const API_URL = 'https://localhost:7201/api/Quiz';

export const createQuiz = async (params: QuizParams) => {
  const response = await api.post(`${API_URL}`, params);
  console.log('New Quiz', response.data)
  return response.data 
}