import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import ReactDom from 'react-dom/client'
import {BrowserRouter, Route, Routes} from 'react-router-dom'

import './App.css'
import Login from './Components/Login'
import Dashboard from './Components/Dashboard'
import Order from './pages/OrderPage'
import AddOrder from './Components/order-component/AddOrder'
import Employee from './Components/Employee'
import ProductPage from './pages/ProductPage'

function App() {
  return (
    <div>
      <Routes>
        <Route path='/' element ={<Login/>}/>
        <Route path='/dashboard' element ={<Dashboard/>}/>
        <Route path='/order' element={<Order/>}/>
        <Route path='/order/add-order' element= {<AddOrder/>}/>
        <Route path='/employee' element = {<Employee/>}/>
        <Route path='/product' element = {<ProductPage/>}/>
      </Routes>
    </div>
  )
}

export default App
