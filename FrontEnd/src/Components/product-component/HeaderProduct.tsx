import React from "react";
import "../../styles/product.css"


interface HeaderProductProps{
    onCreate :() => void;
}

function HeaderProduct({ onCreate}: HeaderProductProps) {
    return (
         <div className="product-header">
            <h2>Quản lý sản phẩm</h2>
            <button className="btn-add" onClick={() =>onCreate()}>Thêm sản phẩm</button>
        </div>
    );
}
export default HeaderProduct;