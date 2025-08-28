import React from 'react';
import '../styles/login.css'
function Login() {
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