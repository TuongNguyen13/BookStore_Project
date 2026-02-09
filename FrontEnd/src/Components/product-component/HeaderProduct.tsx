import React from "react";
import "../../styles/product.css";

interface HeaderProductProps {
    searchValue: string;
    onSearchChange: (value: string) => void;
    onSearch: () => void;
    onCreate: () => void;
    userName: string;
    employeeCode: string;
}

function HeaderProduct({
    searchValue,
    onSearchChange,
    onSearch,
    onCreate,
    userName,
    employeeCode
}: HeaderProductProps) {
    return (
        <div className="product-header">
            <h2>Quản lý sản phẩm</h2>

            {/* Search group */}
            <div className="search-group">
                <input
                    className="inp-search"
                    type="text"
                    placeholder="Tìm kiếm..."
                    value={searchValue}
                    onChange={(e) => onSearchChange(e.target.value)}
                    onKeyDown={(e) => e.key === "Enter" && onSearch()}
                />
                <button
                    className="btn-search-product"
                    onClick={onSearch}
                >
                    Tìm kiếm
                </button>
            </div>

            {/* User + add */}
            <div className="person-event-group">
                <a
                    href={`/profile/${employeeCode}`}
                    className="user-link"
                >
                    <i className="icon-user"></i>
                    {userName}
                </a>

                <button
                    className="btn-add"
                    onClick={onCreate}
                >
                    Thêm sản phẩm
                </button>
            </div>
        </div>
    );
}

export default HeaderProduct;
