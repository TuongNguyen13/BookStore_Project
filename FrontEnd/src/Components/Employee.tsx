import React, { useState, useEffect, type ChangeEvent, type FormEvent } from "react";
import '../styles/employee.css';

// ================= Types =================
export interface Employee {
    employeeCode: string;
    employeeName: string;
    gender: string;
    birthday: string;
    employeeNumber: string;
    email: string;
    employeeAddress: string;
    employeeRole: string;
}

const BASE_URL = "https://localhost:44315/api/Employees";

function EmployeePage() {
    const [employees, setEmployees] = useState<Employee[]>([]);
    const [showModal, setShowModal] = useState(false);
    const [editingCode, setEditingCode] = useState<string | null>(null);

    const [errors, setErrors] = useState({ 
        email: "",
         employeeNumber: ""
         });

    const [form, setForm] = useState<Employee>({
        employeeCode: "",
        employeeName: "",
        gender: "Nam",
        birthday: "",
        employeeNumber: "",
        email: "",
        employeeAddress: "",
        employeeRole: "normal-employee",
    });

    useEffect(() => {
        document.title = "Nhân viên";
        fetchEmployees();
        fetchNewEmployeeCode();
    }, []);

    const authHeader = () => ({
        "Content-Type": "application/json",
        "Authorization": `Bearer ${localStorage.getItem("token")}`,
    });

    // ================= API =================
    const fetchEmployees = async () => {
        const res = await fetch(`${BASE_URL}/get-employee`, {
            headers: authHeader(),
        });
        const data = await res.json();
        setEmployees(data.messages);
    };

    const fetchNewEmployeeCode = async () => {
        const res = await fetch(`${BASE_URL}/get-new-employee-code`, {
            headers: authHeader(),
        });
        const data = await res.json();
        setForm((prev) => ({ ...prev, employeeCode: data.messages }));
    }

    const createEmployee = async () => {
        await fetch(`${BASE_URL}/add-employee`, {
            method: "POST",
            headers: authHeader(),
            body: JSON.stringify(form),
        });
    };

    const updateEmployee = async (code: string) => {
        await fetch(`${BASE_URL}/edit-employee/${code}`, {
            method: "PUT",
            headers: authHeader(),
            body: JSON.stringify(form),
        });
    };

    const deleteEmployee = async (code: string) => {
        if (!window.confirm("Bạn có chắc muốn xóa nhân viên này?")) return;
        await fetch(`${BASE_URL}/delete-employee/${code}`, {
            method: "DELETE",
            headers: authHeader(),
        });
        fetchEmployees();
    };

    // ================= Handlers =================
    const handleChange = (e: ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setForm({ ...form, [name]: value });

        if (name === "email") {
            setErrors({
                ...errors,
                email: inValidEmail(value) ? "" : "Email không hợp lệ!"
            });
        }
        if (name === "employeeNumber") {
            setErrors({
                ...errors,
                employeeNumber: inValidPhoneNumber(value) ? "" : "Số điện thoại không hợp lệ!"
            });
        }
    };

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        if (errors.email || errors.employeeNumber) {
            alert("Vui lòng nhập đúng thông tin");
            return;
        }

        if (editingCode) {
            await updateEmployee(editingCode);
        } else {
            await createEmployee();
        }

        setShowModal(false);
        setEditingCode(null);
        resetForm();
        fetchEmployees();
    };

    const formatDateForInput = (date: string) => {
        if (!date) return "";
        return date.split("T")[0];
    };

    const inValidEmail = (email: string) => {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    };

    const inValidPhoneNumber = (phone: string) => {
        const phoneRegex = /^0\d{9}$/;
        return phoneRegex.test(phone);
    };
    const handleEdit = (emp: Employee) => {
        setEditingCode(emp.employeeCode);
        setForm({
            ...emp,
            birthday: formatDateForInput(emp.birthday),
        });
        setShowModal(true);
    };

    const resetForm = () => {
        setForm({
            employeeCode: "",
            employeeName: "",
            gender: "Nam",
            birthday: "",
            email: "",
            employeeNumber: "",
            employeeAddress: "",
            employeeRole: "normal-employee",
        });
    };

    return (
        <div className="employee-container">
            <div className="employee-header">
                <h2>Quản lý nhân viên</h2>
                <div className="employee-search">
                    <input type="text" placeholder="Tìm kiếm..." />
                    <button>Tìm kiếm</button>
                    <div>
                        <button onClick={() => setShowModal(true)}>Thêm nhân viên</button>
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
                            <th>Số điện thoại</th>
                            <th>Địa chỉ</th>
                            <th>Vai trò</th>
                            <th>Chức năng</th>
                        </tr>
                    </thead>

                    <tbody>
                        {employees.map((emp) => (
                            <tr key={emp.employeeCode} >
                                <td>{emp.employeeCode}</td>
                                <td>{emp.employeeName}</td>
                                <td>{emp.gender}</td>
                                <td>{emp.employeeNumber}</td>
                                <td>{emp.employeeAddress}</td>
                                <td>{emp.employeeRole === "normal-employee" ? "Nhân viên" : emp.employeeRole === "managa-employee" ? "Quản Lý" : "Admin"}</td>
                                <td>
                                    <button onClick={() => handleEdit(emp)}>Sửa</button>
                                    <button onClick={() => void deleteEmployee(emp.employeeCode)}>Xóa</button>
                                </td>
                            </tr>
                        ))}
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
                            <form onSubmit={handleSubmit}>
                                <h2 className="add-employee-header">{editingCode ? "Sửa nhân viên" : "Thêm nhân viên"}</h2>

                                <div>
                                    <label htmlFor="employeeCode" >Mã nhân viên  </label>
                                    <input type="text" value={form.employeeCode} onChange={handleChange} disabled />
                                </div>

                                <div>
                                    <label htmlFor="employeeName">Tên nhân viên</label>
                                    <input type="text" placeholder="Nhập tên nhân viên" name="employeeName" value={form.employeeName} onChange={handleChange} />
                                </div>
                                <div>
                                    <label htmlFor="gender">Giới tính</label>
                                    <div>
                                        <input type="radio" name="gender" value="Nam" checked={form.gender === "Nam"} onChange={handleChange} /> Nam


                                        <input type="radio" name="gender" value="Nữ" checked={form.gender === "Nữ"} onChange={handleChange} /> Nữ
                                    </div>

                                </div>
                                <div>
                                    <label htmlFor="birthday">Ngày sinh</label>
                                    <input type="date" name="birthday" placeholder="Nhập ngày sinh" value={form.birthday} onChange={handleChange} />
                                </div>

                                <div>
                                    <label htmlFor="email">Email</label>
                                    <input type="text" placeholder="Nhập email..." name="email" value={form.email} onChange={handleChange} />
                                </div>
                                {errors.email && (
                                    <span style={{ color: "red", fontSize: "13px" }}>
                                        {errors.email}
                                    </span>
                                )}
                                <div>
                                    <label htmlFor="employeeNumber">Số điện thoại</label>
                                    <input type="text" placeholder="Nhập số điện thoại..." name="employeeNumber" value={form.employeeNumber} onChange={handleChange} />
                                </div>

                                {errors.employeeNumber && (
                                    <span style={{ color: "red", fontSize: "13px" }}>
                                        {errors.employeeNumber}
                                    </span>
                                )}
                                <div>
                                    <label htmlFor="employeeAddress">Địa chỉ</label>
                                    <input type="text" placeholder="Nhập địa chỉ nhân viên..." name="employeeAddress" value={form.employeeAddress} onChange={handleChange} />
                                </div>

                                <div>
                                    <label htmlFor="employeeRole">Chức vụ</label>
                                    <select name="employeeRole" id="txt-employee-role" value={form.employeeRole} onChange={handleChange} >
                                        <option value="normal-employee" defaultChecked>Nhân viên</option>
                                        <option value="managa-employee">Quản lý</option>
                                        <option value="admin-employee">Admin</option>
                                    </select>
                                </div>

                                <div>
                                    <button type="submit">{editingCode ? "Cập nhật" : "Thêm"}</button>
                                    <button onClick={() => setShowModal(false)}>Cancel</button>
                                </div>

                            </form>
                        </div>
                    </div>
                )}
            </div>

        </div>
    );
}

export default EmployeePage;
