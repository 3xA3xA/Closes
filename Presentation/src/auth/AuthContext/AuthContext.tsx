import { createContext, useCallback, useContext, useEffect, useState } from "react";
import type { AuthContextType, LoginCredentials, RegisterCredentials, User } from "../types";
import { jwtDecode } from "jwt-decode";
import { login as apiLogin, register as apiRegister } from "../../api/AuthService/authService";

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [user, setUser] = useState<User | null>(null);

  const checkTokenExpiration = useCallback((token: string): boolean => {
    const decoded = jwtDecode<{ exp: number }>(token);
    return decoded.exp * 1000 > Date.now();
  }, []);

  const login = useCallback(async (credentials: LoginCredentials) => {
    try {
      const userData = await apiLogin(credentials);
      localStorage.setItem('accessToken', userData.token || '');
      console.log('userData при логинации', userData)
      setUser(userData);
    } catch (error) {
      console.error('Login failed:', error);
      throw error;
    }
  }, []);

  const registration = useCallback(async (credentials: RegisterCredentials) => {
    try {
      const userData = await apiRegister(credentials);
      const { token } = userData;
      localStorage.setItem('accessToken', token || '');
      setUser(userData);
    } catch {
      console.error('Registration failed');
    }
  }, [])
  
  const logout = useCallback(() => {
    localStorage.removeItem('accessToken');
    setUser(null);
  }, []);

  const checkAuth = useCallback(async () => {
    // const token = localStorage.getItem('accessToken');
    
    // if (!token) {
    //   logout();
    //   return;
    // }
  
    // try {
    //   if (checkTokenExpiration(token)) {
    //     const userData = jwtDecode<User>(token);
        
    //     if (!userData?.id) {
    //       throw new Error('Invalid token payload');
    //     }
        
    //     setUser({ ...userData, token: token });
    //     return;
    //   }
      
    //   const newToken = await refreshToken();
    //   localStorage.setItem('accessToken', newToken);
      
    //   const newUserData = jwtDecode<User>(newToken);
    //   if (!newUserData?.id) {
    //     throw new Error('Invalid refreshed token payload');
    //   }
      
    //   setUser({ ...newUserData, token: newToken });
      
    // } catch (error) {
    //   console.error('Authentication check failed:', error);
    //   logout();
    // }
  }, [checkTokenExpiration, logout]);

  useEffect(() => {
    checkAuth();
  }, [checkAuth]);

  return (
    <AuthContext.Provider value={{ user, login, registration, logout, checkAuth }}>
      {children}
    </AuthContext.Provider>
  );
}

export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
};