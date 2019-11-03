
# CleanArchitecture

CleanArchitecture  is a sample application built using ASP.NET Core and Entity Framework Core. 


## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or Visual Studio 2019](https://visualstudio.microsoft.com/vs/) (version 16.3 or later)
* [.NET Core SDK 3](https://dotnet.microsoft.com/download/dotnet-core/3.0)

### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  2. At the root directory, restore required packages by running:
      ```
     dotnet restore
     ```
  3. Next, build the solution by running:
     ```
     dotnet build
     ```

  4.  Launch the app  by running:
     ```
	 dotnet run
	 ```
  
  5. Launch [https://localhost:44336/api](http://localhost:44336/api) in your browser to view the API

## Technologies
* .NET Core 3
* ASP.NET Core 3
* Entity Framework Core 3


## Functionalities
-   Basic CRUD operations with CQRS+ MediatR  
-   Authentication backed by JWT (not yet fully implemented)

## TO-DO list
-   Full API testing results will be provided once the application will have been completed
-   Unit tests will be planned to do using FluentAssertions
-	RabbitMQ as a message queue with Masstransit
-	Logging with Serilog
-	Caching with Redis
-   Building service, database and UI using Docker from scratch




