CREATE DATABASE Cb
USE Cb

CREATE TABLE Users(
  Login CHAR(30),
  Password CHAR(30),
  UserType INT
  )

--������(��������, ����, �������������), ��������, �������

CREATE TABLE �������������������(
  ��� INT PRIMARY KEY,
  ������������� CHAR(30)
  )

CREATE TABLE ���������(
  ��� INT PRIMARY KEY,
  ��������� CHAR(30)
  )

CREATE TABLE �����(
  ��� INT PRIMARY KEY,
  ������������� INT FOREIGN KEY REFERENCES �������������������(���),
  ��������� INT FOREIGN KEY REFERENCES ���������(���),
  �������� CHAR(30),
  ����������� INT,
  ������������������ INT
  )

CREATE TABLE ��������(
  ��� INT PRIMARY KEY,
  ������������ DATE,
  ����� INT FOREIGN KEY REFERENCES �����(���),
  ���� INT,
  ���������� INT,
  ��������� INT
  )

CREATE TABLE �������(
  ��� INT PRIMARY KEY,
  ������������ DATE,
  ����� INT FOREIGN KEY REFERENCES �����(���),
  ����������� INT,
  ���������� INT,
  ��������� INT
  )

  INSERT Users (Login, Password, UserType)
  VALUES ('Administrator', 'admin', 1); 

  INSERT ������������������� (���, �������������)
  VALUES (0, 'IBANEZ');
  INSERT ������������������� (���, �������������)
  VALUES (1, 'LINE 6');
  INSERT ������������������� (���, �������������)
  VALUES (2, 'DDario');

  INSERT ��������� (���, ���������)
  VALUES (0, '������');
  INSERT ��������� (���, ���������)
  VALUES (1, '�����-���������');
  INSERT ��������� (���, ���������)
  VALUES (2, '��������');
  INSERT  ��������� (���, ���������)
  VALUES (3, '������');
  
  INSERT ����� (���, �������������, ���������, ��������, �����������, ������������������)
  VALUES (0, 0, 0, 'GRG 121DX', 18000, 3);
  INSERT ����� (���, �������������, ���������, ��������, �����������, ������������������)
  VALUES (1, 1, 1, 'SPIDER CLASSIC 15', 13000, 2);

  INSERT �������� (���, ������������, �����, ����, ����������, ���������)
  VALUES (0, GETDATE(), 0, 15000, 2, 30000);
  INSERT �������� (���, ������������, �����, ����, ����������, ���������)
  VALUES (1, GETDATE(), 1, 9000, 2, 18000);

  INSERT ������� (���, ������������, �����, �����������, ����������, ���������)
  VALUES (0, '13.06.2019', 0, 18000, 1, 18000);
 
 SELECT * FROM ������������������� WHERE ��� = (SELECT MAX(�������������������.���) FROM �������������������)

  CREATE PROCEDURE dbo.AddManufacturer
  @id INT,
  @manufacturer CHAR(30)
  AS
  BEGIN
    INSERT ������������������� (���, �������������)
  VALUES (@id, @manufacturer);
  END
  GO

  SELECT * FROM ��������� WHERE ��������� = '������'
  SELECT * FROM �������������������
    SELECT * FROM ������������������� WHERE ������������� = 'Line 6'
      DELETE ��������� WHERE ��������� = ''

  CREATE PROCEDURE dbo.AddCategory
  @id INT,
  @category CHAR(30)
  AS
  BEGIN
    INSERT ��������� (���, ���������)
  VALUES (@id, @category);
  END
  GO
  

SELECT * FROM ������������������� WHERE ������������� = 'asdf'