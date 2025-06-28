import axios from 'axios'

const axiosClient = axios.create({
  baseURL: 'https://localhost:7147/api', // Update if needed
})

axiosClient.interceptors.request.use((config) => {
  const token = localStorage.getItem('accessToken')
  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config
})

export default axiosClient
