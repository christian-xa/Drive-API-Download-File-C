# Drive-API-Download-Sheet-C#
This C# code uses the .NET Framework, and will download an excel sheet from Google Drive to the specified location.

NOTE*** To change the file type you are downloading, change the MIME Type in "FilesResource.ExportRequest request"
Also, you should probably change the namespace

Requirements:

1.
Turn on the Drive API and Download Credentials

Follow this[1] guide to create a Google developers project and enable OAuth consent.
Download the client configuration file and SAVE THE FILE credentials.json TO YOUR WORKING DIRECTORY.


2.
Create a new .NET Framework project and install the following NuGet Packages:
Newtonsoft.Json
Google.Apis.Drive.v3
Google.Apis.Core
Google.Apis.Auth
Google.Apis

3.
Use "program.cs" in place of your own. 
Replace the following variables:
ApplicationName
fileId
path
