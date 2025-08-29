import React from 'react'
import '../styles/order.css'
function order() {
  return (
    <div className='order-container'>
        <div className="search-container">
            <input type="text" placeholder="Nhập từ khóa tìm kiếm" />
            <button>Tìm kiếm</button>
        </div>
    </div>
  )
}export default order