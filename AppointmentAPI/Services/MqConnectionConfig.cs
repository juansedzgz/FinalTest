using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentAPI.Services
{
    public abstract class MqConnectionConfig
    {
        readonly IConnection _connection;
        protected readonly IModel Channel;

        // Constructor
        protected MqConnectionConfig(ConnectionFactory factory)
        {

            // Establecer las credenciales de conexión
            factory.UserName = "nkuewxof";
            factory.Password = "050T_Ziwo-Zfd6deckFmHHlUJEPXJKI9";
            factory.HostName = "chimpanzee-01.rmq.cloudamqp.com";
            factory.VirtualHost = "nkuewxof";
            factory.Port = AmqpTcpEndpoint.UseDefaultPort; // 5672

            _connection = factory.CreateConnection();
            Channel = _connection.CreateModel();
        }

        protected void CreateQueueIfNotExists(string queueName)
        {
            Channel.QueueDeclare(queueName, autoDelete: false, exclusive: false, durable: true, arguments: new Dictionary<string, object>());
        }

        // Destructor
        ~MqConnectionConfig()
        {
            _connection.Dispose();
            Channel.Dispose();
        }
    }
}