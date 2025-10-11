use master
Go
create database Book_Manage
use Book_Manage

ALTER DATABASE Book_Manage SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO
Drop Database Book_Manage
Go

 --create table Employee
 --(
	--ID int primary key,
	--EmployeeId varchar(20) unique,
	--EmployeeName nvarchar(255),
	--Gender nvarchar(20),
	--BirthDay DateTime,
	--EmployeeAddress nvarchar(255),
	--Email varchar(255)
 --)

 -- create table Account
 --(
	--ID int primary key,
	--Username varchar(20) not null,
	--Pass varchar(255),
	--EmployeeId varchar(20)
 --)

 --Alter table Account
 --add constraint fk_Account_Employee foreign key (EmployeeId)
 --references Employee(EmployeeId)

 --create table Products
 --(
	--ID int primary key,
	--ProductId varchar(20) unique,
	--ProductName nvarchar(255),
	--Cost float,
	--ProductYear Datetime
 --)

 -- create table Customer
 --(
	--ID int primary key,
	--CustomerId varchar(20) unique,
	--CustomerName nvarchar(255),
	--Gender nvarchar(20),
	--PhoneNumber varchar(11),
	--CustomerAddress nvarchar (255)
 --)

 --create table Orders
 --(
	--ID int primary key,
	--OrderId varchar(20) unique,
	--CustomerId varchar(20),
	--EmployeeId varchar(20),
	--DateCreated date
 --)

 --alter table Orders
 --add
 --constraint fk_Order_Customer foreign key (CustomerId) 
 --references Customer(CustomerId),
 --constraint fk_Order_EmployeeId foreign key (EmployeeId)
 --references Employee(EmployeeId)

 ----create table Producter
 ----(
	----ID int primary key,
	----ProductId varchar(20) not null,
	----ProductName nvarchar(255) not null
 ----)

 --create table OrderDetails
 --(
	--ID int primary key,
	--OrderId varchar(20),
	--ProductId varchar(20),
	--Quantity float,
 --)

 --Alter table OrderDetails
 --Add constraint fk_OrderDetail_Orders foreign key (OrderId) 
 --References Orders(OrderId),
 --constraint fk_OrderDetail_ProductId foreign key (ProductId) 
 --references Products(ProductId)

 --create table Receipt
 --(
	--ID int primary key,
	--ReceiptId varchar(20) unique,
	--EmployeeId varchar(20),
	--SupplierId varchar(20),
	--DateCreated Date
 --)

 -- create table Supplier
 --(
	--ID int primary key,
	--SupplierId varchar(20) unique,
	--SupplierName nvarchar(255),
	--SupplierAddress nvarchar(255),
	--SupplierPhoneNumber varchar(11)
 --)

 --alter table Receipt
 --add
 --constraint fk_receipt_Supplier foreign key (SupplierId) 
 --references Supplier(SupplierId),
 --constraint fk_Receipt_Employee foreign key (EmployeeId) 
 --references Employee(EmployeeId)


 --create table ReceiptDetail
 --(
	--id int primary key,
	--ReceiptId varchar(20),
	--ProductId varchar(20),
	--Quatity float,
 --)

 --alter table ReceiptDetail
 --add 
 --constraint fk_ReceiptDetail_Receipt foreign key (ReceiptId)
 --references Receipt(ReceiptId),
 --constraint fk_ReceiptDetail_Products foreign key (ProductId)
 --references Products(ProductId)
 

 -- Phần khác 
 CREATE TABLE Employee (
    ID INT IDENTITY PRIMARY KEY,
    EmployeeCode VARCHAR(20) UNIQUE NOT NULL,
    EmployeeName NVARCHAR(255) NOT NULL,
    Gender NVARCHAR(20),
    BirthDay DATE,
    EmployeeAddress NVARCHAR(255),
    Email VARCHAR(255) UNIQUE
);

-- Tài khoản
CREATE TABLE Account (
    ID INT IDENTITY PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Pass VARCHAR(255) NOT NULL,
    EmployeeID INT NOT NULL,
    CONSTRAINT fk_Account_Employee FOREIGN KEY (EmployeeID) 
        REFERENCES Employee(ID)
);
ALTER TABLE Account ADD CONSTRAINT UQ_Account_EmployeeID UNIQUE (EmployeeID);


-- Nhà cung cấp
CREATE TABLE Supplier (
    ID INT IDENTITY PRIMARY KEY,
    SupplierCode VARCHAR(20) UNIQUE NOT NULL,
    SupplierName NVARCHAR(255) NOT NULL,
    SupplierAddress NVARCHAR(255),
    SupplierPhone VARCHAR(15)
);

-- Sản phẩm (bao gồm sách, bút, thước, ...)
CREATE TABLE Products (
    ID INT IDENTITY PRIMARY KEY,
    ProductCode VARCHAR(20) UNIQUE NOT NULL,
    ProductName NVARCHAR(255) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    ProductYear INT,
    StockQuantity INT DEFAULT 0
);

