import { useEffect } from "react"

function AddBook() {
    useEffect(() => {
        document.title = "Thêm Thông Tin Sách"
    }, [])
    
    return (
        <div>AddBook</div>
    )
}

export default AddBook