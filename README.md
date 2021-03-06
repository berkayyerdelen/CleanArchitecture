
# CleanArchitecture

CleanArchitecture  is a sample application built using ASP.NET Core and Entity Framework Core. 


## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or Visual Studio 2019](https://visualstudio.microsoft.com/vs/) (version 16.3 or later)
* [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.0)
* [Couchbase Server 6.0.0 Community](https://www.couchbase.com/downloads)
* [Seq 5.1](https://datalust.co/download)
* [MongoDB 4.2.1](https://www.mongodb.com/download-center/community)
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



     
	 dotnet run
	 
  
  5. Launch [https://localhost:44336/api](http://localhost:44336/api) in your browser to view the API
  6. Serilog http://localhost:5341
  7. CouchBase http://127.0.0.1:8091/ui/
  8. MongoDb http://localhost:27017/serilogs/logs
  9. Hangfire https://localhost:44384/


## Technologies
* .NET Core 3.1
* ASP.NET Core 3.1
* Entity Framework Core 3.1
* CouchBase Server 6.0.0
* Seq 5.1
* MongoDB 4.2.1
* Hangfire 1.7.7


## Functionalities
-   Basic CRUD operations with CQRS + MediatR  
-   Authentication backed by JWT 
-	Caching with Couchbase
-	Logging with Serilog
-	Inserting AuditLogs to MongoDB
-	Recursive method calls for couchbase cache with Hangfire
-	Couchbase management

## TO-DO list
-   Full API testing results will be provided once the application will have been completed
-   Unit tests will be planned to do using FluentAssertions
-	RabbitMQ as a message queue with Masstransit
-   Building service, database and UI using Docker from scratch
-    SPA  =  Angular ?? Vue ?? Svelte;




