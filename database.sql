CREATE DATABASE Cb
USE Cb

CREATE TABLE Users(
  Login CHAR(30),
  Password CHAR(30),
  UserType BINARY
  )

--товары(названия, типы, производители), поставки, продажи

CREATE TABLE ПроизводителиТовара(
  Код INT PRIMARY KEY,
  Производитель CHAR(30)
  )

CREATE TABLE ТипыТовара(
  Код INT PRIMARY KEY,
  Тип CHAR(30)
  )

CREATE TABLE Товар(
  Код INT PRIMARY KEY,
  Производитель INT FOREIGN KEY REFERENCES ПроизводителиТовара(Код),
  Тип INT FOREIGN KEY REFERENCES ТипыТовара(Код),
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


  INSERT ПроизводителиТовара (Код, Производитель)
  VALUES (0, 'IBANEZ');
  INSERT ПроизводителиТовара (Код, Производитель)
  VALUES (1, 'LINE 6');
  INSERT ПроизводителиТовара (Код, Производитель)
  VALUES (2, 'DDario');

  INSERT ТипыТовара (Код, Тип)
  VALUES (0, 'Гитара');
  INSERT ТипыТовара (Код, Тип)
  VALUES (1, 'Комбо-усилитель');
  INSERT ТипыТовара (Код, Тип)
  VALUES (2, 'Медиатор');
  INSERT  ТипыТовара (Код, Тип)
  VALUES (3, 'Струны');
  
  INSERT Товар (Код, Производитель, Тип, Название, ЦенаПродажи, КоличествоНаСкладе)
  VALUES (0, 0, 0, 'GRG 121DX', 18000, 3);
  INSERT Товар (Код, Производитель, Тип, Название, ЦенаПродажи, КоличествоНаСкладе)
  VALUES (1, 1, 1, 'SPIDER CLASSIC 15', 13000, 2);

  INSERT Поставки (Код, ДатаПоставки, Товар, Цена, Количество, Стоимость)
  VALUES (0, GETDATE(), 0, 15000, 2, 30000);
  INSERT Поставки (Код, ДатаПоставки, Товар, Цена, Количество, Стоимость)
  VALUES (1, GETDATE(), 1, 9000, 2, 18000);

  INSERT Продажи (Код, ДатаПоставки, Товар, ЦенаПродажи, Количество, Стоимость)
  VALUES (0, '13.06.2019', 0, 18000, 1, 18000);

