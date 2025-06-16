import { GrCatalog } from "react-icons/gr";
import { GrGroup } from "react-icons/gr";
import { GrSchedule } from "react-icons/gr";
import { GiLoveMystery } from "react-icons/gi";

const familyAchievements = [
    { icon: GrCatalog,  name: 'Пройти 5 тестов', passed: true },
    { icon: GrCatalog, name: 'Пройти 10 тестов', passed: false },
    { icon: GrCatalog, name: 'Пройти 15 тестов', passed: false },
    { icon: GrGroup, name: 'Провести 5 встреч всей семьёй', passed: false },
    { icon: GrGroup, name: 'Провести 10 встреч всей семьёй', passed: false },
    { icon: GrGroup, name: 'Провести 15 встреч всей семьёй', passed: false },
    { icon: GrSchedule, name: 'Провести в приложении 1 день', passed: true },
    { icon: GrSchedule, name: 'Провести в приложении месяц', passed: true },
    { icon: GrSchedule, name: 'Провести в приложении год', passed: true },
]

const pairAchievements = [
    { icon: GrCatalog, name: 'Пройти романтических 5 тестов', passed: true },
    { icon: GrCatalog, name: 'Пройти романтических 10 тестов', passed: false },
    { icon: GrCatalog, name: 'Пройти романтических 15 тестов', passed: false },
    { icon: GiLoveMystery, name: 'Провести 5 свиданий', passed: false },
    { icon: GiLoveMystery, name: 'Провести 10 свиданий', passed: false },
    { icon: GiLoveMystery, name: 'Провести 15 свиданий', passed: false },
    { icon: GrSchedule, name: 'Вместе неделю', passed: true },
    { icon: GrSchedule, name: 'Вместе месяц', passed: true },
    { icon: GrSchedule, name: 'Первая годовщина', passed: true },
]

const friendsAchievements = [
    { icon: GrCatalog, name: 'Пройти 5 тестов на сплочение', passed: true },
    { icon: GrCatalog, name: 'Пройти 10 тестов на сплочение', passed: false },
    { icon: GrCatalog, name: 'Пройти 15 тестов на сплочение', passed: false },
    { icon: GrGroup, name: 'Провести 5 встреч', passed: false },
    { icon: GrGroup, name: 'Провести 10 встреч', passed: false },
    { icon: GrGroup, name: 'Провести 15 встреч', passed: false },
    { icon: GrSchedule, name: 'В приложении неделю', passed: true },
    { icon: GrSchedule, name: 'В приложении месяц', passed: true },
    { icon: GrSchedule, name: 'В приложении год', passed: true },
]

export type GroupType = 0 | 1 | 2;

export const achievementsMap = {
  0: familyAchievements,
  1: pairAchievements,
  2: friendsAchievements
};