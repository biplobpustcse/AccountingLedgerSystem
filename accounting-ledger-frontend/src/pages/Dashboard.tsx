import { useEffect, useState } from 'react'
import axiosClient from '../api/axiosClient'
import { useNavigate } from 'react-router-dom'

interface Account {
  id: number
  name: string
  type: string
}

export default function Dashboard() {
  const [accounts, setAccounts] = useState<Account[]>([])
  const navigate = useNavigate()

  const fetchAccounts = async () => {
    try {
      const res = await axiosClient.get('/accounts')
      setAccounts(res.data)
    } catch (err) {
      alert('Session expired. Please log in again.')
      localStorage.removeItem('accessToken')
      navigate('/login')
    }
  }

  const logout = () => {
    localStorage.removeItem('accessToken')
    navigate('/login')
  }

  useEffect(() => {
    fetchAccounts()
  }, [])

  return (
    <div className="min-h-screen bg-gray-50 p-6">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Dashboard</h1>
        <button
          onClick={logout}
          className="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-600"
        >
          Logout
        </button>
      </div>

      <h2 className="text-xl font-semibold mb-4">Accounts</h2>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        {accounts.map((acc) => (
          <div
            key={acc.id}
            className="bg-white p-4 shadow rounded border-l-4 border-blue-500"
          >
            <p className="font-bold">{acc.name}</p>
            <p className="text-sm text-gray-500">{acc.type}</p>
          </div>
        ))}
      </div>
    </div>
  )
}
