// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Selectron Systems AG">
//   Copyright (c) Selectron Systems AG. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace CORSTool
{
    using System;
    using System.Collections.Generic;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Shared.Protocol;

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=rwwa;AccountKey=vyMfaSPxURHXZaIhhFJQRg5ZLEN6qDj4yU78r3oeOH+pZzdcf4S86QvGAsB6L8JaPti9qJbB929hy1Y9hipFmw==");
                var blobClient = storageAccount.CreateCloudBlobClient();
                var serviceProperties = blobClient.GetServiceProperties();

                serviceProperties.Cors.CorsRules.Clear();

                serviceProperties.Cors.CorsRules.Add(
                new CorsRule
                {
                    AllowedHeaders = new List<string> { "*" },
                    AllowedMethods = CorsHttpMethods.Put | CorsHttpMethods.Get | CorsHttpMethods.Head,
                    AllowedOrigins = new List<string> { "*" },            ////This is the URL of our application.                                                 
                    MaxAgeInSeconds = 1 * 60 * 60,                                              ////Let the browswer cache it for an hour
                });

                blobClient.SetServiceProperties(serviceProperties);
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }

            Console.WriteLine("CORS Policies set!");
            Console.ReadLine();
        }
    }
}