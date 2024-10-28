
<h1 align="center">
  Auto-Field Translation Thought Experiment
  <br>
</h1>

<p align="center">
    <a href="#architecture">Architecture</a> •
    <a href="#how-to-run">How To Run</a> •
    <a href="#local-env">Local Environment</a> •
    <a href="#use-cases">Use-Cases</a> •
    <a href="#deployment">Deployment</a>
    <a href="#technologies">Technologies</a>
</p>

<h2 id="architecture">Architecture</h2>

This project is 

<h2 id="how-to-run">How To Run</h2>

To clone and run this application,
you'll need [Git](https://git-scm.com) and [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
installed on your computer.
From your command line:

```bash
# Clone this repository
$ git clone https://github.com/clearbusinessinsight/cbiBackend.git

# Go into the repository
$ cd cbiBackend/src/Web

# Run the app
$ dotnet run
```

<h2 id="local-env">Local Environment</h2>

A local environment can be set up by creating a `.secrets.json` file in the Web project.
The file should contain the following:

```json
{
  "AES_Key": "YourAESKey",
  "Azure": {
    "StorageAccount": "YourStorageAccount",
    "StorageKey": "YourStorageKey",
    "FileStorage": "YourFileStorage"
  },
  "DbConnectionString": "Server=tcp:YourServer,1433;Initial Catalog=YourCatalog;Persist Security Info=False;User ID=YourUserId;Password=YourPassword;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
```

A personal database should be created in Azure for the developer working on the project so you are not directly modifying the production/qa database.

### Migrations

Running database migrations is easy. Ensure you add the following flags to your command (values assume you are executing from repository root)

* `--project src/Infrastructure`
* `--startup-project src/Web`
* `--output-dir Data/Migrations`

For example, to add a new migration from the root folder:

`dotnet ef migrations add "SampleMigration" --project src\Infrastructure --startup-project src\Web --output-dir Data\Migrations`

<h2 id="use-cases">Use-Cases</h2>

To enable Use-Cases ensure the [.NET template](https://www.nuget.org/packages/Clean.Architecture.Solution.Template) for Clean Architecture is installed. You can install it by running the following command
```
dotnet new install Clean.Architecture.Solution.Template::8.0.5
```

You can create use cases (commands or queries) by navigating to `./src/Application` and running `dotnet new ca-usecase`. Here are some examples:

To create a new command:
```bash
dotnet new ca-usecase --name CreateTodoList --feature-name TodoLists --usecase-type command --return-type int
```

To create a query:
```bash
dotnet new ca-usecase -n GetTodos -fn TodoLists -ut query -rt TodosVm
```

To learn more, run the following command:
```bash
dotnet new ca-usecase --help
```

<h2 id="deployment">Deployment</h2>

This project includes a full CI/CD pipeline.
The pipeline is responsible for building, testing, publishing and deploying the solution to Azure.

<h2 id="technologies">Technologies</h2>

* [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [FluentValidation](https://fluentvalidation.net/)
* [XUnit](https://xunit.net/), [FluentAssertions](https://fluentassertions.com/) & [Moq](https://github.com/moq)