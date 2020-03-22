using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;

namespace DownloadSheet1
{
    class Program
    {
        static void Main(string[] args)
            {
            UserCredential credential;

            // If modifying these scopes, delete previously saved token.json
            string[] Scopes = { DriveService.Scope.DriveReadonly };
            string ApplicationName = "########";
            string fileId = "########";
            string path = @"########"; //Include filename & extension i.e. C:\stuff\sheet001.xlsx
            var jetStream = new System.IO.MemoryStream();

            using (var stream =
                    new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
            
            FilesResource.ExportRequest request = new FilesResource.ExportRequest(service, fileId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case Google.Apis.Download.DownloadStatus.Downloading:
                        {
                            Console.WriteLine(progress.BytesDownloaded);
                            break;
                        }
                    case Google.Apis.Download.DownloadStatus.Completed:
                        {
                            Console.WriteLine("Download complete.");
                            using (System.IO.FileStream file = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                            {
                                jetStream.WriteTo(file);
                            }
                            break;
                        }
                    case Google.Apis.Download.DownloadStatus.Failed:
                        {
                            Console.WriteLine("Download failed.");
                            break;
                        }
                }
            };

            request.DownloadWithStatus(jetStream);
        }
    }
}
