using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace AppointmentAPI.Services
{
    public class MqProducer: MqConnectionConfig
    {
        public MqProducer(ConnectionFactory factory) : base(factory) {}

        public void SendMessage(string queueName, byte[] message)
        {
            Debug.WriteLine("ESTOY CREANDO LA COLA");
            CreateQueueIfNotExists(queueName);
            // Envío de mensajes
            Channel.BasicPublish(
                string.Empty,
                queueName,
                basicProperties: null,
                mandatory: true,
                body: message);
        }
    }
}