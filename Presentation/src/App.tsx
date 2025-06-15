import { BrowserRouter, Route, Routes } from 'react-router-dom'
import { GuestRoute } from './components/GuestRoute/GuestRoute'
import { ProtectedRoute } from './components/ProtectedRoute/ProtectedRoute'
import { Login } from './pages/public/LoginPage/Login'
import { RootRedirect } from './components/RootRedirect/RootRedirect'
import { Registration } from './pages/public/RegistrationPage/Registartion'
import { UserAccountPage } from './pages/private/UserAccountPage/UserAccount'
import { useState } from 'react'
import { GroupHomePage } from './pages/private/GroupHomePage/GroupHomePage'
import type { Group } from './pages/private/UserAccountPage/types'

function App() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedGroup, setSelectedGroup] = useState<Group | null>(null)

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<RootRedirect />} />
        
        <Route element={<GuestRoute />}>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Registration />} />
        </Route>

        <Route element={<ProtectedRoute />}>
          <Route path="/userAccount" element={
            <UserAccountPage 
              isModalOpen={isModalOpen}
              selectedGroup={selectedGroup} 
              setIsModalOpen={setIsModalOpen}
              setSelectedGroup={setSelectedGroup}
            />} 
          />
          <Route path="/groupHomePage/:groupId" element={<GroupHomePage />} />
        </Route>

        <Route path="*" element={<div>404 Not Found</div>} />
      </Routes>
    </BrowserRouter>
  )
}

export default App
