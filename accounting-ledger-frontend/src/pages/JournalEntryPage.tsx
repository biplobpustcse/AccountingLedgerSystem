import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'
import axiosClient from '../api/axiosClient'

interface Account {
  id: number
  name: string
  type: string
}

interface Line {
  accountId: number
  debit: number
  credit: number
}

export default function JournalEntryPage() {
  const [accounts, setAccounts] = useState<Account[]>([])
  const [description, setDescription] = useState('')
  const [date, setDate] = useState('')
  const [lines, setLines] = useState<Line[]>([{ accountId: 0, debit: 0, credit: 0 }])
  const navigate = useNavigate()

  useEffect(() => {
    fetchAccounts()
  }, [])

  const fetchAccounts = async () => {
    try {
      const res = await axiosClient.get('/accounts')
      setAccounts(res.data)
    } catch {
      alert('Failed to load accounts.')
    }
  }

  const addLine = () => {
    setLines([...lines, { accountId: 0, debit: 0, credit: 0 }])
  }

  const updateLine = (index: number, key: keyof Line, value: string | number) => {
    const updated = [...lines]
    updated[index][key] = key === 'accountId' ? +value : parseFloat(value.toString())
    setLines(updated)
  }

  const removeLine = (index: number) => {
    setLines(lines.filter((_, i) => i !== index))
  }

  const totalDebit = lines.reduce((sum, l) => sum + l.debit, 0)
  const totalCredit = lines.reduce((sum, l) => sum + l.credit, 0)

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()

    if (totalDebit !== totalCredit) {
      alert('Total Debit and Credit must be equal!')
      return
    }

    try {
      await axiosClient.post('/journalentries', {
        date,
        description,
        lines
      })
      alert('Journal entry added successfully!')
      navigate('/')
    } catch (err) {
      alert('Failed to create journal entry.')
    }
  }

  return (
    <div className="min-h-screen p-6 bg-gray-50">
      <div className="max-w-3xl mx-auto bg-white shadow p-6 rounded">
        <div className="flex justify-between items-center mb-6">
          <h1 className="text-2xl font-bold">Journal Entry</h1>
          <button
            onClick={() => navigate('/')}
            className="bg-gray-600 text-white px-4 py-2 rounded hover:bg-gray-700"
          >
            Back to Dashboard
          </button>
        </div>

        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <input
              type="date"
              value={date}
              onChange={(e) => setDate(e.target.value)}
              required
              className="w-full p-2 border rounded"
            />
          </div>
          <div className="mb-4">
            <textarea
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              placeholder="Description"
              className="w-full p-2 border rounded"
              required
            ></textarea>
          </div>

          <h2 className="text-lg font-semibold mb-2">Lines</h2>

          {/* Column headers */}
          <div className="grid grid-cols-4 gap-4 font-semibold text-gray-700 mb-2">
            <div className="col-span-2">Account</div>
            <div className="text-right">Debit</div>
            <div className="text-right">Credit</div>
          </div>

          {lines.map((line, index) => (
            <div key={index} className="grid grid-cols-4 gap-4 mb-2 items-center">
              <select
                value={line.accountId}
                onChange={(e) => updateLine(index, 'accountId', e.target.value)}
                className="p-2 border rounded col-span-2"
              >
                <option value={0}>Select Account</option>
                {accounts.map((acc) => (
                  <option key={acc.id} value={acc.id}>
                    {acc.name}
                  </option>
                ))}
              </select>
              <input
                type="number"
                placeholder="Debit"
                value={line.debit}
                onChange={(e) => updateLine(index, 'debit', e.target.value)}
                className="p-2 border rounded text-right"
              />
              <input
                type="number"
                placeholder="Credit"
                value={line.credit}
                onChange={(e) => updateLine(index, 'credit', e.target.value)}
                className="p-2 border rounded text-right"
              />
              {lines.length > 1 && (
                <button
                  type="button"
                  onClick={() => removeLine(index)}
                  className="text-sm text-red-500"
                >
                  Remove
                </button>
              )}
            </div>
          ))}

          <div className="mb-4 flex justify-between items-center">
            <button
              type="button"
              onClick={addLine}
              className="bg-gray-200 px-3 py-1 rounded hover:bg-gray-300"
            >
              + Add Line
            </button>
            <div className="text-sm font-medium text-gray-600">
              Total Debit: {totalDebit.toFixed(2)} | Total Credit: {totalCredit.toFixed(2)}
            </div>
          </div>

          <button
            type="submit"
            disabled={
              totalDebit <= 0 ||
              totalCredit <= 0 ||
              totalDebit !== totalCredit ||
              lines.some((l) => l.accountId === 0)
            }
            className={`${
              totalDebit <= 0 ||
              totalCredit <= 0 ||
              totalDebit !== totalCredit ||
              lines.some((l) => l.accountId === 0)
                ? 'bg-gray-400 cursor-not-allowed'
                : 'bg-blue-600 hover:bg-blue-700'
            } text-white px-4 py-2 rounded`}
          >
            Submit Entry
          </button>
        </form>
      </div>
    </div>
  )
}
