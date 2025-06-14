import { Navigate, Outlet } from "react-router-dom";
import { useAuth } from "../../auth/AuthContext/AuthContext";


export const GuestRoute = () => {
  const { user } = useAuth();

  if (user) {
    return <Navigate to="/dashboard" replace />;
  }

  return <Outlet />;
}