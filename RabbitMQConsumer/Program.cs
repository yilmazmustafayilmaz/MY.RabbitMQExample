using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { Uri = new Uri("amqps://lumyzppt:AEBg0q_ZsuhKPOWccUJL4mPMyl4Ird8J@crow.rmq.cloudamqp.com/lumyzppt") };
using var connection = factory.CreateConnection();
using (IModel channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "Example", durable: false, exclusive: false, autoDelete: false, arguments: null);

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, bdea) =>
    {
        var body = bdea.Body;
        var message = Encoding.UTF8.GetString(body.ToArray());
        Product product = JsonConvert.DeserializeObject<Product>(message);
        Console.WriteLine($"{product.Name} - {product.Description}");
    };
    channel.BasicConsume(queue: "Example", autoAck:true ,consumer: consumer);
    Console.WriteLine("Mesaj teslim alındı..");
    Console.ReadLine();
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
