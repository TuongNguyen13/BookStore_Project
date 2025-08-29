import React from 'react'
import Login from './Components/Login'
import './App.css'
import { Routes, Route } from 'react-router-dom'
import Order from './Components/order'

function App() {
  return (
   <Routes>
    <Route path='/' element={<Login/>}/>
    <Route path='/order' element={<Order/>}/>
   </Routes>
    
  )
}

export default App
