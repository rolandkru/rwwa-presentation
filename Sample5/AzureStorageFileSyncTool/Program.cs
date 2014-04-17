// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   This program synchronizes backup files from blob storage to the local file system.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SQLAzureBackupFileSyncTool
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Auth;
    using Microsoft.WindowsAzure.Storage.Blob;

    /// <summary>
    ///     This program synchronizes backup files from blob storage to the local file system.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Sync Started.");
                Console.WriteLine("Account: {0}", ConfigurationManager.AppSettings["AccountName"]);
                Console.WriteLine("Container: {0}", ConfigurationManager.AppSettings["ContainerName"]);
                Console.WriteLine("Local Drive: {0}", ConfigurationManager.AppSettings["LocalDirectory"]);

                var storageCredentials = new StorageCredentials(ConfigurationManager.AppSettings["AccountName"], ConfigurationManager.AppSettings["AccountKey"]);

                var storageAccount = new CloudStorageAccount(storageCredentials, false);
                var blobClient = storageAccount.CreateCloudBlobClient();

                var blobContainer = blobClient.GetContainerReference(ConfigurationManager.AppSettings["ContainerName"]);
                blobContainer.CreateIfNotExists();

                var localDirectory = new DirectoryInfo(ConfigurationManager.AppSettings["LocalDirectory"]);

                var blobList = blobContainer.ListBlobs(null, true).ToList();
                var localList = localDirectory.EnumerateFiles("*.*", SearchOption.AllDirectories).ToList();

                foreach (var localFile in localList)
                {
                    bool upload = true;

                    foreach (var blob in blobList.Select(b => b as ICloudBlob))
                    {
                        if (blob.Name.Equals(localFile.Name, StringComparison.Ordinal))
                        {
                            upload = false;
                            break;
                        }
                    }

                    if (upload)
                    {
                        var blockBlob = blobContainer.GetBlockBlobReference(localFile.Name);
                        using (var fileStream = File.OpenRead(localFile.FullName))
                        {
                            blockBlob.UploadFromStream(fileStream);
                        }
                    }
                }

                foreach (var blob in blobList.Select(b => b as ICloudBlob))
                {
                    bool delete = true;

                    foreach (var localFile in localList)
                    {
                        if (blob.Name.Equals(localFile.Name, StringComparison.Ordinal))
                        {
                            delete = false;
                            break;
                        }
                    }

                    if (delete)
                    {
                        blob.Delete();
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }
            finally
            {
                Console.WriteLine("Sync Finished.");
                Console.ReadLine();
            }
        }
    }
}