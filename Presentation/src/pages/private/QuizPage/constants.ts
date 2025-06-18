import type { IconType } from "react-icons";
import { GiNotebook } from "react-icons/gi";
import { GiArchiveResearch } from "react-icons/gi";
import { GiCompass } from "react-icons/gi";
import { GiLoveMystery } from "react-icons/gi";
import { GrCatalog } from "react-icons/gr";

export type QuizCategory = 'general' | 'love' | 'compatibility' | 'facts' | 'plans';

export interface Quiz {
  name: string;
  description: string;
  category: number; // 1-5
  createdAt: string;
}

export const icons: Record<QuizCategory, IconType> = {
  general: GrCatalog,
  love: GiLoveMystery,
  compatibility: GiCompass,
  facts: GiArchiveResearch,
  plans: GiNotebook
};

// Константы для категорий
export const CATEGORIES = {
  0: { name: 'Общие', color: '#6366f1', icon: 'general' },
  1: { name: 'Любовь', color: '#ec4899', icon: 'love' },
  2: { name: 'Совместимость', color: '#14b8a6', icon: 'compatibility' },
  3: { name: 'Факты', color: '#f59e0b', icon: 'facts' },
  4: { name: 'Планы', color: '#8b5cf6', icon: 'plans' },
};

