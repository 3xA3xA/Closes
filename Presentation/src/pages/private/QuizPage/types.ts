export interface QuizQuestion {
  text: string;
}

export interface QuizFormValues {
  name: string;
  description: string;
  category: string;
  questions: QuizQuestion[];
}

export interface QuizQuestion {
  id: string,
  quizId: string,
  order: number,
  text: string
}

export interface QuizItem {
  id: string,
  name: string,
  category: string,
  description: string,
  createdAt: string,
  userId: string,
  quizItems: QuizQuestion[]
}