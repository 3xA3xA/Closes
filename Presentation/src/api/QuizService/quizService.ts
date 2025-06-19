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

interface GetPassedQuizesByIdsParams {
  groupMemberId: string
}

interface GetAnswersQuizByIdsParams {
  quizId: string;
  groupMemberId: string
}

interface GetPassedQuizMembersByIds {
  quizId: string;
  groupId: string;
  excludeMemberId: string

}

export interface ExtendedUser {
  id: string,
  userId: string,
  groupId: string,
  joinedAt: string,
  role: number,
  uniqueColor: string,
  user: {
    id: string,
    name: string,
    email: string,
    passwordHash: string,
    avatarUrl: string,
    wishlists: [],
    createdWishlistItems: [],
    activityMembers: []
  },
  group: null
  quizItems: any
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

export const getAnswersQuizByIds = async (params: GetAnswersQuizByIdsParams) => {
  const response = await api.get(`${API_URL}/with-answers/${params.quizId}/${params.groupMemberId}`);
  return response.data
}

export const submitQuestionsQuiz = async (params: SubmitQuestionsQuiz) => {
  const response = await api.post(`${API_URL}/submit`, params);
  return response.data
}

export const getPassedQuizesByIds = async (params: GetPassedQuizesByIdsParams) => {
  const response = await api.get(`${API_URL}/for-member/${params.groupMemberId}`);
  return response.data as QuizItem[]
}

export const getPassedQuizMembersByIds = async (params: GetPassedQuizMembersByIds) => {
  const response = await api.get(`${API_URL}/quiz-members/${params.quizId}/${params.groupId}/${params.excludeMemberId}`);
  return response.data as ExtendedUser[]
}
