CREATE DATABASE PISTORE

USE PISTORE

CREATE TABLE [User] (
    UserID VARCHAR(10) PRIMARY KEY,          
    Username VARCHAR(30) NOT NULL UNIQUE,    
    Password VARCHAR(50) NOT NULL,
	Role NVARCHAR(10) NOT NULL CHECK (Role IN ('Admin', 'Employee'))
);


CREATE TABLE Employee (
    eID VARCHAR(10) PRIMARY KEY,       
    UserID VARCHAR(10) NOT NULL,              
    Name NVARCHAR(50) NOT NULL,               
    Email NVARCHAR(255) NOT NULL,             
    Phone VARCHAR(11) NOT NULL,               
    Address NVARCHAR(255) NOT NULL,           
    Salary DECIMAL(15,2) NOT NULL,            
    HireDate DATE NOT NULL,                   
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);



CREATE TABLE CLIENT (
	cID varchar(10) not null primary key , 
	name nvarchar(50) not null  ,
	email nvarchar(255) not null ,
	phone varchar(11) not null ,
	address nvarchar(255) not null 
)

CREATE TABLE PRODUCT ( 
	pID VARCHAR(10) NOT NULL PRIMARY KEY , 
	NAME NVARCHAR(255) NOT NULL , 
	DESCRIPTION TEXT NOT NULL , 
	PRICE DECIMAL(8,2) NOT NULL ,
	QUANTITY INT NOT NULL DEFAULT 0 

)

CREATE TABLE ORDERS ( 
	oID VARCHAR(10) NOT NULL PRIMARY KEY , 
	cID VARCHAR(10) NOT NULL ,
	eID VARCHAR(10) NOT NULL ,
	ORDERDATE DATETIME NOT NULL DEFAULT GETDATE(), 
	TOTAL DECIMAL(15,2) NOT NULL , 
	FOREIGN KEY (cID) REFERENCES CLIENT(cID) , 
	FOREIGN KEY (eID) REFERENCES EMPLOYEE(eID)

)

CREATE TABLE ORDERITEM( 
	oiID VARCHAR(10) NOT NULL PRIMARY KEY , 
	oID VARCHAR(10) NOT NULL , 
	pID VARCHAR(10) NOT NULL , 
	QUANTITY INT NOT NULL ,

	FOREIGN KEY (oID) REFERENCES ORDERS(oID) , 
	FOREIGN KEY (pID) REFERENCES PRODUCT(pID)
)

CREATE TABLE BILL (
	bID VARCHAR(10) NOT NULL PRIMARY KEY , 
	oID VARCHAR(10) NOT NULL , 
	cID VARCHAR(10) NOT NULL , 
	eID VARCHAR(10) NOT NULL ,
	BILLDATE DATETIME DEFAULT GETDATE() 

	FOREIGN KEY (oID) REFERENCES ORDERS(oID) , 
	FOREIGN KEY (cID) REFERENCES CLIENT(cID) , 
	FOREIGN KEY (eID) REFERENCES EMPLOYEE(eID)
)

GO
CREATE PROCEDURE GenerateUserID
AS 
BEGIN
	DECLARE @NewID VARCHAR(10);
    DECLARE @MaxID VARCHAR(10);
    DECLARE @NewNumber INT;

    
    SELECT @MaxID = MAX(userID) from [user]

    IF @MaxID IS NULL
    BEGIN
        
        SET @NewID = 'U000000001';
    END
    ELSE
    BEGIN
        
        SET @NewNumber = CAST(SUBSTRING(@MaxID, 2, 9) AS INT) + 1;

       
        SET @NewID = 'U' + RIGHT('000000000' + CAST(@NewNumber AS VARCHAR(9)), 9);
    END

    
    SELECT @NewID AS NewUserID;
END;
GO




--EID PROC
GO
CREATE PROCEDURE GenerateEmployeeID
AS
BEGIN
    DECLARE @NewID VARCHAR(10);
    DECLARE @MaxID VARCHAR(10);
    DECLARE @NewNumber INT;

    
    SELECT @MaxID = MAX(eID) FROM EMPLOYEE;

    IF @MaxID IS NULL
    BEGIN
        
        SET @NewID = 'E000000001';
    END
    ELSE
    BEGIN
        
        SET @NewNumber = CAST(SUBSTRING(@MaxID, 2, 9) AS INT) + 1;

       
        SET @NewID = 'E' + RIGHT('000000000' + CAST(@NewNumber AS VARCHAR(9)), 9);
    END

    
    SELECT @NewID AS NewEmployeeID;
END;
GO

