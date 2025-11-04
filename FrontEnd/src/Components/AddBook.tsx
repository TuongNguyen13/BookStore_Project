import { useEffect, useState, type ChangeEvent } from "react"
import '../styles/add_book.css'


interface Book {
  productCode: string;
  productName: string;
  productImageUrl: string;
  productYear: number | string;
  price: number | string;
}

function AddBook() {

   const [book, setBook] = useState<Book>({
    productCode: '',
    productName: '',
    productImageUrl: '',
    productYear: 0,
    price: 0
    });

    const [status, setStatus] = useState(0);
    const [message, setMessage] = useState('');

    const handleChange = (e: ChangeEvent<HTMLFormElement>) => {
        const { name, value } = e.target;
        setBook({ ...book, [name]: value });
    }

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const token = localStorage.getItem('token');
            if (!token) {
                window.location.href = '/login'; // Chuyển hướng nếu không có token
                return;
            }
            const response = await fetch('https://localhost:44315/api/Books', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`,
                },
                body: JSON.stringify(book),
            });
            if (response.ok) {
                const data = await response.json();
                setStatus(1);
                setMessage('Thêm sách thành công!');
                setBook({ 
                    productCode: '',
                    productName: '',
                    productImageUrl: '',
                    productYear: 0,
                    price: 0
                }); // Reset form
            } else {
                const errorData = await response.json();
                setStatus(0);
                setMessage(`Lỗi: ${errorData.message || 'Không thể thêm sách'}`);
            }   
        } catch (error) {
            console.error('Error adding book:', error);
            setMessage('Đã xảy ra lỗi khi thêm sách');
        }
    }

    useEffect(() => {
        document.title = "Thêm Thông Tin Sách"
        // const fetchData = async () => {
        //     const token = localStorage.getItem('token');
        //     if (!token) {
        //         window.location.href = '/login'; // Chuyển hướng nếu không có token
        //         return;
        //     }
        //     try {
        //         const response = await fetch('https://localhost:44315/api/ValidateToken', {
        //             method: 'GET',
        //             headers: {
        //                 'Authorization': `Bearer ${token}`,
        //             },
        //         });
        //         if (response.status === 401) {
        //             window.location.href = '/login'; // Chuyển hướng nếu token không hợp lệ
        //         }
        //     } catch (error) {
        //         console.error('Error validating token:', error);
        //         window.location.href = '/login'; // Chuyển hướng nếu có lỗi
        //     }
        // };
        // fetchData();
    }, [])
    
   return (
    <div className="add-book-container">
      <form className="form-container" method="POST">
        <h2>Thêm Thông Tin Sách</h2>

        <div className="form-group">
          <label htmlFor="productCode">Mã sách:</label>
          <input type="text" id="productCode" name="productCode" required />
        </div>

        <div className="form-group">
          <label htmlFor="productName">Tên sách:</label>
          <input type="text" id="productName" name="productName" required />
        </div>

        <div className="form-group">
          <label htmlFor="productImageUrl">URL ảnh sách:</label>
          <input type="text" id="productImageUrl" name="productImageUrl" required />
        </div>

        <div className="form-group">
          <label htmlFor="productYear">Năm xuất bản:</label>
          <input type="number" id="productYear" name="productYear" required />
        </div>

        <div className="form-group">
          <label htmlFor="price">Giá:</label>
          <input type="number" id="price" name="price" required />
        </div>

        <button type="submit">Thêm sách</button>
      </form>
    </div>
  );
}
export default AddBook