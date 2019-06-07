CREATE DATABASE Cb
USE Cb

CREATE TABLE Users(
  Login CHAR(30),
  Password CHAR(30),
  UserType INT
  )

--товары(названия, типы, производители), поставки, продажи

CREATE TABLE ПроизводителиТовара(
  Код INT PRIMARY KEY,
  Производитель CHAR(30)
  )

CREATE TABLE Категория(
  Код INT PRIMARY KEY,
  Категория CHAR(30)
  )

CREATE TABLE Товар(
  Код INT PRIMARY KEY,
  Производитель INT FOREIGN KEY REFERENCES ПроизводителиТовара(Код),
  Категория INT FOREIGN KEY REFERENCES Категория(Код),
  Название CHAR(30),
  ЦенаПродажи INT,
  КоличествоНаСкладе INT
  )

CREATE TABLE Поставки(
  Код INT PRIMARY KEY,
  ДатаПоставки DATE,
  Товар INT FOREIGN KEY REFERENCES Товар(Код),
  Цена INT,
  Количество INT,
  Стоимость INT
  )

CREATE TABLE Продажи(
  Код INT PRIMARY KEY,
  ДатаПоставки DATE,
  Товар INT FOREIGN KEY REFERENCES Товар(Код),
  ЦенаПродажи INT,
  Количество INT,
  Стоимость INT
  )

  INSERT Users (Login, Password, UserType)
  VALUES ('Administrator', 'admin', 1); 
  INSERT Users (Login, Password, UserType)
  VALUES ('Seller', 'Lespaul228', 0);

  INSERT ПроизводителиТовара (Код, Производитель)
  VALUES (0, 'IBANEZ');
  INSERT ПроизводителиТовара (Код, Производитель)
  VALUES (1, 'LINE 6');
  INSERT ПроизводителиТовара (Код, Производитель)
  VALUES (2, 'DDario');

  INSERT Категория (Код, Категория)
  VALUES (0, 'Гитара');
  INSERT Категория (Код, Категория)
  VALUES (1, 'Комбо-усилитель');
  INSERT Категория (Код, Категория)
  VALUES (2, 'Медиатор');
  INSERT  Категория (Код, Категория)
  VALUES (3, 'Струны');
  
  INSERT Товар (Код, Производитель, Категория, Название, ЦенаПродажи, КоличествоНаСкладе)
  VALUES (0, 0, 0, 'GRG 121DX', 18000, 3);
  INSERT Товар (Код, Производитель, Категория, Название, ЦенаПродажи, КоличествоНаСкладе)
  VALUES (1, 1, 1, 'SPIDER CLASSIC 15', 13000, 2);

  INSERT Поставки (Код, ДатаПоставки, Товар, Цена, Количество, Стоимость)
  VALUES (0, GETDATE(), 0, 15000, 2, 30000);
  INSERT Поставки (Код, ДатаПоставки, Товар, Цена, Количество, Стоимость)
  VALUES (1, GETDATE(), 1, 9000, 2, 18000);

  INSERT Продажи (Код, ДатаПоставки, Товар, ЦенаПродажи, Количество, Стоимость)
  VALUES (0, '13.06.2019', 0, 18000, 1, 18000);
 

  CREATE PROCEDURE dbo.AddManufacturer
  @id INT,
  @manufacturer CHAR(30)
  AS
  BEGIN
    INSERT ПроизводителиТовара (Код, Производитель)
  VALUES (@id, @manufacturer);
  END
  GO

  CREATE PROCEDURE dbo.AddCategory
  @id INT,
  @category CHAR(30)
  AS
  BEGIN
    INSERT Категория (Код, Категория)
  VALUES (@id, @category);
  END
  GO
  


SELECT * FROM ПроизводителиТовара WHERE Производитель = 'asdf'
 SELECT * FROM ПроизводителиТовара WHERE Код = (SELECT MAX(ПроизводителиТовара.Код) FROM ПроизводителиТовара)
  SELECT * FROM Категория 
    SELECT Товар.ЦенаПродажи FROM Товар WHERE Название = ''
SELECT Товар.Код FROM Товар WHERE Название = ''
SELECT * FROM Продажи
SELECT * FROM Товар


UPDATE Товар SET КоличествоНаСкладе = 0 WHERE Название = '';
SELECT Товар.Код FROM Товар WHERE Код = (SELECT MAX(Товар.Код) FROM Товар)
INSERT Товар (Код, Производитель, Категория, Название, ЦенаПродажи, КоличествоНаСкладе) VALUES (0, 0, 0, '', 0, 0);
SELECT ПроизводителиТовара.Код FROM ПроизводителиТовара WHERE Производитель = 'IBANEZ'
SELECT Код FROM Категория WHERE Категория = 'Гитара'
  SELECT Код FROM Поставки п WHERE Код = (SELECT MAX(Код) FROM Поставки)
SELECT Название FROM Товар WHERE Название = 'GRG 121DX'
  SELECT * FROM Товар WHERE Название = 'GRG 121DX'
INSERT Поставки (Код, ДатаПоставки, Товар, Цена, Количество, Стоимость) VALUES (0, GETDATE(), 0, 0, 0, 0)
UPDATE Товар SET КоличествоНаСкладе = 10 WHERE Название = 'GRG 121DX'
SELECT КоличествоНаСкладе, Код FROM Товар WHERE Название = 'GRG 121DX'
SELECT * FROM Users
INSERT Users (Login, Password, UserType) VALUES ('', '', 0);
DELETE Users WHERE Login = ''
SELECT Производитель FROM Товар WHERE Код = (SELECT Код FROM Категория WHERE Категория = 'Гитара')
SELECT Производитель FROM ПроизводителиТовара WHERE Код = 0
SELECT Название, ЦенаПродажи FROM Товар WHERE Производитель = (SELECT Код FROM ПроизводителиТовара WHERE Производитель = 'IBANEZ')
SELECT ЦенаПродажи FROM Товар WHERE Название = 'GRG 121DX'
  SELECT Название FROM Товар WHERE Производитель = (SELECT Код FROM ПроизводителиТовара WHERE Производитель = '')
    SELECT Производитель FROM ПроизводителиТовара WHERE Код = (SELECT Производитель FROM Товар)
SELECT Код FROM Категория WHERE Категория = 'Гитара'
SELECT Производитель FROM Товар WHERE Категория = 0
SELECT Производитель FROM ПроизводителиТовара WHERE Код = 0
SELECT Название FROM Товар WHERE Производитель = 0 AND Категория = 0
SELECT Код FROM Категория WHERE Категория = 'Гитара'
SELECT Код FROM ПроизводителиТовара WHERE Производитель = 'Gibson'
SELECT * FROM Поставки 
SELECT Производитель, Категория FROM Товар WHERE Код = 1
 SELECT Категория FROM Категория WHERE Код = 1
SELECT Название FROM Товар WHERE Код = 0
SELECT * FROM Продажи
SELECT * FROM Товар
SELECT Категория FROM Категория WHERE Код = 
SELECT * FROM Продажи
SELECT * FROM ПроизводителиТовара
UPDATE Товар SET Код = 0, Производитель = 0, Категория = 0 ,Название = '', ЦенаПродажи = 0, КоличествоНаСкладе = 0 WHERE Код = 0;
SELECT Код FROM ПроизводителиТовара  WHERE Производитель = 'IBANEZ'
SELECT * FROM Товар