GO
CREATE PROCEDURE GenerateClientID
AS
BEGIN
    DECLARE @NewID VARCHAR(10);
    DECLARE @MaxID VARCHAR(10);
    DECLARE @NewNumber INT;

    
    SELECT @MaxID = MAX(cID) FROM CLIENT;

    IF @MaxID IS NULL
    BEGIN
        
        SET @NewID = 'C000000001';
    END
    ELSE
    BEGIN
        
        SET @NewNumber = CAST(SUBSTRING(@MaxID, 2, 9) AS INT) + 1;

        
        SET @NewID = 'C' + RIGHT('000000000' + CAST(@NewNumber AS VARCHAR(9)), 9);
    END

   
    SELECT @NewID AS NewClientID;
END;
GO

GO
CREATE PROCEDURE GenerateProductID
AS
BEGIN
    DECLARE @NewID VARCHAR(10);
    DECLARE @MaxID VARCHAR(10);
    DECLARE @NewNumber INT;

    
    SELECT @MaxID = MAX(pID) FROM PRODUCT;

    IF @MaxID IS NULL
    BEGIN
        
        SET @NewID = 'P000000001';
    END
    ELSE
    BEGIN
        
        SET @NewNumber = CAST(SUBSTRING(@MaxID, 2, 9) AS INT) + 1;

        
        SET @NewID = 'P' + RIGHT('000000000' + CAST(@NewNumber AS VARCHAR(9)), 9);
    END

    
    SELECT @NewID AS NewProductID;
END;
GO

GO
CREATE PROCEDURE GenerateOrderID
AS
BEGIN
    DECLARE @NewID VARCHAR(10);
    DECLARE @MaxID VARCHAR(10);
    DECLARE @NewNumber INT;

    
    SELECT @MaxID = MAX(oID) FROM ORDERS;

    IF @MaxID IS NULL
    BEGIN
        
        SET @NewID = 'O000000001';
    END
    ELSE
    BEGIN
        
        SET @NewNumber = CAST(SUBSTRING(@MaxID, 2, 9) AS INT) + 1;

        
        SET @NewID = 'O' + RIGHT('000000000' + CAST(@NewNumber AS VARCHAR(9)), 9);
    END

    
    SELECT @NewID AS NewOrderID;
END;
GO

GO
CREATE PROCEDURE GenerateBillID
AS
BEGIN
    DECLARE @NewID VARCHAR(10);
    DECLARE @MaxID VARCHAR(10);
    DECLARE @NewNumber INT;

    
    SELECT @MaxID = MAX(bID) FROM BILL;

    IF @MaxID IS NULL
    BEGIN
        
        SET @NewID = 'B000000001';
    END
    ELSE
    BEGIN
        
        SET @NewNumber = CAST(SUBSTRING(@MaxID, 2, 9) AS INT) + 1;

        
        SET @NewID = 'B' + RIGHT('000000000' + CAST(@NewNumber AS VARCHAR(9)), 9);
    END

    
    SELECT @NewID AS NewBillID;
END;
GO

GO
CREATE PROCEDURE GenerateOrderItemID
AS
BEGIN
    DECLARE @NewID VARCHAR(10);
    DECLARE @MaxID VARCHAR(10);
    DECLARE @NewNumber INT;

    
    SELECT @MaxID = MAX(oiID) FROM ORDERITEM;

    IF @MaxID IS NULL
    BEGIN
        
        SET @NewID = 'I000000001';
    END
    ELSE
    BEGIN
        
        SET @NewNumber = CAST(SUBSTRING(@MaxID, 2, 9) AS INT) + 1;

        
        SET @NewID = 'I' + RIGHT('000000000' + CAST(@NewNumber AS VARCHAR(9)), 9);
    END

    
    SELECT @NewID AS NewOrderItemID;
END;
GO


--EXEC GenerateEmployeeID
--EXEC GenerateClientID
--EXEC GenerateProductID
--EXEC GenerateOrderID
--EXEC GenerateOrderItemID
--EXEC GenerateUserID 
--EXEC GenerateBillID

--DROP PROC GenerateEmployeeID
--DROP PROC GenerateClientID
--DROP PROC GenerateProductID
--DROP PROC GenerateOrderID
--DROP PROC GenerateOrderItemID
--DROP PROC GenerateUserID




--select * from [user]
--select * from CLIENT
--delete from CLIENT where cid like 'C000000003'

INSERT INTO [USER] (UserID, Username, Password, Role) values 
	('U000000001', 'admin','123','Admin'),
	('U000000002', 'employee1', 'pass5678', 'Employee'),
	('U000000003', 'employee2', 'pass9101', 'Employee');

