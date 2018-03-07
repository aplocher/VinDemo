# VinDemo

This was a simple demo application I did that utilizes:

* Fully decoupled layers (separated into separate projects)
* Database projects for generating schema
* AutoFac for IoC
* Unit tests
* Entry points from separate WebAPI and MVC5 projects using decoupled/shared business logic
* Repository / UnitOfWork patterns

Things I would have changed (and may someday):

* Get rid of Entity Framework
    * Or, get rid of my repository implementation and use EF's built in UoW/Repo (IDbContext/IDbSet)
* Used DTOs and AutoMapper and implemented validation annotations outside the domain layer (without this it's nearly impossible to utilize the database when validating - e.g. like checking if a username already exists in the database)
* Finish writing unit tests