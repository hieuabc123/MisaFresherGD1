let baseAPI = ""


//1. Nếu là môi trường triển khai sản phẩm
//2. Nếu là môi trường phát triển thì API url theo môi trường phát triển
if (process.env.NODE_ENV === "production") {
    baseAPI = "http://localhost:60651/api/v1"
} else {
    baseAPI = "http://localhost:60651/api/v1"
}
export const baseUrl = baseAPI;