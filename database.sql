CREATE DATABASE Cb
USE Cb

CREATE TABLE Users(
  Login CHAR(30),
  Password CHAR(30),
  UserType CHAR(30)
  )

INSERT Users (Login, Password, UserType)
  VALUES ('admin', 'admin', 'Administration');


SELECT * FROM Users