export interface ApiResponse<T> {
    status: number;
    message: T;
}