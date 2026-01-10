import {useEffect, useState} from "react";
import ProductItem from "./ProductItem";
import ProductForm from "../product-component/ProductForm"
import ProductDetail from "./ProductDetail";
import "../../styles/product.css"

export interface Product {
    productCode : string;
    productName : string;
    productType : string;
    price : number;
    productYear : number;
    stockQuantity : number;
    productImageUrl : string;
}

interface ProductListProps {
    products: Product[];
    onDelete: (productCode: string) => void;
    onEdit: (product: Product) => void;
    onView: (product: Product) => void;
}





function ProductList({ products, onDelete, onEdit, onView }: ProductListProps) {
    return <div className="product-list">
        {products.map((product) => (
            <ProductItem key={product.productCode} 
                product={product}
             onDelete={onDelete}
             onEdit={onEdit}
             onView = {onView}
              />
        ))}
    </div>;
}
export default ProductList;