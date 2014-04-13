namespace Common
{
    using System.ServiceModel;

    /// <summary>
    /// The ISimulationServer interface.
    /// </summary>
    [ServiceContract(Name = "ISimulationServer", Namespace = "http://rolandkru/rwwa")]
    public interface ISimulationServer
    {
        /// <summary>
        /// The Command operation.
        /// </summary>
        /// <param name="command">The command from the bus.</param>
        [OperationContract(IsOneWay = false)]
        void ExecuteCommand(Command command);
    }
}