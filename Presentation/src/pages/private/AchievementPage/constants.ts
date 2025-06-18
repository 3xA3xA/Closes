import { GrCatalog } from "react-icons/gr";
import { GrGroup } from "react-icons/gr";
import { GrSchedule } from "react-icons/gr";
import { GiLoveMystery } from "react-icons/gi";
import type { IconType } from "react-icons";

export type IconKey = 'catalog' | 'group' | 'schedule' | 'love';

export const icons: Record<IconKey, IconType> = {
  catalog: GrCatalog,
  group: GrGroup,
  schedule: GrSchedule,
  love: GiLoveMystery,
};

const familyAchievements: { icon: IconKey, name: string, passed: boolean, priority: number }[] = [
    { icon: 'catalog',  name: 'Пройти 5 тестов', passed: true, priority: 0 },
    { icon: 'catalog', name: 'Пройти 10 тестов', passed: false, priority: 1 },
    { icon: 'catalog', name: 'Пройти 15 тестов', passed: false, priority: 2 },
    { icon: 'group', name: 'Провести 5 встреч всей семьёй', passed: false, priority: 0 },
    { icon: 'group', name: 'Провести 10 встреч всей семьёй', passed: false, priority: 1 },
    { icon: 'group', name: 'Провести 15 встреч всей семьёй', passed: false, priority: 2},
    { icon: 'schedule', name: 'Провести в приложении 1 день', passed: true, priority: 0 },
    { icon: 'schedule', name: 'Провести в приложении месяц', passed: true, priority: 1 },
    { icon: 'schedule', name: 'Провести в приложении год', passed: true, priority: 2 },
]

const pairAchievements: { icon: IconKey, name: string, passed: boolean, priority: number }[] = [
    { icon: 'catalog', name: 'Пройти романтических 5 тестов', passed: true, priority: 0 },
    { icon: 'catalog', name: 'Пройти романтических 10 тестов', passed: false, priority: 1 },
    { icon: 'catalog', name: 'Пройти романтических 15 тестов', passed: false, priority: 2 },
    { icon: 'love', name: 'Провести 5 свиданий', passed: false, priority: 0 },
    { icon: 'love', name: 'Провести 10 свиданий', passed: false, priority: 1 },
    { icon: 'love', name: 'Провести 15 свиданий', passed: false, priority: 2 },
    { icon: 'schedule', name: 'Вместе неделю', passed: true, priority: 0 },
    { icon: 'schedule', name: 'Вместе месяц', passed: true, priority: 1 },
    { icon: 'schedule', name: 'Первая годовщина', passed: true, priority: 2 },
]

const friendsAchievements: { icon: IconKey, name: string, passed: boolean, priority: number }[] = [
    { icon: 'catalog', name: 'Пройти 5 тестов на сплочение', passed: true, priority: 0 },
    { icon: 'catalog', name: 'Пройти 10 тестов на сплочение', passed: false, priority: 1 },
    { icon: 'catalog', name: 'Пройти 15 тестов на сплочение', passed: false, priority: 2 },
    { icon: 'group', name: 'Провести 5 встреч', passed: false, priority: 0 },
    { icon: 'group', name: 'Провести 10 встреч', passed: false, priority: 1 },
    { icon: 'group', name: 'Провести 15 встреч', passed: false, priority: 2 },
    { icon: 'schedule', name: 'В приложении неделю', passed: true, priority: 0 },
    { icon: 'schedule', name: 'В приложении месяц', passed: true, priority: 1 },
    { icon:'schedule', name: 'В приложении год', passed: true, priority: 2 },
]

export type GroupType = 0 | 1 | 2;

export const achievementsMap = {
  0: familyAchievements,
  1: pairAchievements,
  2: friendsAchievements
};