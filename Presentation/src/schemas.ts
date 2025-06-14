import * as yup from 'yup';

export const loginSchema = yup.object({
  email: yup.string().email('Некорректный email').required('Обязательное поле'),
  password: yup.string().required('Обязательное поле'),
});

export const registerSchema = yup.object({
  email: yup.string().email('Некорректный email').required('Обязательное поле'),
  username: yup.string().min(3, 'Минимум 3 символа').required('Обязательное поле'),
  password: yup.string().min(6, 'Минимум 6 символов').required('Обязательное поле'),
  confirmPassword: yup.string()
    .oneOf([yup.ref('password')], 'Пароли не совпадают')
    .required('Подтвердите пароль'),
});

export const userUpdateSchema = yup.object({
  name: yup.string().min(2, 'Минимум 2 символа').nullable(),
  email: yup.string().email('Некорректный email').nullable(),
  newPassword: yup
    .string()
    .min(6, 'Минимум 6 символов')
    .nullable(),
  oldPassword: yup
    .string()
    .min(6, 'Минимум 6 символов')
    .nullable(),
}).partial();

export type LoginFormData = yup.InferType<typeof loginSchema>;
export type RegisterFormData = yup.InferType<typeof registerSchema>;
export type UserUpdateFormData = yup.InferType<typeof userUpdateSchema>;