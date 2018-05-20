# AzureServiceBusSample

Sample console application for reading & writing to Azure Service Bus:
- Queues
- Topics

## Setup
1. Create a Service Bus in Azure
2. Create a Queue called dbqueue
3. Create a Topic called dbqueue
4. Create a Shared Access Policy for the Service Bus giving Manage access
5. Copy the Connection string for the Shared Access Policy to the _conn variable in the application
6. In the Topic, create a Subscription called ConsoleApp