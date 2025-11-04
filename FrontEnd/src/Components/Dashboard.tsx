import React,{useEffect} from "react";
import "../styles/dashboard.css";

function Dashboard() {
    useEffect(() => {
        document.title = "Trang chủ";
    }, []);

    const handlePageOnClick = (pageName : string) => {
            switch (pageName)
            {
                case "order":
                    window.location.href = "/order";
                    break;

                case "product":
                    window.location.href = "/product";
                    break;

                case "employee":
                    window.location.href = "/employee";
                    break;

                case "suplier":
                    window.location.href = "/suplier";
                    break;

                default:
                window.location.href = "/dashboard";
            }
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
                    <li><a href="/dashboard" onClick={() => handlePageOnClick("dashboard")}>Trang chủ</a></li>
                    <li><a href="/order" onClick={()=>handlePageOnClick("order")}>Đơn đặt hàng</a></li>
                    <li><a href="/product" onClick={() => handlePageOnClick("product")}>Sản phẩm</a></li>
                    <li><a href="/employee" onClick={() => handlePageOnClick("employee")}>Nhân viên</a></li>
                    <li><a href="/suplier" onClick={() => handlePageOnClick("suplier")}>Nhà cung cấp</a></li>
                </ul>
            </div>
            <div>

            </div>

        </div>
    );
}
export default Dashboard;