alter table Products
add ProductImageUrl varchar(255)

-- Khách hàng
CREATE TABLE Customer (
    ID INT IDENTITY PRIMARY KEY,
    CustomerCode VARCHAR(20) UNIQUE NOT NULL,
    CustomerName NVARCHAR(255) NOT NULL,
    Gender NVARCHAR(20),
    PhoneNumber VARCHAR(15),
    CustomerAddress NVARCHAR(255)
);

-- Đơn hàng
CREATE TABLE Orders (
    ID INT IDENTITY PRIMARY KEY,
    OrderCode VARCHAR(20) UNIQUE NOT NULL,
    CustomerID INT NOT NULL,
    EmployeeID INT NOT NULL,
    DateCreated DATETIME DEFAULT GETDATE(),
    CONSTRAINT fk_Order_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(ID),
    CONSTRAINT fk_Order_Employee FOREIGN KEY (EmployeeID) REFERENCES Employee(ID)
);

-- Chi tiết đơn hàng (PK kép)
CREATE TABLE OrderDetails (
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice float default 0 NOT NULL,
    PRIMARY KEY (OrderID, ProductID),
    CONSTRAINT fk_OrderDetail_Orders FOREIGN KEY (OrderID) REFERENCES Orders(ID),
    CONSTRAINT fk_OrderDetail_Products FOREIGN KEY (ProductID) REFERENCES Products(ID)
);

-- Phiếu nhập hàng
CREATE TABLE Receipt (
    ID INT IDENTITY PRIMARY KEY,
    ReceiptCode VARCHAR(20) UNIQUE NOT NULL,
    EmployeeID INT NOT NULL,
    SupplierID INT NOT NULL,
    DateCreated DATETIME DEFAULT GETDATE(),
    CONSTRAINT fk_Receipt_Employee FOREIGN KEY (EmployeeID) REFERENCES Employee(ID),
    CONSTRAINT fk_Receipt_Supplier FOREIGN KEY (SupplierID) REFERENCES Supplier(ID)
);

-- Chi tiết phiếu nhập (PK kép)
CREATE TABLE ReceiptDetail (
    ReceiptID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice float default 0 NOT NULL,
    PRIMARY KEY (ReceiptID, ProductID),
    CONSTRAINT fk_ReceiptDetail_Receipt FOREIGN KEY (ReceiptID) REFERENCES Receipt(ID),
    CONSTRAINT fk_ReceiptDetail_Product FOREIGN KEY (ProductID) REFERENCES Products(ID)
);

CREATE TABLE Categories (
    ID INT IDENTITY PRIMARY KEY,
    CategoryCode VARCHAR(20) UNIQUE NOT NULL,
    CategoryName NVARCHAR(255) NOT NULL
);

ALTER TABLE Products
ADD CategoryID INT;

ALTER TABLE Products
ADD CONSTRAINT fk_Product_Category FOREIGN KEY (CategoryID) REFERENCES Categories(ID);


CREATE TABLE Authors (
    ID INT IDENTITY PRIMARY KEY,
    AuthorCode VARCHAR(20) UNIQUE NOT NULL,
    AuthorName NVARCHAR(255) NOT NULL,
    Bio NVARCHAR(255)
);

CREATE TABLE Publishers (
    ID INT IDENTITY PRIMARY KEY,
    PublisherCode VARCHAR(20) UNIQUE NOT NULL,
    PublisherName NVARCHAR(255) NOT NULL,
    Address NVARCHAR(255),
    Phone VARCHAR(15)
);

CREATE TABLE Books (
    ProductID INT PRIMARY KEY, -- khóa chính đồng thời là FK
    ISBN VARCHAR(20) UNIQUE NOT NULL,
    PublishYear INT,
    PublisherID INT,
    CONSTRAINT fk_Books_Product FOREIGN KEY (ProductID) REFERENCES Products(ID),
    CONSTRAINT fk_Books_Publisher FOREIGN KEY (PublisherID) REFERENCES Publishers(ID)
);


CREATE TABLE BookAuthors (
    ProductID INT NOT NULL,
    AuthorID INT NOT NULL,
    PRIMARY KEY (ProductID, AuthorID),
    CONSTRAINT fk_BookAuthors_Book FOREIGN KEY (ProductID) REFERENCES Books(ProductID),
    CONSTRAINT fk_BookAuthors_Author FOREIGN KEY (AuthorID) REFERENCES Authors(ID)
);

CREATE TABLE Payments (
    ID INT IDENTITY PRIMARY KEY,
    OrderID INT NOT NULL,
    PaymentDate DATETIME DEFAULT GETDATE(),
    Method NVARCHAR(50), -- Cash, Card, BankTransfer, E-Wallet
    Amount float default 0 NOT NULL,
    CONSTRAINT fk_Payment_Order FOREIGN KEY (OrderID) REFERENCES Orders(ID)
);

select * from Account
SELECT MIN(ID) AS MinID, MAX(ID) AS MaxID, COUNT(*) AS TotalRows
FROM Account;






