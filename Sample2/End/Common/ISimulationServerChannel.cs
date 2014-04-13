namespace Common
{
    using System.ServiceModel;

    /// <summary>
    /// The ISimulationServerChannel interface.
    /// </summary>
    public interface ISimulationServerChannel : ISimulationServer, IClientChannel
    {
    }
}