Create A New .net cs Project
PS C:\Deepak\Projects\CSharp-DemoProject> mkdir BlobStorageExample
   
PS C:\Deepak\Projects\CSharp-DemoProject> cd BlobStorageExample
PS C:\Deepak\Projects\CSharp-DemoProject\BlobStorageExample> dotnet new console
if you dont have dotnet run following command 
Download a .NET SDK:
https://aka.ms/dotnet/download

dotnet add package Azure.Storage.Blobs --version 12.23.0

PS C:\Users\deepa\AppData\Roaming\NuGet> dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org
Package source with Name: nuget.org added successfully.
PS C:\Users\deepa\AppData\Roaming\NuGet> dotnet clean
MSBUILD : error MSB1003: Specify a project or solution file. The current working directory does not contain a project or solution file.
PS C:\Users\deepa\AppData\Roaming\NuGet> dotnet build
MSBUILD : error MSB1003: Specify a project or solution file. The current working directory does not contain a project or solution file.
PS C:\Users\deepa\AppData\Roaming\NuGet>

