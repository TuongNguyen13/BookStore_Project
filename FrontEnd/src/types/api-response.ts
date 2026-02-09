export interface ApiResponse<T> {
    status: number;
    message: T;
}

export const ApiResponse = "https://localhost:44315/";