namespace Client
{
    using System;
    using System.ServiceModel;
    using System.Threading;

    using Common;

    /// <summary>
    ///     The service bus relay client. 
    /// </summary>
    internal class RobotClient
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">The args.</param>
        private static void Main(string[] args)
        {
            var channelFactory = new ChannelFactory<ISimulationServerChannel>("SimulationServer");
            ISimulationServerChannel channel = channelFactory.CreateChannel();
            channel.Open();

            Console.WriteLine("Press any key to exit");

            var rnd = new Random();

            while (!Console.KeyAvailable)
            {
                var command = (Command)rnd.Next(0, 3);
                channel.ExecuteCommand(command);
                Console.WriteLine("Execute Command '{0}'", command);
                Thread.Sleep(1000);
            }

            channel.Close();
            channelFactory.Close();
        }
    }
}