INSERT INTO Employee (eID, UserID, Name, Email, Phone, Address, Salary, HireDate) VALUES 
('E000000001', 'U000000002', 'John Doe', 'johndoe@example.com', '0912345678', '123 Main St', 50000.00, '2023-06-15'),
('E000000002', 'U000000003', 'Jane Smith', 'janesmith@example.com', '0918765432', '456 Elm St', 52000.00, '2023-07-01'),
('E000000003', 'U000000001', 'Chanh Nguyen', 'chanhnguyen34@gmail.com', '0393514981', 'abcd tphcm', 100000.00, '2022-01-02');

INSERT INTO CLIENT (cID, name, email, phone, address) VALUES 
('C000000001', 'John Doe', 'john.doe@example.com', '0912345678', '123 Elm St, City A'),
('C000000002', 'Jane Smith', 'jane.smith@example.com', '0923456789', '456 Oak St, City B'),
('C000000003', 'Michael Johnson', 'michael.j@example.com', '0934567890', '789 Pine St, City C'),
('C000000004', 'Emily Davis', 'emily.d@example.com', '0945678901', '101 Maple St, City D'),
('C000000005', 'Christopher Brown', 'chris.brown@example.com', '0956789012', '202 Birch St, City E'),
('C000000006', 'Sarah Wilson', 'sarah.wilson@example.com', '0967890123', '303 Cedar St, City F'),
('C000000007', 'David Martinez', 'david.martinez@example.com', '0978901234', '404 Cherry St, City G'),
('C000000008', 'Laura Lee', 'laura.lee@example.com', '0989012345', '505 Walnut St, City H'),
('C000000009', 'Daniel Garcia', 'daniel.garcia@example.com', '0990123456', '606 Spruce St, City I'),
('C000000010', 'Samantha Rodriguez', 'samantha.r@example.com', '0911223344', '707 Fir St, City J');


