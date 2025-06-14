import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './main.css'
import './index.css'
import App from './App.tsx'
import { AuthProvider, useAuth } from './auth/AuthContext/AuthContext.tsx'
import { setupInterceptors } from './api/interceptors.ts'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <AuthProvider>
      <AppWrapper />
    </AuthProvider>
    
  </StrictMode>,
)

function AppWrapper() {
  const { logout } = useAuth();
  setupInterceptors(logout);
  return <App />;
}
