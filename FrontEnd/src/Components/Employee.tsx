import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import '../styles/employee.css'



function Employee() {
    const [showModal, setShowModal] = useState(false);

    useEffect(() => {
        document.title = "Nhân viên";

    }, [])

    const handleAddEmployee = () => {
        setShowModal(true)
    }

    const handleCancelEmployee = () => {
        setShowModal(false)
    }

    return (
        <div className="employee-container">
            <div className="employee-header">
                <h2>Quản lý nhân viên</h2>
                     <div className="employee-search">
                    <input type="text" placeholder="Tìm kiếm..." />
                    <button>Tìm kiếm</button>
                <div>
                    <button onClick={() => handleAddEmployee()}>Thêm nhân viên</button>
                </div>
                </div>
                </div>
               
                <div className="employee-table-information">
                    <table>
                        <caption>Thông tin nhân viên</caption>
                        <thead>
                            <tr>
                                <th>Mã nhân viên</th>
                                <th>Tên nhân viên</th>
                                <th>Giới tính</th>
                                <th>Email</th>
                                <th>Chức năng</th>
                            </tr>
                        </thead>

                        <tbody>
                        <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                        </tr>

                        </tbody>

                    </table>
                </div>

                {/* phân trang */}
                <div>
                    <button>Trang trước</button>
                    <span></span>
                    <button>Trang sau</button>
                </div>
                {/*Thêm - sửa nhân viên */}
                <div>
                    {showModal && (
                        <div>
                            <div>
                                <h2>{ }</h2>
                                <form action="#">
                                    <div>
                                        <label htmlFor="emplyee-code">Mã nhân viên</label>
                                        <input type="text" disabled />
                                    </div>

                                    <div>
                                        <label htmlFor="employee-name">Tên nhân viên</label>
                                        <input type="text" placeholder="Nhập tên nhân viên" name="employee-name" />
                                    </div>
                                    <div>
                                        <label htmlFor="employee-gender">Giới tính</label>
                                        <div>
                                            
                                                <span>Nam </span>
                                                <input type="radio" name="employee-gender" value="male" />
    
                                                <span> Nữ </span>
                                                <input type="radio" name="employee-gender" value="female" />
                                        </div>

                                    </div>
                                    <div>
                                        <label htmlFor="employee-birthday">Ngày sinh</label>
                                        <input type="date" name="employee-birthday" placeholder="Nhập ngày sinh" />
                                    </div>
                                    <div>
                                        <label htmlFor="employee-email">Email</label>
                                        <input type="text" placeholder="Nhập email..." name="employee-email" />
                                    </div>
                                    <div>
                                        <select name="employee-grant" id="txt-employee-grant">
                                            <option value="normal-employee" defaultChecked>Nhân viên</option>
                                            <option value="managa-employee">Quản lý</option>
                                            <option value="admin-employee">Admin</option>
                                        </select>
                                    </div>
                                    <div>
                                        <label htmlFor="employee-address">Địa chỉ</label>
                                        <input type="text" placeholder="Nhập địa chỉ nhân viên..." />
                                    </div>
                                    <div>
                                        <button type="submit">Thêm nhân viên</button>
                                        <button onClick={()=>handleCancelEmployee()}>Cancel</button>
                                    </div>

                                </form>
                            </div>
                        </div>
                    )}
                </div>
            
        </div>
    );
}

export default Employee;