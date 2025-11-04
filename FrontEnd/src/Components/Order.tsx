import { Navigate, useNavigate } from 'react-router-dom'
import '../styles/order.css'
import React, { useEffect, useState } from 'react'

interface Products {
  productCode: string;
  productName: string;
  Price: string;
  ProductYear: number;
  StockQuantity: number;
  ProductImageUrl: string;
}

function Order() {
  
  const [products, setProducts] = useState<Products[]>([]);
  const navigate = useNavigate();
  const [searchTerm, setSearchTerm] = useState('');

  useEffect(() => {
    document.title = 'Đơn đặt hàng'

    const fetchProductCode = async () => {
      try {
        const response = await fetch('http://localhost:8080/api/v1/products/getProductCode');
        const data = await response.json();
        setProducts(data.productCode);
      } catch (error) {
        console.error('Lỗi tải dữ liệu products:', error);
      }
    };
    fetchProductCode();
  }, []);



  const handelAddAndEditOrder = (str: string) => {
    if(str === 'add'){
    navigate("/order/add-order");
    } else if(str === 'edit'){
      navigate("/order/edit-order");
    }
    
  }

  const handleDashboardClick = () => {
      window.location.href = "/dashboard";
  }

  const handleDeltailProduct = (productCode : string) => {
    navigate(`/order/Chi-Tiet-Sach/${productCode}`);
  }

 const filterProducts = products.filter(product => {
  const code = product.productCode?.toLowerCase() || '';
  const name = product.productName?.toLowerCase() || '';
  const term = searchTerm.toLowerCase();

  return (product.productCode !== null && product.productCode !== undefined) &&
         (code.includes(term) || name.includes(term));
});

  return (
    <div>
      <div className='order-container'>
        <div className='search-wrapper'>
          <div className="search-container">
            <input type="text"
             placeholder=" Tìm kiếm..."
             value={searchTerm}
             onChange={(e)=> setSearchTerm(e.target.value)}/>
            <button className='search-btn' >Tìm kiếm</button>
          </div>
            <button className='add-order' onClick={()=>handelAddAndEditOrder("add")}> 
              Thêm đơn hàng</button>
            {/* <button className='add-book' onClick={()=>handelAddAndEditBook("add")}> 
              Thêm sách</button> */}
                        
        </div>

        <div className='order-nav'>
          <ul className='order-nav-list'>
            <li><a href="/dashboard" onClick={() => handleDashboardClick }>Trang chủ</a></li>
            <li><a href="/Infomation">Thông tin cá nhân</a></li>
            <li><a href="/Hr">Nhân viên</a></li>
            <li><a href="/Suplier">Kho hàng</a></li>
            <li><a href="/Products">Sản phẩm</a></li>
          </ul>
        </div>

        <div className="order-list">
          {filterProducts.map((product) => (
          <div key={product.productCode} className='order-item' onClick={()=>handleDeltailProduct(product.productCode)}>
             <h3>{product.productName}</h3>
            <img src="{product.ProductImageUrl}" alt={`ảnh sách ${product.productName}`} />
            <br />
            <span>{product.ProductYear}</span>
            <br />
            <span> {product.Price} &#8363; </span>
            <br />
            <span>Tồn kho: {product.StockQuantity}</span>
          </div>))}
        </div>
      </div>
    </div>
  );
} export default Order