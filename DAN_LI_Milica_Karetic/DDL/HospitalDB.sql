-- Dropping the tables before recreating the database in the order depending how the foreign keys are placed.
IF OBJECT_ID('tblUser', 'U') IS NOT NULL DROP TABLE tblUser;
IF OBJECT_ID('tblDoctor', 'U') IS NOT NULL DROP TABLE tblDoctor;
IF OBJECT_ID('tblSickLeave', 'U') IS NOT NULL DROP TABLE tblSickLeave;

-- Checks if the database already exists.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'HospitalDB')
CREATE DATABASE HospitalDB;
GO
USE HospitalDB

CREATE TABLE tblSickLeave (
	SickLeaveID INT IDENTITY(1,1) PRIMARY KEY	NOT NULL,
	SickLeaveDate DATE 							NOT NULL,
	Reason VARCHAR (200)						NOT NULL,
	CompanyName VARCHAR (40),
	EmergencyCase BIT							NOT NULL,
);


CREATE TABLE tblDoctor (
	DoctorID INT IDENTITY(1,1) PRIMARY KEY		NOT NULL,
	FirstName VARCHAR (40)						NOT NULL,
	LastName VARCHAR (40)						NOT NULL,
	JMBG VARCHAR (13)							NOT NULL,
	BankAccount VARCHAR (40)					NOT NULL,
	Username VARCHAR (40) UNIQUE				NOT NULL,
	Password VARCHAR (40)					NOT NULL,	
);




CREATE TABLE tblUser(
	UserID INT IDENTITY(1,1) PRIMARY KEY 	NOT NULL,
	FirstName VARCHAR (40)					NOT NULL,
	LastName VARCHAR (40)					NOT NULL,
	JMBG VARCHAR (13)						NOT NULL,
	HealthInsuranceNumber VARCHAR (40)		NOT NULL,
	Username VARCHAR (40) UNIQUE			NOT NULL,
	Password VARCHAR (40)				NOT NULL,
	DoctorID INT FOREIGN KEY REFERENCES tblDoctor(DoctorID),
	SickLeaveID INT FOREIGN KEY REFERENCES tblSickLeave(SickLeaveID)
);