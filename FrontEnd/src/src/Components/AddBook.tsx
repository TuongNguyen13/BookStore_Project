import { useEffect } from "react"

function AddBook() {
    useEffect(() => {
        document.title = "Thêm Thông Tin Sách"
    }, [])
    
    return (
        <div>
            <h2>Thêm Thông Tin Sách</h2>
            <div className="add-book-container">
                <form action="https://localhost:44315/api/AddBook" method="POST">
                    <label htmlFor="productCode">Mã Sách:</label>
                    <input type="text" id="productCode" name="productCode" required />
                    <br />
                    <label htmlFor="productName">Tên Sách:</label>
                    <input type="text" id="productName" name="productName" required />
                    <br />
                    <label htmlFor="productImageUrl">URL Ảnh Sách:</label>
                    <input type="text" id="productImageUrl" name="productImageUrl" required />
                    <br />
                    <label htmlFor="productYear">Năm Xuất Bản:</label>
                    <input type="number" id="productYear" name="productYear" required />
                    <br />
                    <label htmlFor="price">Giá:</label>
                    <input type="number" id="price" name="price" required />
                    <br />
                    <button type="submit">Thêm sách</button>
                </form>
        </div>
        </div>
    )
}

export default AddBook