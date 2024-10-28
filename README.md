
<h1 align="center">
  Auto-Field Translation Thought Experiment
  <br>
</h1>

<p align="center">
    <a href="#overview">Route Overview</a> •
    <a href="#architecture">Architecture</a> •
    <a href="#how-to-run">How To Run</a> •
    <a href="#local-env">Local Environment</a> •
    <a href="#technologies">Technologies</a>
</p>

<h2 id="overview">Route Overview</h2>
For a extensive list of available API routes, please refer to the [API Documentation Wiki](https://github.com/Dominicod/AutoFieldTranslationExperiment/wiki/API-Documentation).

This application also does support Swagger, which can be accessed at the root of the application when in development.
(e.g. `https://localhost:7101/swagger`)

<h2 id="architecture">Architecture</h2>

This project is based on a semi-traditional "N-Layer" architecture. The project is divided into the following layers:
- **Web**: This layer contains all application/client facing logic (DTOs, Services, Controllers, Ect...).
- **Domain**: This layer contains all domain logic. (Entities, Value Objects, Ect...).
- **Infrastructure**: This layer contains all infrastructure logic. (Data Access, External Services, Ect...).

<h2 id="how-to-run">How To Run</h2>

To clone and run this application,
you'll need [Git](https://git-scm.com) and [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
installed on your computer.
From your command line:

```bash
# Clone this repository
$ git clone https://github.com/Dominicod/AutoFieldTranslationExperiment.git

# Go into the repository
$ cd AutoFieldTranslationExperiment/src/Web

# Run the app
$ dotnet run
```

<h2 id="local-env">Local Environment</h2>

A local environment can be set up by creating a `.secrets.json` file in the Web project.
The file should contain the following:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tcp:{YOUR_SERVER_NAME}.database.windows.net,1433;Initial Catalog={YOUR_CATALOG};Persist Security Info=False;User ID={YOUR_USER_ID};Password={YOUR_PASSWORD};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  },
  "Keys": {
    "Azure": {
      "AIService": "{YOUR_AZURE_TRANSLATION_SERVICE_KEY}"
    }
  },
  "AzureRegions": {
    "AIService": "{YOUR_AZURE_TRANSLATION_SERVICE_REGION}"
  }
}
```

These secrets are all required for the project to function, and if they are missing an exception will be thrown.

### Migrations

Running database migrations is easy. Ensure you add the following flags to your command (values assume you are executing from repository root)

* `--project src/Infrastructure`
* `--startup-project src/Web`
* `--output-dir Migrations`

For example, to add a new migration from the root folder:

`dotnet ef migrations add "SampleMigration" --project src\Infrastructure --startup-project src\Web --output-dir Migrations`

<h2 id="technologies">Technologies</h2>

* [Azure Translator Text API](https://azure.microsoft.com/en-us/products/ai-services/ai-translator)
* [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
* [FluentValidation](https://fluentvalidation.net/)
