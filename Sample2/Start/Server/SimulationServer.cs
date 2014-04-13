namespace Server
{
    using System;
    using System.ServiceModel;

    using Common;

    /// <summary>
    /// The simulation server.
    /// </summary>
    [ServiceBehavior(Name = "ISimulationServer", Namespace = "http://rolandkru/rwwa")]
    public class SimulationServer : ISimulationServer
    {
        /// <summary>
        /// The execute command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void ExecuteCommand(Command command)
        {
            Console.WriteLine("Executing command '{0}'", command);
        }
    }
}