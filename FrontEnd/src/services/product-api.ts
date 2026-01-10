import axios from "axios";
import type { Product } from "../types/product";
import type { ApiResponse } from "../types/api-response";

export const API_URL = "https://localhost:44315/api/Product";

export const fetchProducts = async () =>
    axios.get<ApiResponse<Product[]>>(`${API_URL}/get-product`);

export const fetchProductByCode = async (productCode: string) =>
    axios.get<ApiResponse<Product>>(`${API_URL}/get-product-by-id/${productCode}`);

export const generateProductCode =async () => 
    axios.get<{status: number; message: string}>(`${API_URL}/get-product-code`);

export const createProduct = async (formData: FormData) =>
    axios.post<ApiResponse<Product>>(
        `${API_URL}/add-product`,
        formData
    );


export const updateProduct = async (productCode: string, formData: FormData) =>
    axios.put<ApiResponse<Product>>(
        `${API_URL}/update-product/${productCode}`,
        formData
    );

export const deleteProduct = async (productCode: string) =>
    axios.delete<ApiResponse<null>>(`${API_URL}/delete-product/${productCode}`);
