export interface User {
  id: number;
  name: string;
  email: string;
  token: string;
  avatarUrl: string
}

export interface AuthContextType {
    user: User | null;
    login: (credentials: LoginCredentials) => void;
    registration: (credentials: RegisterCredentials) => void;
    logout: () => void;
    checkAuth: () => Promise<void>;
}

export interface LoginCredentials {
  email: string;
  password: string;
}
  
  export interface RegisterCredentials extends LoginCredentials {
    name: string;
  }