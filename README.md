## Sqlite Demo App
A demo app for building ASP.Net core WebApi apps using entity framework core 2.1 with SQLite database

### Build-Status

| Branch        | Build Status | 
| ------------- |:-------------:|
| master      | [![Build status](https://rajivyanamandra.visualstudio.com/SqliteDemoApp/_apis/build/status/SqliteDemoApp-ASP.NET%20Core-CI)](https://rajivyanamandra.visualstudio.com/SqliteDemoApp/_apis/build/status/SqliteDemoApp-ASP.NET%20Core-CI?branchName=master) |

## Purpose for building this project
TODO:// Add this

### Project Layout

The project layout has been modeled based on the onion architecture as outlined here at [.NET application architecture from Microsoft](https://docs.microsoft.com/en-us/dotnet/standard/modernize-with-azure-and-containers/index)

| Project        | Project Type|Description | 
| :------------- |:-------------|:-------------|
|Sqlite.Core|Class Library|Is the Application Core component|
|Sqlite.Infrastructure|Class Library|is where runtime implementations are defined|
|Sqlite.WebUi|ASP.Net Core WebApi|is the UI component|
|Sqlite.Tests|Class Library|is the UI component|

#### Sqlite.Core
TODO:// Add this

#### Sqlite.Infrastructure
TODO:// Add this

#### Sqlite.WebUi
TODO:// Add this

#### Sqlite.Tests
TODO:// Add this

### Continous Integration
I have set-up the continous integration on `master` branch using Visual Studio Team Services. After each commit or pull request merge on the master the CI build would kick-off the build process.

The build status badge shows the build output as shown here at Build-Status
