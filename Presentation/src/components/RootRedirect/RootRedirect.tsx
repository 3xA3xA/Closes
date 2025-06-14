import { Navigate } from 'react-router-dom';
import { useAuth } from '../../auth/AuthContext/AuthContext';

export function RootRedirect() {
  const { user } = useAuth();
  
  return user ? (
    <Navigate to="/userAccount" replace />
  ) : (
    <Navigate to="/login" replace />
  );
}