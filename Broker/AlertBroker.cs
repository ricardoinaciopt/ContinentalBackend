using System.Text;
using ContinentalBackend.Models;
using System.Threading.Channels;
using RabbitMQ.Client;

namespace ContinentalBackend.Broker
{
    public class AlertBroker
    {
        private RabbitMQ.Client.IModel _channel;

        public AlertBroker() 
        {

            var factory = new ConnectionFactory { HostName = "localhost" };

            _channel = factory.CreateConnection().CreateModel();

            _channel.ExchangeDeclare(exchange: "alertas", type: ExchangeType.Topic);
        }


        public void SendAlerta(string rkey, string alerta)    //recebe a routing key e o alerta em formato json
        {
            var body = Encoding.UTF8.GetBytes(alerta);

            //Enviar a mensagem codificada em UTF8 para o exchange Alertas
            _channel.BasicPublish(exchange: "alertas",
                             routingKey: rkey,
                             basicProperties: null,
                             body: body);
            Console.WriteLine($" [x] Enviado '{rkey}':'{alerta}'");
           
        }
       

    }
}
