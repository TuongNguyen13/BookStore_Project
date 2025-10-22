import Login from './Components/Login'
import { Routes, Route } from 'react-router-dom'
import Order from './Components/Order'
import AddBook from './Components/AddBook'
import AddOrder from './Components/AddOrder'
import Dashboard from './Components/Dashboard'

function App() {
  return (
   <Routes>
    <Route path='/' element={<Login/>}/>
    <Route path='/dashboard' element={<Dashboard/>}/>
    <Route path='/order' element={<Order/>}/>
<Route path='/order/add-order' element={<AddOrder/>}/>
    {/* <Route path='/order/Them-Thong-Tin-Sach' element={<AddBook/>}/> */}
   </Routes>
    
  )
}

export default App
