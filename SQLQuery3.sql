CREATE TABLE VendorTable(
	VendorID INT IDENTITY(1,1) PRIMARY KEY,
	VendorRegion VARCHAR(100) NOT NULL,
	VendorCreateAt VARCHAR(100) DEFAULT CURRENT_TIMESTAMP,
	VendorEmail VARCHAR(100) NOT NULL UNIQUE,
	VendorGst VARCHAR(100) NOT NULL UNIQUE,
	VendorState VARCHAR(100) NOT NULL
);



INSERT INTO VendorTable (VendorRegion, VendorEmail, VendorGst, VendorState)
VALUES 
('North', 'a@gmail.com', '122AF233', 'Delhi'),
('East', 'b@gmail.com', '222AF023', 'Assam'),
('West', 'c@gmail.com', '322AF233', 'Gujarat'),
('South', 'd@gmail.com', '422AF023', 'Kerala'),
('South', 'e@gmail.com', '522AF143', 'Andhra');




SELECT * FROM VendorTable;
SELECT VendorRegion FROM VendorTable;



CREATE TABLE AccountTable (
    Username VARCHAR(100) NOT NULL,
    UserEmail VARCHAR(255) NOT NULL UNIQUE,
    UserPassword VARCHAR(100) NOT NULL,
    UserMobileNo VARCHAR(15) NOT NULL,
    UserAddress VARCHAR(255) NOT NULL,
    UserId INT IDENTITY(1,1) PRIMARY KEY,   
    UserRole VARCHAR(50) NOT NULL,           
    UserCreateAt DATETIME DEFAULT GETDATE() 
);

SELECT *FROM AccountTable;


INSERT INTO AccountTable (Username, UserEmail, UserPassword, UserMobileNo, UserAddress, UserRole, UserCreateAt)
VALUES 
('a', 'a@gmail.com', 'a123', '932132233', 'Plotno334', 'Vendor', '2012-09-21'),
('b', 'b@gmail.com', 'b123', '913344455', 'sector21', 'UserRole', '2003-02-24'),
('c', 'c@gmail.com', 'c123', '916798444', 'xyz', 'UserRole', '2019-01-01'),
('d', 'd@gmail.com', 'd123', '917423333', 'xyz', 'Vendor', '2009-09-22'),
('e', 'e@gmail.com', 'e123', '913454589', 'xyz', 'Vendor', '2017-12-30');

CREATE TABLE Product (
    ProductId VARCHAR(10) PRIMARY KEY,
    ProductName VARCHAR(50),
    ProductDescription VARCHAR(255),
    ProductType VARCHAR(50),
    VendorId INT,
    ProductPrice VARCHAR(20),
    ProductReview VARCHAR(255),
    ProductRating VARCHAR(10),
    ProductBoughtByPeople INT,
    ProductBrand VARCHAR(50)
);



INSERT INTO Product (ProductId, ProductName, ProductDescription, ProductType, VendorId, ProductPrice, ProductReview, ProductRating, ProductBoughtByPeople, ProductBrand)
VALUES
('1AF2', 'Mobile', 'aa', 'electronic', 2, 'Rs 12334', 'best product', '3.8/5', 10000, 'Realme'),
('2AS3', 'Laptop', 'bbbbb', 'electronic', 3, 'Rs 65000', 'Nice product', '4.5/5', 3348, 'Asus');

INSERT INTO Product (ProductId, ProductName, ProductDescription, ProductType, VendorId, ProductPrice, ProductReview, ProductRating, ProductBoughtByPeople, ProductBrand)
VALUES
('3BF4', 'Smartwatch', 'Nice watch', 'electronic', 4, 'Rs 5999', 'Amazing for fitness', '4.2/5', 5000, 'Samsung'),
('4CF5', 'Tablet', 'Good tablet for studies', 'electronic', 5, 'Rs 22000', 'Great screen and battery life', '4.0/5', 2500, 'Lenovo'),
('5DF6', 'Headphones', 'Noise-cancelling', 'electronic', 6, 'Rs 7999', 'Excellent sound quality', '4.6/5', 12000, 'Sony');


SELECT * FROM Product;

CREATE TABLE OrderTable (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName VARCHAR(50),
    ProductID INT,
    ProductPrice VARCHAR(20),
    UserId INT,
    VendorId INT,
    ProductPaymentMode VARCHAR(20),
    UserName VARCHAR(50)
);

INSERT INTO OrderTable (ProductName, ProductID, ProductPrice, UserId, VendorId, ProductPaymentMode, UserName)
VALUES
('Moblie', 12, 'Rs 233455', 123, 2, 'Cash', 'a'),
('Laptop', 24, 'Rs 334455', 2334, 1, 'Online', 'b'),
('Speaker', 2345, 'Rs 10000', 334, 23, 'Cash', 'c'),
('Mic', 34, 'Rs 499', 33, 1, 'Cash', 'd');


SELECT * FROM OrderTable;


ALTER TABLE Product
ADD ProductStatus VARCHAR(20) DEFAULT 'Active';


UPDATE Product
SET ProductStatus = 'Discontinued'
WHERE ProductId = '1AF2'; -- Update based on the ProductId or any condition


select * from Product;

UPDATE Product
SET ProductStatus = 'Discontinued'
WHERE ProductId IN ('4CF5');


ALTER TABLE VendorTable
ADD ApprovalStatus VARCHAR(20) 
CHECK (ApprovalStatus IN ('Pending', 'Approved', 'Rejected')) 
DEFAULT 'Pending';


select * from VendorTable;

UPDATE VendorTable
SET ApprovalStatus = 'Approved'
WHERE VendorId = 4;


UPDATE VendorTable
SET ApprovalStatus = 'Rejected'
WHERE VendorId = 5;



ALTER TABLE ProductTable
ADD CONSTRAINT fk_vendor_product
FOREIGN KEY (VendorId)
REFERENCES VendorTable(VendorId);
