import React from "react";
import { useState, useEffect } from "react";
import HeaderProduct from "../Components/product-component/HeaderProduct";
import ProductList from "../Components/product-component/ProductList"; 
import type { Product } from "../types/product";
import ProductForm from "../Components/product-component/ProductForm";
import ProductDetail from "../Components/product-component/ProductDetail";

import { fetchProducts,createProduct, updateProduct, deleteProduct, generateProductCode } from "../services/product-api";
import "../styles/product.css";


function ProductPage() {

const [product, setProduct] = useState<Product[]>([]);

const [loading, setLoading] = useState<boolean>(false);

const [showFormModal, setShowFormModal] = useState (false);

const [showDetailModal, setShowDetailModal] = useState (false);

const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);

const [modal, setModal] = useState <"add" | "edit"> ("add"); 

 const [searchValue, setSearchValue] = useState("");


const fetchProductsAsync = async () => {
    setLoading (true);
    try {
        const res = await fetchProducts();
        setProduct(res.data.message);
        console.log(res.data.message);
    } catch (error) {
        console.error("Error fetching products:", error);
    }
    setLoading(false);
};

const handleCreate = async () => {
  setModal("add");
  setSelectedProduct(null);

  const res = await generateProductCode();

  setSelectedProduct({
        productCode: res.data.message,
        productName: "",
        productType: "",
        price: 0,
        productYear: 0,
        stockQuantity: 0,
        productImageUrl: ""
  });
   
  setShowFormModal(true);
}

 const handleSearch = () => {
        console.log("Search keyword:", searchValue);
        // 👉 Gọi API search tại đây
    };

const handleEdit = (product: Product) => {
    setModal("edit");
    setSelectedProduct(product);
    setShowFormModal(true);
}

const handleViewClick = (product: Product) => {
    setSelectedProduct(product);
    setShowDetailModal(true);
} 


const handleSubmitForm = async (formData: FormData) => {
    if(modal === "add")
        await createProduct(formData);
    else{
           if (!selectedProduct) return;
        await updateProduct(selectedProduct.productCode, formData);
    }
    setShowFormModal(false);
    fetchProducts();
}

const handleDelete = async (productCode: string) => {
    try {
        if (!window.confirm("Are you sure you want to delete this product?")) 
            return; 
        await deleteProduct(productCode);
        fetchProductsAsync();
    } catch (error) {
        console.error("Error deleting product:", error);
    }
}
useEffect(() => {
    fetchProductsAsync();
}, []);


    return (
    <div  className = "product-container">
      <HeaderProduct
                searchValue={searchValue}
                onSearchChange={setSearchValue}
                onSearch={handleSearch}
                onCreate={handleCreate}
                userName="Nguyễn Văn A"
                employeeCode="EMP001"
            />
        {
            loading ? (
                <p>Đang tải...</p>
            ): (
                <ProductList products={product} 
                onView={handleViewClick}
                 onEdit={handleEdit}
                  onDelete={handleDelete}
                />

            )
        }

        
    {showFormModal && (
      <ProductForm
        mode={modal}
        product={selectedProduct}
        onSubmit={handleSubmitForm}
        onCancel={() => setShowFormModal(false)}
      />
    )}


    {showDetailModal && selectedProduct && (
      <ProductDetail
        product={selectedProduct}
        onClose={() => setShowDetailModal(false)}
      />
    )}
    </div>
    );
}
export default ProductPage;