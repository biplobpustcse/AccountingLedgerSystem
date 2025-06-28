import { Routes, Route, Navigate } from 'react-router-dom'
import LoginPage from './pages/LoginPage'
import Dashboard from './pages/Dashboard'
import AccountsPage from './pages/AccountsPage'
import JournalEntryPage from './pages/JournalEntryPage'


function App() {
  const isAuthenticated = !!localStorage.getItem('accessToken')

  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route path="/" element={isAuthenticated ? <Dashboard /> : <Navigate to="/login" />} />
      <Route path="/accounts" element={isAuthenticated ? <AccountsPage /> : <Navigate to="/login" />} />
      <Route path="/journal-entry" element={isAuthenticated ? <JournalEntryPage /> : <Navigate to="/login" />}/>
    </Routes>
  )
}

export default App
