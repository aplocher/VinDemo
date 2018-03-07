/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
SET IDENTITY_INSERT Member ON
GO

MERGE INTO Member AS target
USING (VALUES
(1 ,'mario', 'Mario', 'Bros', 'mario@nintendo.com', '5555551212', '02/10/1986', GETUTCDATE(), GETUTCDATE(), null)            ,
(2 ,'luigi', 'Luigi', 'Bros', 'luigi@nintendo.com', '5555551212', '05/12/1979', GETUTCDATE(), GETUTCDATE(), null)            ,
(3 ,'sonic', 'Sonic', 'Hedgehog', 'sonic@sega.com', '5555551212', '01/10/1984', GETUTCDATE(), GETUTCDATE(), null)            ,
(4 ,'lara', 'Lara', 'Croft', 'lcroft@eidos.com', '5555551212', '01/08/1990', GETUTCDATE(), GETUTCDATE(), null)               ,
(5 ,'ryuh', 'Ryu', 'Hayabusa', 'ry@gaiden.com','5555551212', '12/09/1956', GETUTCDATE(), GETUTCDATE(), null)                  ,
(6 ,'mister', 'Mister', 'Pacman', 'misterpac@namco.com', '5555551212', '02/21/1990', GETUTCDATE(), GETUTCDATE(), null)       ,
(7 ,'miss', 'Miss', 'Pacman', 'misspac@namco.com', '5555551212', '10/31/1993', GETUTCDATE(), GETUTCDATE(), null)             ,
(8 ,'maxp', 'Max', 'Payne', 'max@pay.ne', '5555551212', '01/05/1981', GETUTCDATE(), GETUTCDATE(), null)                       ,
(9 ,'john', 'John', 'Marston', 'john@marstonfamily.net', '5555551212', '03/29/1984', GETUTCDATE(), GETUTCDATE(), null)        ,
(10,'princess', 'Princess', 'Zelda', 'zelda@hyrule.com', '5555551212', '12/26/1986', GETUTCDATE(), GETUTCDATE(), null)       ,
(11,'maestro', 'Maestro', 'Jefe', 'chief@unsc.gov', '5555551212', '09/08/1999', GETUTCDATE(), GETUTCDATE(), null)            ,
(12,'link', 'Link', 'Legend', 'link@hyrule.com', '5555551212', '05/01/2000', GETUTCDATE(), GETUTCDATE(), null)               ,
(13,'gordon', 'Gordon', 'Freeman', 'gordan.freeman@city17.net', '5555551212', '05/20/1973', GETUTCDATE(), GETUTCDATE(), null)
)
AS source (MemberId, Username, FirstName, LastName, Email, PhoneNumber, DateOfBirth, CreatedDate, ModifiedDate, DeactivatedDate)
ON target.MemberId=source.MemberId
WHEN MATCHED THEN
UPDATE SET Username=source.Username, FirstName=source.FirstName, LastName=source.LastName, Email=source.Email, PhoneNumber=source.PhoneNumber, DateOfBirth=source.DateOfBirth, CreatedDate=source.CreatedDate, ModifiedDate=source.ModifiedDate, DeactivatedDate=source.DeactivatedDate
WHEN NOT MATCHED BY TARGET THEN
INSERT (MemberId, Username, FirstName, LastName, Email, PhoneNumber, DateOfBirth, CreatedDate, ModifiedDate, DeactivatedDate) VALUES (MemberId, Username, FirstName, LastName, Email, PhoneNumber, DateOfBirth, CreatedDate, ModifiedDate, DeactivatedDate)
WHEN NOT MATCHED BY SOURCE THEN
DELETE;

SET IDENTITY_INSERT Member OFF
GO