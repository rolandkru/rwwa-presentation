
//// From: http://social.msdn.microsoft.com/Forums/windowsazure/en-US/02278eee-e845-421a-a025-4c48e1d7ae13/is-there-a-data-contract-or-schema-for-the-xml-message-posted-by-azure-scheduler-in-the-storage?forum=azurescheduler

namespace Scheduler
{
    using System.Runtime.Serialization;

    /// <summary>
    ///     storage queue message
    /// </summary>
    [DataContract]
    public sealed class StorageQueueMessage
    {
        /// <summary>
        ///     Gets or sets the ETag
        /// </summary>
        public string ExecutionTag { get; set; }

        /// <summary>
        ///     Gets or sets the Client Request ID
        /// </summary>
        public string ClientRequestId { get; set; }

        /// <summary>
        ///     Gets or sets the Expected executionTime
        /// </summary>
        public string ExpectedExecutionTime { get; set; }

        /// <summary>
        ///     Gets or sets the Scheduler Job ID
        /// </summary>
        public string SchedulerJobId { get; set; }

        /// <summary>
        ///     Gets or sets the Scheduler JobCollection ID
        /// </summary>
        public string SchedulerJobCollectionId { get; set; }

        /// <summary>
        ///     Gets or sets the Region
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        ///     Gets or sets the Message
        /// </summary>
        public string Message { get; set; }
    }
}