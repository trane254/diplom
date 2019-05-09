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
  product INT FOREIGN KEY REFERENCES Products(id_code)
  )

--https://works.doklad.ru/view/qulslt8qYLQ/2.html








INSERT Users (Login, Password, UserType)
  VALUES ('Administrator', 'admin', 'Administrations');
INSERT Users (Login, Password, UserType)
  VALUES ('Seller', 'qwerty', 'Sellers');


SELECT * FROM Users WHERE Login = 'Administrator'



SELECT * FROM Users
  
  
  DELETE Users 