import { useEffect, useState } from 'react'
import axiosClient from '../api/axiosClient'
import { useNavigate } from 'react-router-dom'

interface Account {
  id: number
  name: string
  type: string
}

export default function AccountsPage() {
  const [accounts, setAccounts] = useState<Account[]>([])
  const [name, setName] = useState('')
  const [type, setType] = useState('Asset')
  const navigate = useNavigate()

  const fetchAccounts = async () => {
    try {
      const res = await axiosClient.get('/accounts')
      setAccounts(res.data)
    } catch {
      alert('Session expired. Please log in again.')
      localStorage.removeItem('accessToken')
      navigate('/login')
    }
  }

  const handleAddAccount = async (e: React.FormEvent) => {
    e.preventDefault()
    try {
      await axiosClient.post('/accounts', { name, type })
      setName('')
      setType('Asset')
      fetchAccounts()
    } catch {
      alert('Failed to create account.')
    }
  }

  useEffect(() => {
    fetchAccounts()
  }, [])

  return (
    <div className="min-h-screen bg-gray-50 p-6">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Accounts</h1>
        <button
          onClick={() => navigate('/')}
          className="bg-gray-600 text-white px-4 py-2 rounded hover:bg-gray-700"
        >
          Back to Dashboard
        </button>
      </div>

      <form onSubmit={handleAddAccount} className="bg-white p-4 rounded shadow-md mb-6 min-w-full">
        <h2 className="text-xl font-semibold mb-4">Add New Account</h2>
        <input
          type="text"
          placeholder="Account Name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
          className="w-full p-2 border mb-4 rounded"
        />
        <select
          value={type}
          onChange={(e) => setType(e.target.value)}
          className="w-full p-2 border mb-4 rounded"
        >
          <option value="Asset">Asset</option>
          <option value="Liability">Liability</option>
          <option value="Equity">Equity</option>
          <option value="Revenue">Revenue</option>
          <option value="Expense">Expense</option>
        </select>
        <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
          Add Account
        </button>
      </form>

      <h2 className="text-xl font-semibold mb-4">All Accounts</h2>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        {accounts.map((acc) => (
          <div key={acc.id} className="bg-white p-4 shadow rounded border-l-4 border-blue-500">
            <p className="font-bold">{acc.name}</p>
            <p className="text-sm text-gray-500">{acc.type}</p>
          </div>
        ))}
      </div>
    </div>
  )
}
