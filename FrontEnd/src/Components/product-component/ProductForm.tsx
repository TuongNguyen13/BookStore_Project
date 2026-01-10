import { useState, useEffect } from "react";
import type { Product } from "../../types/product";
import "../../styles/product.css"

interface ProductFormProps {
    mode: 'add' | 'edit';
    product: Product | null;
    onSubmit: (formData: FormData) => void;
    onCancel: () => void;
}

const EmptyProduct: Product = {
    productCode: "",
    productName: "",
    productType: "",
    price: 0,
    productYear: new Date().getFullYear(),
    stockQuantity: 0,
    productImageUrl: ""
};

function ProductForm({ mode, product, onSubmit, onCancel }: ProductFormProps) {
    const [form, setForm] = useState<Product>(product || EmptyProduct);
    const [imageFile, setImageFile] = useState<File | null>(null);
    const [preview, setPreview] = useState<string>('');


    useEffect(() => {
        if (product) {
            setForm(product);
           setPreview(`https://localhost:44315${product.productImageUrl}`);
        } else {
            setForm(EmptyProduct);
            setPreview("");
        }
    }, [product]);


    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setForm(prevForm => ({
            ...prevForm,
            [name]: name === 'price' || name === 'ProductYear' || name === 'stockQuantity' ?
            Number(value) : value}));
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        const formData = new FormData();
        formData.append('productCode', form.productCode);
        formData.append('productName', form.productName);
        formData.append('productType', form.productType);
        formData.append('price', form.price.toString());
        formData.append('ProductYear', form.productYear.toString());
        formData.append('stockQuantity', form.stockQuantity.toString());
        if (imageFile) {
            formData.append('productImageUrl', imageFile);
        }

        onSubmit(formData);
    };
    return (
        <div className="modal-overplay">
            <div className="modal">
                <h3>{mode === 'add' ? "Thêm sản phẩm" : "Cập nhật sản phẩm"}</h3>
                <form onSubmit={handleSubmit} className="product-form">
                    <label htmlFor="productCode">Mã sản phẩm:</label>
                    <input type="text"
                        name="productCode"
                        value={form.productCode || ""}
                        onChange={handleChange}
                        readOnly />


                    <label htmlFor="productName">Tên sản phẩm:</label>

                    <input type="text"
                        name="productName"
                        value={form.productName}
                        onChange={handleChange} />


                    <label htmlFor="productType">Loại sản phẩm:</label>

                    <input type="text"
                        name="productType"
                        value={form.productType}
                        onChange={handleChange} />

                    <label htmlFor="price">Giá:</label>

                    <input type="number"
                        name="price"
                        value={form.price}
                        onChange={handleChange} />

                    <label htmlFor="productYear">Năm sản xuất:</label>

                    <input type="number"
                        name="productYear"
                        value={form.productYear}
                        onChange={handleChange} />

                    <label htmlFor="stockQuantity">Số lượng sản phẩm:</label>

                    <input type="number"
                        name="stockQuantity"
                        value={form.stockQuantity}
                        onChange={handleChange} />

                    <label htmlFor="productImageUrl">Hình ảnh sản phẩm</label>
                    <input type="file"
                        accept="image/*"
                        name="productImageUrl"
                        onChange={(e) => {
                            if (e.target.files && e.target.files[0]) {
                                setImageFile(e.target.files[0]);
                                setPreview(URL.createObjectURL(e.target.files[0]));
                            }
                        }}
                    />
                    {preview && (
                        <img src={preview} alt="xem ảnh trước"
                            style={{ width: "100%", height: 150, objectFit: "cover" }} />
                    )}
                    <button type="submit" className="btn-submit">{mode === "add" ? "Thêm " : "Cập nhật"}</button>
                    <button className="btn-cancel" onClick={onCancel}>Cancel</button>
                </form>
            </div>
        </div>
    );
}

export default ProductForm