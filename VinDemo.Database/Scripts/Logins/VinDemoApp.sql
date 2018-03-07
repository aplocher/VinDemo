CREATE LOGIN [VinDemoApp] WITH PASSWORD = 'RjcWpoIzwxjQtIxymcqlvmsFT7_#$'
GO

/*
 TODO - User probably shouldnt be god */
ALTER SERVER ROLE [sysadmin] ADD MEMBER [VinDemoApp]
GO