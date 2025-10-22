import React,{useEffect} from "react";
import "../styles/dashboard.css";

function Dashboard() {
    useEffect(() => {
        document.title = "Trang chủ";
        
    }, []);

    const handleOrderClick = () => {
        window.location.href = "/order";
    }

    const handleDashboardClick = () => {
        window.location.href = "/dashboard";
    }
    return (
        <div className="dashboard-container">
            <div className="dashboard-header">
                  <img src="#" alt="Logo nhà sách" />
                <input type="text" id="txtSearchDasboard" placeholder="Tìm kiếm..."/>
                <div>
                    <a href="/profile" className="employee-profile"><i></i>Nguyễn Văn A</a>
                </div>

            </div>
            <div className="dashboard-navbar">
              
                <ul className="dashboard-nav-list">
                    <li><a href="/dashboard" onClick={() => handleDashboardClick}>Trang chủ</a></li>
                    <li><a href="/order" onClick={()=>handleOrderClick}>Đơn đặt hàng</a></li>
                    <li><a href="/product">Sản phẩm</a></li>
                    <li><a href="/employee">Nhân viên</a></li>
                    <li><a href="/suplior">Nhà cung cấp</a></li>
                </ul>
            </div>
            <div>

            </div>

        </div>
    );
}
export default Dashboard;