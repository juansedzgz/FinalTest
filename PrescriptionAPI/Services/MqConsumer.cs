using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Web;
using PrescriptionAPI.Services;

namespace PrescriptionAPI.Services
{
    public class MqConsumer : MqConnectionConfig
    {
        readonly EventingBasicConsumer _consumer;

        public MqConsumer(ConnectionFactory factory) : base(factory)
        {
            _consumer = new EventingBasicConsumer(Channel);
        }

        public void AddListener(string queueName, EventHandler<BasicDeliverEventArgs> handler)
        {
            _consumer.Received += handler;
            CreateQueueIfNotExists(queueName);
            Channel.BasicConsume(queue: queueName, autoAck: true, consumer: _consumer, noLocal: false, exclusive: false, consumerTag: Guid.NewGuid().ToString(), arguments: new Dictionary<string, object>());
        }

        public void RemoveListener(EventHandler<BasicDeliverEventArgs> handler)
        {
            _consumer.Received -= handler;
        }
    }
}