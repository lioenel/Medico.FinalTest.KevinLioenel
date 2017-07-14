CREATE TABLE [dbo].[Employee] (
    [Code]    VARCHAR (50)  NOT NULL,
    [Name]    VARCHAR (MAX) CONSTRAINT [DF_Employee_Name] DEFAULT ('') NOT NULL,
    [Phone]   VARCHAR (MAX) CONSTRAINT [DF_Employee_Phone] DEFAULT ('') NOT NULL,
    [Address] VARCHAR (MAX) CONSTRAINT [DF_Employee_Address] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([Code] ASC)
);

