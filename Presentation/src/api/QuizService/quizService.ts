import type { QuizItem } from "../../pages/private/QuizPage/types";
import api from "../interceptors";

interface CreateQuizParams  {
  name: string,
  description: string,
  category: string,
  userId: string,
  questions: {text: string}[]
}

interface SubmitQuestionsQuiz {
  groupMemberId: string,
  quizId: string,
  answers: string[]
}

const API_URL = 'https://localhost:7201/api/Quiz';

export const createQuiz = async (params: CreateQuizParams) => {
  const response = await api.post(`${API_URL}`, params);
  console.log('New Quiz', response.data)
  return response.data 
}

export const getQuizes = async () => {
  const response = await api.get(`${API_URL}`);
  return response.data
}

export const getQuestionsQuizById = async (params: string) => {
  const response = await api.get(`${API_URL}/with-items/${params}`);
  return response.data as QuizItem
}

export const submitQuestionsQuiz = async (params: SubmitQuestionsQuiz) => {
  const response = await api.post(`${API_URL}/submit`, params);
  return response.data
}