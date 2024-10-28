using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace LMS.Services
{
    public class MessageConsumerService : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageConsumerService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() => ConsumeLeadAssignments(stoppingToken));
            Task.Run(() => ConsumeEmailNotifications(stoppingToken));
            Task.Run(() => ConsumeActivityLogs(stoppingToken));
            return Task.CompletedTask;
        }

        private void ConsumeLeadAssignments(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                // Handle the message (e.g., notify the agent)
                System.Diagnostics.Debug.WriteLine($"Lead Assignment: {message}");
            };

            _channel.BasicConsume(queue: "lead_assignments", autoAck: true, consumer: consumer);
        }

        private void ConsumeEmailNotifications(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                // Handle the message (e.g., send an email)
                System.Diagnostics.Debug.WriteLine($"Email Notification: {message}");
            };

            _channel.BasicConsume(queue: "email_notifications", autoAck: true, consumer: consumer);
        }

        private void ConsumeActivityLogs(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                // Handle the message (e.g., log the activity)
                System.Diagnostics.Debug.WriteLine($"Activity Log: {message}");
            };

            _channel.BasicConsume(queue: "activity_logs", autoAck: true, consumer: consumer);
        }
    }
}

