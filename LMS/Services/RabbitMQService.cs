// Services/MessageQueueService.cs
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace LMS.Services
{
    public class MessageQueueService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageQueueService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" }; // Change if using a different host
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "lead_assignments",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _channel.QueueDeclare(queue: "email_notifications",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _channel.QueueDeclare(queue: "activity_logs",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        public void SendLeadAssignmentNotification(string message)
        {
            SendMessage("lead_assignments", message);
        }

        public void SendEmailNotification(string message)
        {
            SendMessage("email_notifications", message);
        }

        public void SendActivityLog(string message)
        {
            SendMessage("activity_logs", message);
        }

        private void SendMessage(string queueName, string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                  routingKey: queueName,
                                  basicProperties: null,
                                  body: body);
        }
    }
}
