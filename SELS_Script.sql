/*Create new database name is SELS */
USE MASTER;
GO 
CREATE DATABASE SELS ;
GO

/*Go to the SELS data base to accses the data base*/
USE SELS ;
GO

/*Create user table */
CREATE TABLE dbo.[User] (
ID INT IDENTITY (1,1),
Username  VARCHAR (MAX),
Password VARCHAR(MAX),
Phonenumber VARCHAR(10)
)