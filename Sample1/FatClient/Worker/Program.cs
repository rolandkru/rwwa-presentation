// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Worker
{
    using System;
    using System.Threading;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Auth;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Microsoft.WindowsAzure.Storage.Queue;

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main entry point.
        /// </summary>
        /// <param name="args">
        /// The command line arguments. Not used.
        /// </param>
        private static void Main(string[] args)
        {
            var credentials = new StorageCredentials("rwwa", "vyMfaSPxURHXZaIhhFJQRg5ZLEN6qDj4yU78r3oeOH+pZzdcf4S86QvGAsB6L8JaPti9qJbB929hy1Y9hipFmw==");

            // Retrieve storage account from connection string.
            var storageAccount = new CloudStorageAccount(credentials, true);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue
            CloudQueue queue = queueClient.GetQueueReference("dataqueue");
            queue.CreateIfNotExists();

            Console.WriteLine("Up and listening to the queue.");

            while (true)
            {
                CloudQueueMessage message;
                do
                {
                    message = queue.GetMessage();
                    if (message != null)
                    {
                        Console.WriteLine("Received Message for BLOB " + message.AsString);
                        var blobUrl = message.AsString;
                        var blockBlob = new CloudBlockBlob(new Uri(blobUrl), credentials);
                        if (blockBlob.Exists())
                        {
                            // TODO: download and process BLOB 
                            /*using (var fileStream = System.IO.File.OpenWrite(@"path\myfile"))
                            {
                                blockBlob.DownloadToStream(fileStream);
                            }*/
                       
                            blockBlob.Delete();
                        }

                        queue.DeleteMessage(message);
                        Console.WriteLine("BLOB " + message.AsString + " and queue message deleted.");
                    }
                }
                while (message != null);
    
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }
    }
}