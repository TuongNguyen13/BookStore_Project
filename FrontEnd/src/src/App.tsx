import Login from './Components/Login'
import { Routes, Route } from 'react-router-dom'
import Order from './Components/order'
import AddBook from './Components/AddBook'

function App() {
  return (
   <Routes>
    <Route path='/' element={<Login/>}/>
    <Route path='/order' element={<Order/>}/>
    <Route path='/order/Them-Thong-Tin-Sach' element={<AddBook/>}/>
   </Routes>
    
  )
}

export default App
