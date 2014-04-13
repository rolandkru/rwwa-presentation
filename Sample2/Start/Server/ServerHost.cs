namespace Server
{
    using System;
    using System.ServiceModel;

    /// <summary>
    /// The service bus relay server.
    /// </summary>
    internal class ServerHost
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">The args.</param>
        private static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(SimulationServer));
            host.Open();

            Console.WriteLine("Press [Enter] to exit");
            Console.ReadLine();

            host.Close();
        }
    }
}