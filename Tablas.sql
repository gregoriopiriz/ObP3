CREATE DATABASE Aliyaba

USE Aliyaba

CREATE TABLE DocumentTypes
(
    Name [VARCHAR](20) NOT NULL PRIMARY KEY,
);

CREATE TABLE Customers
(
    UserName [VARCHAR](20) NOT NULL PRIMARY KEY,

    Password [VARCHAR](20) NOT NULL,
    Name [VARCHAR](20) NOT NULL,
    Surname [VARCHAR](20) NOT NULL,
    Document [VARCHAR](20) NOT NULL,
    Email [VARCHAR](30) NOT NULL,
    PhoneNumber [VARCHAR](30) NOT NULL,

    -- *FOREIGN KEYS* --
    DocumentTypeName [VARCHAR](20),
    CONSTRAINT FK_Customers_DocumentTypes FOREIGN KEY (DocumentTypeName) REFERENCES DocumentTypes(Name)
);

CREATE TABLE Locations
(
    ID INT NOT NULL PRIMARY KEY,

    Latitude FLOAT NOT NULL,
    Longitude FLOAT NOT NULL,

    -- *FOREIGN KEYS* --
    CustomerUsername [VARCHAR](20),
    CONSTRAINT FK_Locations_Customers FOREIGN KEY (CustomerUsername) REFERENCES Customers(UserName)
);

CREATE TABLE Positions
(
    Name [VARCHAR](20) NOT NULL PRIMARY KEY,

    Salary FLOAT NOT NULL
);


CREATE TABLE Employees
(
    ID INT NOT NULL PRIMARY KEY,

    UserName [VARCHAR](20) NOT NULL,
    Password [VARCHAR](20) NOT NULL,
    Name [VARCHAR](20) NOT NULL,
    Surname [VARCHAR](20) NOT NULL,

    -- *FOREIGN KEYS* --
    PositionName [VARCHAR](20),
    CONSTRAINT FK_Employees_Positions FOREIGN KEY (PositionName) REFERENCES Positions(Name),
);

CREATE TABLE States
(
    ID INT NOT NULL PRIMARY KEY,

    Name [VARCHAR](20) NOT NULL,
    Date DATETIME NOT NULL,

    -- *FOREIGN KEYS* --
    EmployeeID INT,
    CONSTRAINT FK_States_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees(ID)
);


CREATE TABLE Orders
(
    ID INT NOT NULL PRIMARY KEY,

    IsExpress BIT NOT NULL,
    TotalPrice FLOAT NOT NULL,
    EntryDate DATETIME NOT NULL,

    -- *FOREIGN KEYS* --
    StateID INT,
	CONSTRAINT FK_Orders_States FOREIGN KEY (StateID) REFERENCES States(ID),
    LocationID INT,
    CONSTRAINT FK_Orders_Location FOREIGN KEY (LocationID) REFERENCES Locations(ID),
    CustomerUsername [VARCHAR](20),
    CONSTRAINT FK_Orders_Cutomers FOREIGN KEY (CustomerUsername) REFERENCES Customers(UserName)
);


CREATE TABLE Deliveries
(
    ID INT NOT NULL PRIMARY KEY,

    DepartureDate DATETIME NOT NULL,
    CommitDate DATETIME NOT NULL,
    VehiclePlate [VARCHAR](7),

    -- *FOREIGN KEYS* --
    EmployeeID INT,
    CONSTRAINT FK_Deliveries_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees(ID),
);

CREATE TABLE OrdersPrepare
(
    ID INT NOT NULL PRIMARY KEY,

    Date DATETIME NOT NULL,

    -- *FOREIGN KEYS* --
    OrderID INT,
    CONSTRAINT FK_Orders_OrdersPrepare FOREIGN KEY (OrderID) REFERENCES Orders(ID),
    DeliveryID INT,
    CONSTRAINT FK_Orders_Deliveries FOREIGN KEY (DeliveryID) REFERENCES Deliveries(ID),
    EmployeeID INT,
    CONSTRAINT FK_Orders_Employees FOREIGN KEY (EmployeeID) REFERENCES Employees(ID)
);

CREATE TABLE Categories
(
    ID INT NOT NULL PRIMARY KEY,

    Name [VARCHAR](20) NOT NULL,
);

CREATE TABLE Products
(
    Code [VARCHAR](20) NOT NULL PRIMARY KEY,

    Description [VARCHAR](200) NOT NULL,

    -- *FOREIGN KEYS* --
    CategoryID INT,
    CONSTRAINT FK_Products_Categorys FOREIGN KEY (CategoryID) REFERENCES Categories(ID),
);

CREATE TABLE OrderDetails
(
    ID INT NOT NULL PRIMARY KEY,

    Quantity INT NOT NULL,

    -- *FOREIGN KEYS* --
    ProductCode [VARCHAR](20),
    CONSTRAINT FK_OrderDetails_Products FOREIGN KEY (ProductCode) REFERENCES Products(Code),
    OrderID INT,
    CONSTRAINT FK_OrderDetails_Orders FOREIGN KEY (OrderID) REFERENCES Orders(ID)
);

CREATE TABLE Photos
(
    ID INT NOT NULL PRIMARY KEY,

    Photo [VARBINARY](MAX),

    -- *FOREIGN KEYS* --
    ProductCode [VARCHAR](20),
    CONSTRAINT FK_Photos_Products FOREIGN KEY (ProductCode) REFERENCES Products(Code),
);

CREATE TABLE Stocks
(
    ID INT NOT NULL PRIMARY KEY,

    Location [VARCHAR](20) NOT NULL,
	Reason [VARCHAR] (200) NOT NULL,
	Date DATETIME NOT NULL,
    Quantity INT NOT NULL,

    -- *FOREIGN KEYS* --
    ProductCode [VARCHAR](20),
    CONSTRAINT FK_Stocks_Products FOREIGN KEY (ProductCode) REFERENCES Products(Code),
);

CREATE TABLE Prices
(
    ID INT NOT NULL PRIMARY KEY,

    Price FLOAT NOT NULL,
    Date DATETIME NOT NULL,

    -- *FOREIGN KEYS* --
    ProductCode [VARCHAR](20),
    CONSTRAINT FK_Prices_Products FOREIGN KEY (ProductCode) REFERENCES Products(Code),
);
