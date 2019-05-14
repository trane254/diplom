CREATE DATABASE Cb
USE Cb

CREATE TABLE Users(
  Login CHAR(30),
  Password CHAR(30),
  UserType CHAR(30)
  )



CREATE TABLE Prods(
  id_code INT PRIMARY KEY,
  prod CHAR(30)
  )
CREATE TABLE Cliens(
  id_code INT PRIMARY KEY,
  FirstAndSecond_name CHAR(30),
  Adress CHAR(30),
  Phone CHAR(30)
  )
CREATE TABLE Products(
  id_code INT PRIMARY KEY,
  manufacturer INT FOREIGN KEY REFERENCES Prods(id_code),
  name CHAR(50),
  price INT
  )
CREATE TABLE Orders(
  id_code INT PRIMARY KEY,
  Client INT FOREIGN KEY REFERENCES Cliens(id_code),
  product INT FOREIGN KEY REFERENCES Products(id_code),
  Selldata DATE,
  )


INSERT Users (Login, Password, UserType)
  VALUES ('Administrator', 'admin', 'Administrations'),
  ('Seller', 'qwerty', 'Sellers');

INSERT Prods (id_code, prod)
  VALUES (0, 'Gibson'),
  (1, 'Ibanez'),
  (2, 'Line 6')
INSERT Products (id_code, manufacturer, name, price)
  VALUES (0, 0, 'Les Paul Custom Shop', 250000),
  (1, 1, 'GRG121DX', 18000),
  (2, 2, 'SPIDER CLASSIC 15', 13000)
INSERT Cliens (id_code, FirstAndSecond_name, Adress, Phone)
  VALUES (0, '�������� �.�.', '����������, 27', '88005553535');
INSERT Orders (id_code, Client, product, Selldata)
  VALUES (0, 0, 0, GETDATE());




--https://works.doklad.ru/view/qulslt8qYLQ/2.html






INSERT Prods (id_code, prod)
  VALUES (10, 'HUITA');



SELECT MAX(id_code) FROM Prods
SELECT * FROM Users WHERE Login = 'Administrator'
SELECT * FROM Cliens
SELECT * FROM Prods 
SELECT * FROM Users
 
CREATE PROCEDURE dbo.DeleteProds
@id INT
AS
BEGIN
DELETE Prods WHERE id_code = @id
END
GO

CREATE PROCEDURE dbo.DeleteCliens
@id INT
AS
BEGIN
DELETE Cliens WHERE id_code = @id
END
GO

CREATE PROCEDURE dbo.DeleteProducts
@id INT
AS
BEGIN
DELETE Products WHERE id_code = @id
END
GO


CREATE PROCEDURE dbo.DeleteOrders
@id INT
AS
BEGIN
DELETE Products WHERE id_code = @id
END
GO

EXECUTE DeleteProds @id=10 

