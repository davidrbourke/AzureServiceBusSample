using Microsoft.ServiceBus.Messaging;
using System;

namespace AzureServiceBusSample
{
    class Program
    {
        private static string _conn = "<Endpoint connection string>";
        private static string _queueEntityPath = ";EntityPath=dbqueue";
        private static QueueClient _client = QueueClient.CreateFromConnectionString($"{_conn}{_queueEntityPath}");
        private static string _topic = "dbtopic";
        private static string _subscriptionName = "ConsoleApp";

        static void Main(string[] args)
        {
            SendMessage("poop");
            ReadMessage();

            SendTopicMessage("blip");
            ReadTopicMessage();

            Console.ReadKey();
        }

        static void SendMessage(string msg)
        {
            var message = new BrokeredMessage(msg);
            _client.Send(message);
        }

        static void ReadMessage()
        {
            var options = new OnMessageOptions
            {
                AutoComplete = false
            };

            // Use a Brokered Message callback
            _client.OnMessage((m) =>
            {
                try
                {
                    var msg = m.GetBody<string>();

                    if (msg == "poop")
                    {
                        Console.WriteLine(msg);

                        // Calling Complete() takes the message out of the queue after the expiration date
                        // Not calling Complete(), the message will reappear in the queue after TTL
                        // Or use the AutoComplete = true in the OnMessageOptions
                        m.Complete();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }, options);
        }

        static void SendTopicMessage(string message)
        {
            var topicClient = TopicClient.CreateFromConnectionString(_conn, _topic);
            var msg = new BrokeredMessage(message);
            topicClient.Send(msg);
        }

        static void ReadTopicMessage()
        {
            var subClient = SubscriptionClient.CreateFromConnectionString(_conn, _topic, _subscriptionName);
            subClient.OnMessage(m =>
            {
                Console.WriteLine(m.GetBody<string>());
            });
        }
    }
}
