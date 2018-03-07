CREATE TABLE [dbo].[Member] (
    [MemberId]        INT           IDENTITY (1, 1) NOT NULL,
    [Username]        VARCHAR (12)  NOT NULL,
    [FirstName]       VARCHAR (100) NOT NULL,
    [LastName]        VARCHAR (100) NOT NULL,
    [Email]           VARCHAR (75)  NOT NULL,
    [PhoneNumber]     VARCHAR (10)  NULL,
    [DateOfBirth]     DATETIME      NOT NULL,
    [CreatedDate]     DATETIME      NOT NULL,
    [ModifiedDate]    DATETIME      NULL,
    [DeactivatedDate] DATETIME      NULL,
    CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED ([MemberId] ASC),
    CONSTRAINT [UQ_Member_Username] UNIQUE NONCLUSTERED ([Username] ASC)
);