INSERT INTO PRODUCT (pID, NAME, DESCRIPTION, PRICE, QUANTITY) VALUES 
('P000000001', 'Apple iPhone 13', 'iPhone with 128GB storage.', 185000.00, 100),
('P000000002', 'Samsung Galaxy A32', 'Mid-range smartphone.', 175000.00, 150),
('P000000003', 'Xiaomi Redmi Note 10', 'Smartphone with AMOLED display.', 150000.00, 120),
('P000000004', 'Oppo A54', 'Affordable smartphone with big battery.', 140000.00, 200),
('P000000005', 'Vivo Y20', 'Smartphone with 5000mAh battery.', 130000.00, 180),
('P000000006', 'Realme 8', 'Smartphone with fast charging.', 165000.00, 90),
('P000000007', 'Motorola Moto G Power', 'Long-lasting battery smartphone.', 120000.00, 160),
('P000000008', 'Nokia G20', '48MP camera smartphone.', 115000.00, 110),
('P000000009', 'Sony WH-CH510', 'Wireless headphones, 35 hours playtime.', 80000.00, 140),
('P000000010', 'JBL Go 2', 'Portable Bluetooth speaker.', 85000.00, 100),
('P000000011', 'Dell Inspiron 15', '15-inch laptop with Intel i5.', 180000.00, 80),
('P000000012', 'Lenovo IdeaPad 3', 'Laptop with AMD Ryzen 5.', 170000.00, 75),
('P000000013', 'HP Pavilion x360', '2-in-1 laptop with touchscreen.', 160000.00, 50),
('P000000014', 'Acer Aspire 5', 'Lightweight laptop for daily tasks.', 150000.00, 60),
('P000000015', 'Apple AirPods', 'Wireless earbuds.', 110000.00, 130),
('P000000016', 'Samsung Galaxy Buds', 'True wireless earbuds.', 120000.00, 140),
('P000000017', 'Fitbit Inspire 2', 'Fitness tracker.', 90000.00, 160),
('P000000018', 'Xiaomi Mi Band 6', 'Fitness tracker with AMOLED display.', 70000.00, 200),
('P000000019', 'GoPro HERO9', 'Action camera.', 180000.00, 50),
('P000000020', 'Canon EOS M50', 'Mirrorless camera with 4K video.', 185000.00, 20),
('P000000021', 'Nikon D3500', 'User-friendly DSLR.', 160000.00, 25),
('P000000022', 'Lenovo Legion 5', 'Gaming laptop with RTX 3060.', 185000.00, 15),
('P000000023', 'ASUS TUF Gaming Monitor', '27-inch gaming monitor.', 140000.00, 30),
('P000000024', 'Dell UltraSharp U2419H', '24-inch monitor with color accuracy.', 130000.00, 40),
('P000000025', 'Samsung Galaxy Tab S7', 'High-performance tablet.', 180000.00, 10),
('P000000026', 'Amazon Kindle', 'E-reader with light.', 120000.00, 70),
('P000000027', 'Sony SRS-XB12', 'Compact Bluetooth speaker.', 110000.00, 80),
('P000000028', 'Anker PowerCore 10000', 'Portable charger.', 90000.00, 150),
('P000000029', 'Razer Kraken X', 'Lightweight gaming headset.', 140000.00, 60),
('P000000030', 'Logitech G203', 'Wired gaming mouse.', 90000.00, 100),
('P000000031', 'Corsair K55 RGB', 'Gaming keyboard.', 110000.00, 90),
('P000000032', 'TP-Link Archer A6', 'Dual-band Wi-Fi router.', 130000.00, 110),
('P000000033', 'Seagate 1TB External HDD', 'Portable hard drive.', 140000.00, 70),
('P000000034', 'Logitech C920', 'HD webcam.', 150000.00, 50),
('P000000035', 'Epson EcoTank ET-2720', 'All-in-one inkjet printer.', 185000.00, 20),
('P000000036', 'Brother HL-L2350DW', 'Compact laser printer.', 120000.00, 60),
('P000000037', 'Amazon Fire TV Stick', 'Streaming device.', 90000.00, 80),
('P000000038', 'NVIDIA GeForce GTX 1650', 'Graphics card.', 150000.00, 30),
('P000000039', 'HyperX Cloud II', 'Gaming headset.', 160000.00, 50),
('P000000040', 'Razer Huntsman Mini', 'Compact gaming keyboard.', 180000.00, 40),
('P000000041', 'Apple MacBook Air', 'Lightweight laptop with M1 chip.', 185000.00, 10),
('P000000042', 'Dell XPS 13', 'Premium ultrabook.', 180000.00, 15),
('P000000043', 'Microsoft Surface Pro 7', '2-in-1 laptop.', 175000.00, 20),
('P000000044', 'Oculus Quest 2', 'VR headset.', 160000.00, 10),
('P000000045', 'Google Nest Mini', 'Smart speaker.', 90000.00, 100),
('P000000046', 'Roku Streaming Stick+', 'Streaming device.', 110000.00, 80),
('P000000047', 'Anker Soundcore Mini', 'Compact Bluetooth speaker.', 80000.00, 150),
('P000000048', 'Xiaomi Mi 4K Action Camera', '4K action camera.', 175000.00, 25),
('P000000049', 'Samsung Galaxy Z Flip 3', 'Foldable smartphone.', 185000.00, 5),
('P000000050', 'Fitbit Versa 3', 'Smartwatch with GPS.', 170000.00, 20);





INSERT INTO ORDERS (oID, cID, eID, ORDERDATE, TOTAL) VALUES 
('O000000001', 'C000000001', 'E000000001', '2024-11-01', 455000.00),
('O000000002', 'C000000002', 'E000000002', '2024-11-01', 290000.00),
('O000000003', 'C000000003', 'E000000003', '2024-11-01', 345000.00),
('O000000004', 'C000000004', 'E000000001', '2024-11-01', 370000.00),
('O000000005', 'C000000005', 'E000000002', '2024-11-01', 295000.00);


INSERT INTO ORDERITEM (oiID, oID, pID, QUANTITY) VALUES 
('I000000001', 'O000000001', 'P000000001', 2),  
('I000000002', 'O000000001', 'P000000002', 1),  
('I000000003', 'O000000002', 'P000000003', 3),  
('I000000004', 'O000000002', 'P000000004', 2),  
('I000000005', 'O000000003', 'P000000005', 1),  
('I000000006', 'O000000003', 'P000000006', 2),  
('I000000007', 'O000000004', 'P000000007', 1),  
('I000000008', 'O000000004', 'P000000008', 1),  
('I000000009', 'O000000005', 'P000000009', 3),  
('I000000010', 'O000000005', 'P000000010', 1);  


INSERT INTO BILL (bID, oID, cID, eID, BILLDATE) VALUES 
('B000000001', 'O000000001', 'C000000001', 'E000000001', '2024-11-02'),
('B000000002', 'O000000002', 'C000000002', 'E000000002', '2024-11-03'),
('B000000003', 'O000000003', 'C000000003', 'E000000003', '2024-11-04'),
('B000000004', 'O000000004', 'C000000004', 'E000000001', '2024-12-01'),
('B000000005', 'O000000005', 'C000000005', 'E000000002', '2023-11-04');


