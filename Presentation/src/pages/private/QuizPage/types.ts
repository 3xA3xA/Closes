
export type QuizCategory = 'Общие' | 'Любовь' | 'Совместимость' | 'Факты' | 'Планы';

export interface QuizQuestion {
  text: string;
}

export interface QuizFormValues {
  name: string;
  description: string;
  category: QuizCategory;
  questions: QuizQuestion[];
}