using RabbitMQ.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_ntf
{
    internal class Configure_RabbitMQ
    {
        private config cfg;
        public Configure_RabbitMQ(config cfg)
        {
            this.cfg = cfg;
        }
        public void sendExchange(string exchange, string message)
        {
            try
            {
                var HostName = cfg.RabbitMQ.Hostname;
                var factory = new ConnectionFactory() { Port = cfg.RabbitMQ.Port };
                if (!string.IsNullOrEmpty(cfg.RabbitMQ.user) && !string.IsNullOrEmpty(cfg.RabbitMQ.pass))
                {
                    factory.UserName = cfg.RabbitMQ.user;
                    factory.Password = cfg.RabbitMQ.pass;
                }
                using (var connection = factory.CreateConnection(HostName))
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: exchange,
                                         durable: true,
                                         type: "direct",
                                         autoDelete: false,
                                         arguments: null);
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: exchange,
                                         routingKey: "",
                                         basicProperties: null,
                                         body: body);
                    channel.BasicQos(0, 10, false);
                }
            }
            catch (Exception e)
            {
                Log.Error($"sendExchange: {e.ToString()}");
            }
        }
    }
}
