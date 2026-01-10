import type { Product } from "../../types/product";
import "../../styles/product.css"
import {API_URL} from "../../services/product-api"

interface ProductItemProps {
    product: Product;
    onView: (product: Product) => void;
    onEdit: (product: Product) => void;
    onDelete: (productCode: string) => void;

}

function ProductItem({product,onEdit,onView, onDelete}: ProductItemProps) {

    return(
<div>
    <div key={product.productCode} className="product-item">
        <img src={`https://localhost:44315${product.productImageUrl}`} alt={product.productName} className="product-image"/>
        <h3>{product.productName}</h3>
        <p>Type: {product.productType}</p>
        <p>Price: {product.price.toLocaleString()} VND</p>
        <p>Year: {product.productYear}</p>
        <p>Stock Quantity: {product.stockQuantity}</p>
        <button className="btn-edit" onClick={()=> onEdit(product)}>Sửa</button>
        <button onClick={() => onDelete (product.productCode)}>Xóa</button>
    </div>
</div>
    );

}
export default ProductItem;