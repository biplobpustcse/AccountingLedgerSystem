import { useEffect, useState } from 'react'
import { useNavigate, Link } from 'react-router-dom'
import axiosClient from '../api/axiosClient'

interface TrialBalance {
  accountId: number
  accountName: string
  totalDebit: number
  totalCredit: number
  netBalance: number
}

export default function Dashboard() {
  const [trialBalance, setTrialBalance] = useState<TrialBalance[]>([])
  const navigate = useNavigate()

  const fetchTrialBalance = async () => {
    try {
      const res = await axiosClient.get('/JournalEntries/trialbalance')
      setTrialBalance(res.data)
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
    fetchTrialBalance()
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

      <div className="mb-6">
        <Link
          to="/accounts"
          className="text-blue-600 underline hover:text-blue-800 font-medium"
        >
          → Manage Accounts
        </Link>
      </div>

      <h2 className="text-xl font-semibold mb-4">Trial Balance</h2>

      <div className="overflow-x-auto">
        <table className="min-w-full bg-white shadow-md rounded">
          <thead className="bg-gray-100">
            <tr>
              <th className="text-left px-3 py-2">Account</th>
              <th className="text-right px-3 py-2">Debit</th>
              <th className="text-right px-3 py-2">Credit</th>
              <th className="text-right px-3 py-2">Net Balance</th>
            </tr>
          </thead>
          <tbody>
            {trialBalance.length === 0 ? (
              <tr>
                <td colSpan={3} className="text-center py-4 text-gray-500">
                  No data available
                </td>
              </tr>
            ) : (
              trialBalance.map((item) => (
                <tr key={item.accountId} className="border-t">
                  <td className="px-3 py-2 font-medium">{item.accountName}</td>
                  <td className="px-3 py-2 text-right text-green-700">
                    {item.totalDebit > 0 ? item.totalDebit.toFixed(2) : '-'}
                  </td>
                  <td className="px-3 py-2 text-right text-red-700">
                    {item.totalCredit > 0 ? item.totalCredit.toFixed(2) : '-'}
                  </td>
                  <td className="px-3 py-2 text-right text-blue-700">
                    {item.netBalance != 0 ? item.netBalance.toFixed(2) : '-'}
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  )
}