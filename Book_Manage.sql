use master
Go
create database Book_Manage
use Book_Manage

ALTER DATABASE Book_Manage 
SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO
Drop Database Book_Manage
Go

 -- Phần khác 
 CREATE TABLE Employee (
    EmployeeCode VARCHAR(20) PRIMARY KEY,
    EmployeeName NVARCHAR(255) NOT NULL,
    Gender NVARCHAR(20),
    BirthDay DATE,
    EmployeeAddress NVARCHAR(255),
    EmployeeNumber VARCHAR(12),
    Email VARCHAR(255) UNIQUE,
    EmployeeRole nvarchar(20)
);


-- Tài khoản
    CREATE TABLE Account (
        UserCode VARCHAR(20) PRIMARY KEY,
        Username VARCHAR(50) NOT NULL UNIQUE,
        Pass VARCHAR(255) NOT NULL,
        EmployeeCode VARCHAR(20) UNIQUE NOT NULL,
        CONSTRAINT fk_Account_Employee FOREIGN KEY (EmployeeCode) 
            REFERENCES Employee(EmployeeCode)
    );

-- Nhà cung cấp
CREATE TABLE Supplier (
    SupplierCode VARCHAR(20) PRIMARY KEY NOT NULL,
    SupplierName NVARCHAR(255) NOT NULL,
    SupplierAddress NVARCHAR(255),
    SupplierPhone VARCHAR(15),
    SupplierEmail VARCHAR(255)
);

-- Sản phẩm (bao gồm sách, bút, thước, ...)
CREATE TABLE Products (
    ProductCode VARCHAR(20) PRIMARY KEY NOT NULL,
    ProductName NVARCHAR(255) NOT NULL unique,
    ProductType NVARCHAR(255) NOT NULL DEFAULT(N'Khác'),
    Price DECIMAL(18,2) NOT NULL,
    ProductYear INT,
    StockQuantity INT DEFAULT 0,
    ProductImageUrl varchar(255)
);

-- Khách hàng
CREATE TABLE Customer (
    CustomerCode VARCHAR(20) PRIMARY KEY NOT NULL,
    CustomerName NVARCHAR(255) NOT NULL,
    Gender NVARCHAR(20),
    PhoneNumber VARCHAR(15) UNIQUE,
    CustomerAddress NVARCHAR(255)
);

-- Đơn hàng
CREATE TABLE Orders (
    OrderCode VARCHAR(20) PRIMARY KEY NOT NULL, 
    CustomerCode VARCHAR(20) NOT NULL,
    EmployeeCode VARCHAR(20),
    Payment NVARCHAR (30) default (N'Tiền mặt'),
    OrderStatus VARCHAR default ('InCart'), -- Thêm vào giỏ hàng (InCart), Đã thanh toán (complited)
    OrderTotal DECIMAL(18,2),
    DateCreated DATETIME DEFAULT GETDATE(),
    CONSTRAINT fk_Order_Customer
    FOREIGN KEY (CustomerCode) REFERENCES Customer(CustomerCode),
    CONSTRAINT fk_Order_Employee 
    FOREIGN KEY ( EmployeeCode) REFERENCES Employee( EmployeeCode)
);
Alter table Orders 
Drop constraint DF__Orders__OrderSta__48CFD27E 

-- Chi tiết đơn hàng (PK kép)
CREATE TABLE OrderDetails (
    OrderCode VARCHAR(20) NOT NULL,
    ProductCode VARCHAR(20) NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice float default 0 NOT NULL,
    PRIMARY KEY (OrderCode, ProductCode),
    CONSTRAINT fk_OrderDetail_Orders 
    FOREIGN KEY (OrderCode) REFERENCES Orders(OrderCode),
    CONSTRAINT fk_OrderDetail_Products
    FOREIGN KEY (ProductCode) REFERENCES Products(ProductCode)
);

-- Phiếu nhập hàng
CREATE TABLE Receipt (
    ReceiptCode VARCHAR(20) PRIMARY KEY NOT NULL,
    EmployeeCode VARCHAR(20) NOT NULL,
    SupplierCode VARCHAR(20) NOT NULL,
    ReceiptTotal DECIMAL(18,2),
    DateCreated DATETIME DEFAULT GETDATE(),
    ReceiptNote NVARCHAR (255),
    CONSTRAINT fk_Receipt_Employee 
    FOREIGN KEY (EmployeeCode) REFERENCES Employee(EmployeeCode),
    CONSTRAINT fk_Receipt_Supplier
    FOREIGN KEY (SupplierCode) REFERENCES Supplier(SupplierCode)
);

-- Chi tiết phiếu nhập (PK kép)
CREATE TABLE ReceiptDetail (
   ReceiptCode VARCHAR(20) NOT NULL,
    ProductCode VARCHAR(20) NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice float default 0 NOT NULL,
    PRIMARY KEY (ReceiptCode, ProductCode),
    CONSTRAINT fk_ReceiptDetail_Receipt 
    FOREIGN KEY (ReceiptCode) REFERENCES Receipt(ReceiptCode),
    CONSTRAINT fk_ReceiptDetail_Product 
    FOREIGN KEY (ProductCode) REFERENCES Products(ProductCode)
);
