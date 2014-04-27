namespace ClientLog
{
    using log4net.Appender;
    using log4net.Core;

    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// The table storage appender.
    /// </summary>
    public class TableStorageAppender : AppenderSkeleton
    {
        /// <summary>
        /// The table.
        /// </summary>
        private CloudTable table;

        /// <summary>
        /// The activate options.
        /// </summary>
        public override void ActivateOptions()
        {
            base.ActivateOptions();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            this.table = tableClient.GetTableReference("ClientLogs");
            this.table.CreateIfNotExists();
        }

        /// <summary>
        /// The append.
        /// </summary>
        /// <param name="loggingEvent">
        /// The logging event.
        /// </param>
        protected override void Append(LoggingEvent loggingEvent)
        {          
            var entity = new LogEventEntity
                            {
                                Message = loggingEvent.RenderedMessage, 
                                Level = loggingEvent.Level.Name, 
                                LoggerName = loggingEvent.LoggerName, 
                                Domain = loggingEvent.Domain,
                                ThreadName = loggingEvent.ThreadName,
                                Identity = loggingEvent.Identity
                            };

            // Create the TableOperation that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(entity);

            // Execute the insert operation.
            this.table.ExecuteAsync(insertOperation);
         }
    }
}