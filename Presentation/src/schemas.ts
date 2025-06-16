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

export const createGroupSchema = yup.object({
  name: yup.string().min(2, 'Минимум 2 символа').required('Обязаетльное поле'),
  type: yup.string().required('Обязательное поле')
})

export const joinGroupSchema = yup.object({
  code: yup
    .string()
    .required('Код группы обязателен')
    .matches(/^[A-Z0-9]{5}$/, 'Код должен состоять из 5 символов (A-Z, 0-9)')
    .transform(value => value.toUpperCase())
})

export const createWishSchema = yup.object({
  name: yup.string().required('Обязательное поле'),
  description: yup.string().min(3, 'Минимум 3 символа в описании').max(50, 'Максимум 50 символов в описании').required('Обязательное поле'),
  priority: yup.string().required('Приоритет обязателен!')
})

export type LoginFormData = yup.InferType<typeof loginSchema>;
export type RegisterFormData = yup.InferType<typeof registerSchema>;
export type UserUpdateFormData = yup.InferType<typeof userUpdateSchema>;
export type CreateGroupFormData = yup.InferType<typeof createGroupSchema>;
export type JoinGroupFormData = yup.InferType<typeof joinGroupSchema>;
export type CreateWishFormData = yup.InferType<typeof createWishSchema>;