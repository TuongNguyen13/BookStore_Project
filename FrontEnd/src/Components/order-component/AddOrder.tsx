import React, { useEffect } from "react";
import "../../styles/add_order.css";


function AddOrder() {

    useEffect(() => {
        document.title = "Thêm đơn đặt hàng";
    }, []);


    return (
        <div className="add-order-container">
            <form className="form-container" method="POST">
            <h2>Thêm đơn đặt hàng</h2>
                <div className="add-order-form-group">
                    <label htmlFor="orderId">Mã đơn hàng:</label>
                    <input type="text"
                        id="orderId"
                        name="orderId"
                        placeholder="Nhập thông tin..."
                        required />
                </div>
                <div className="add-order-form-group">
                    <label htmlFor="customerName">Tên khách hàng:</label>
                    <input type="text" 
                    id="customerName" 
                    name="customerName" required 
                    placeholder="Nhập thông tin..." />
                </div>
                <div className="add-order-form-group">
                    <label htmlFor="orderDate">Ngày đặt hàng:</label>
                    <input type="date"
                        id="orderDate"
                        name="orderDate"
                        required />
                </div>
                <div className="add-order-form-group">
                    <label htmlFor="totalAmount">Tổng số tiền:</label>
                    <input type="number"
                        id="totalAmount"
                        name="totalAmount"
                        required />
                </div>
                <button type="submit">Thêm đơn hàng</button>
            </form>
            <div className="add-order-info-table">
                
                <table className="add-order-table">
                <caption>Thông tin đơn hàng</caption>
                    <thead>
                        
                        <tr>
                            <th>Mã đơn hàng</th>
                            <th>Tên khách hàng</th>
                            <th>Ngày đặt hàng</th>
                            <th>Tổng số tiền</th>
                        </tr>
                    </thead>
                    <tbody>
                        {/* Dữ liệu đơn đặt hàng sẽ được hiển thị ở đây */}
                    </tbody>
                </table>
            </div>
        </div>
    );
}
export default AddOrder;