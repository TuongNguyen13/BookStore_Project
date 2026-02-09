import { ApiResponse } from "../../types/api-response";
import type { Product } from "../../types/product"

interface ProductDetailProps{
    product: Product;
    onClose: () => void;
}

function ProductDetail({ product, onClose }: ProductDetailProps){
return(
<div className="modal-overplay">
    <div className="modal">
        <h3>Chi tiết sản phẩm</h3>
        <img src={`${ApiResponse}${product.productImageUrl}`} alt={product.productName} />
        <p><b>Mã sản phẩm:</b>{product.productCode}</p>
        <p><b>Tên sản phẩm:</b>{product.productName}</p>
        <p><b>Loại sản phẩm:</b>{product.productType}</p>
        <p><b>Đơn giá:</b>{product.price.toLocaleString()} VNĐ</p>
        <p><b>Năm sản xuất:</b>{product.productYear}</p>
        <p><b>Tồn kho:</b>{product.stockQuantity}</p>

        <button onClick={onClose} className="btn-cancel">Cancel</button>
    </div>
</div>
);


} export default ProductDetail