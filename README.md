# License Manager

License Manager is a desktop/web application to manage software licenses: create, assign, track, revoke, and report on license usage.

## Key features
- Create and store license records
- Assign licenses to users or machines
- Track activation and expiration dates
- Export reports (CSV/PDF)
- Role-based access and audit logs

## Requirements
- Visual Studio 2022 or later (__Visual Studio 2022__)
- .NET 6.0 SDK or later (adjust to your project's target framework)
- Any required database (e.g., SQL Server, SQLite) — see project configuration

## Quick start
1. Open the solution in __Solution Explorer__.
2. Restore NuGet packages: __Tools > NuGet Package Manager > Package Manager Console__ then run:
   dotnet restore
3. Build the solution: __Build > Build Solution__ (or press Ctrl+Shift+B).
4. Run the project you need (F5 or __Debug > Start Debugging__).

## Configuration
- Connection strings and environment-specific settings are stored in appsettings.json (or in each project's configuration file). Update those before running against production data.

## Contributing
- Create feature branches from main.
- Follow existing code style and add unit tests for new functionality.
- Open PRs with a clear description and test steps.

## Contact
For questions or support, open an issue in the repository or contact the project maintainer.
