export interface QuizQuestion {
  text: string;
}

export interface QuizFormValues {
  name: string;
  description: string;
  category: string;
  questions: QuizQuestion[];
}