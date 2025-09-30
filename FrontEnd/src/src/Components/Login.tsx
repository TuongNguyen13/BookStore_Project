import React, { useState } from 'react';
import '../styles/login.css';
import { useNavigate } from 'react-router-dom';

interface LoginResponse {
  status: number;
  message: string;
  token: string;
}

function Login() {
  const navigate = useNavigate();
  const [username, setUsername] = useState('');
  const [pass, setPassword] = useState('');
  const [employeeId, setEmployeeId] = useState(0);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      const response = await fetch('https://localhost:44315/api/Login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ username, pass,employeeId }), // ✅ Gửi đúng dữ liệu
      });

      if (!response.ok) {
        throw new Error('Đăng nhập thất bại');
      }

      const data: LoginResponse = await response.json();

      if (data.status === 1 && data.token) {
        console.log('Đăng nhập thành công:', data);
        localStorage.setItem('token', data.token); // ✅ Lưu token
        navigate('/order'); // ✅ Chuyển hướng
      } else {
        setError(data.message || 'Sai tài khoản hoặc mật khẩu');
      }
    } catch (err) {
      alert(setError((err as Error).message));
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className='login-background'>
      <div className='login-container'>
        <form method='POST'>
          <div className='header-login'>
            <h2>Đăng nhập</h2>
          </div>
          <div className='login-input'>
            <div className='input-username'>
              <label htmlFor="userName">Tên đăng nhập: </label>
              <input
                type="text"
                id="userName"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
              />
            </div>
            <div className='input-password'>
              <label htmlFor="password">Mật khẩu: </label>
              <input
                type="password"
                id="password"
                value={pass}
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>
          </div>
           { error && <p style={{ color: 'red' }}>{error}</p>}
          <div className='login-button'>
            <button type="submit" disabled={loading} onClick={handleSubmit}>
              {loading ? 'Đang đăng nhập...' : 'Đăng nhập'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default Login;
