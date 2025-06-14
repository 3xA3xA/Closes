import { BrowserRouter, Route, Routes } from 'react-router-dom'
import { GuestRoute } from './components/GuestRoute/GuestRoute'
import { ProtectedRoute } from './components/ProtectedRoute/ProtectedRoute'
import { Login } from './pages/public/LoginPage/Login'
import { RootRedirect } from './components/RootRedirect/RootRedirect'
import { Registration } from './pages/public/RegistrationPage/Registartion'
import { UserAccountPage } from './pages/private/UserAccountPage/UserAccount'

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<RootRedirect />} />
        
        <Route element={<GuestRoute />}>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Registration />} />
        </Route>

        <Route element={<ProtectedRoute />}>
          <Route path="/userAccount" element={<UserAccountPage />} />
        </Route>

        <Route path="*" element={<div>404 Not Found</div>} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
