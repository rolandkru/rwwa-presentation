// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scheduler
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Xml.Serialization;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Auth;
    using Microsoft.WindowsAzure.Storage.Queue;

    /// <summary>
    ///     The program.
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
            var credentials = new StorageCredentials("rwatest1", "HygcCw5dUGZQdTBCX5IXhbZOvWG72at9YeIBBV+XCLFUcbe2/63VDJzRd8FBpbnhj46+VjnFdkfMl3wLhA0mtA==");

            // Retrieve storage account from connection string.
            var storageAccount = new CloudStorageAccount(credentials, true);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue
            CloudQueue queue = queueClient.GetQueueReference("dunning");

            Console.WriteLine("Up and listening to the queue.");

            while (true)
            {
                CloudQueueMessage msg;
                do
                {
                    msg = queue.GetMessage();

                    if (msg != null)
                    {
                        var dcs = new XmlSerializer(typeof(StorageQueueMessage));

                        using (var xmlstream = new MemoryStream(Encoding.Unicode.GetBytes(msg.AsString)))
                        {
                            var message = (StorageQueueMessage)dcs.Deserialize(xmlstream);
                            Console.WriteLine("Received Message: " + message.Message);
                        }

                        queue.DeleteMessage(msg);
                    }
                }
                while (msg != null);

                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }
    }
}