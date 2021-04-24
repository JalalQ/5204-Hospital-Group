USE [DefaultConnection]
GO

/****** Object: Table [dbo].[Staffs] Script Date: 4/23/2021 4:10:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Staffs] (
    [StaffId]      INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (MAX) NULL,
    [LastName]     NVARCHAR (MAX) NULL,
    [Email]        NVARCHAR (MAX) NULL,
    [Contact]      NVARCHAR (MAX) NULL,
    [Position]     NVARCHAR (MAX) NULL,
    [DepartmentId] INT            NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_DepartmentId]
    ON [dbo].[Staffs]([DepartmentId] ASC);


GO
ALTER TABLE [dbo].[Staffs]
    ADD CONSTRAINT [PK_dbo.Staffs] PRIMARY KEY CLUSTERED ([StaffId] ASC);


GO
ALTER TABLE [dbo].[Staffs] with nocheck
    ADD CONSTRAINT [FK_dbo.Staffs_dbo.Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Departments] ([DepartmentId]) ON DELETE CASCADE;


