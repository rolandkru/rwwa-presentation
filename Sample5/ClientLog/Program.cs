// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ClientLog
{
    using System;

    using log4net;

    /// <summary>
    /// The program.
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
            log4net.Config.XmlConfigurator.Configure();

            var logger = LogManager.GetLogger("root");
            logger.Fatal("Fatal error");

            Console.WriteLine("Fatal error logged");
            Console.ReadLine();
        }
    }
}