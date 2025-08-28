import React from 'react';
import '../styles/login.css'
import { useNavigate } from 'react-router-dom';
interface LoginRespose
{
  token: string;
  userId: string;
  username: string;
}

function Login() {
  const navigator = useNavigate();
 const [username, setUsername] = React.useState<string>('');
  const [password, setPassword] = React.useState<string>('');
  const [error, setError] = React.useState<string>('');
  const [loading, setLoading] = React.useState<boolean>(false);
  
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError('');
    try {
      const response = await fetch('http://localhost:8080/api/auth/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ username, password }),
      });
      if (!response.ok) {
        throw new Error('Login failed');
      }
      const data: LoginRespose = await response.json();
      console.log('Login successful:', data);
      localStorage.setItem('token', data.token);
      navigator('/dashboard');
      // Handle successful login (e.g., store token, redirect)
    } catch (err) {
      setError((err as Error).message);
    } finally {
      setLoading(false);
    }
  }

  return <div className='login-container'>
    <form action="POST">
      <div className=' header-login'>
        <h2>Đăng nhập</h2>
      </div>
      <div className='login-input'>
        <div className='input-username'>
          <label htmlFor="userName">Tên đăng nhập: </label>
          <input type="text" id="userName" name="userName" />
        </div>
        <div className='input-password'>
          <label htmlFor="password">Mật khẩu: </label>
          <input type="password" id="password" name="password" />
        </div>
      </div>
      <div className='login-button'>
        <button type="submit">Đăng nhập</button>
      </div>
    </form>
  </div>;
} export default Login;