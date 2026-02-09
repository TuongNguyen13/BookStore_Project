import type { Product } from "../../types/product";
import "../../styles/product.css"
import {API_URL} from "../../services/product-api"
import { ApiResponse } from "../../types/api-response";

interface ProductItemProps {
    product: Product;
    onView: (product: Product) => void;
    onEdit: (product: Product) => void;
    onDelete: (productCode: string) => void;

}

function ProductItem({product,onEdit,onView, onDelete}: ProductItemProps) {

    return(
<div>
    <div key={product.productCode} className="product-item" onClick={()=> onView(product)}>
        <img src={`${ApiResponse}${product.productImageUrl}`} alt={product.productName} className="product-image"/>
        <h3>{product.productName}</h3>
        <p>Giá: {product.price.toLocaleString()} VND</p>
        <p>Số lượng tồn: {product.stockQuantity}</p>
        <div className="btn-group">
        <button className="btn-edit" onClick={(e)=>{e.stopPropagation(); onEdit(product);}}>Sửa</button>
        <button onClick={(e) =>{e.stopPropagation(); onDelete (product.productCode);}}>Xóa</button>
        </div>
       
    </div>
</div>
    );

}
export default ProductItem;