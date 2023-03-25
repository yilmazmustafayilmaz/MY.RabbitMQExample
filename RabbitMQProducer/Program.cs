using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

Product product = new Product() { Id = 1, Name = "Book", Description = "This is a Book" };

var factory = new ConnectionFactory() { Uri = new Uri("amqps://lumyzppt:AEBg0q_ZsuhKPOWccUJL4mPMyl4Ird8J@crow.rmq.cloudamqp.com/lumyzppt") };
using var connection = factory.CreateConnection();
using (IModel channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "Example", durable: false, exclusive: false, autoDelete: false, arguments: null);

    string message = JsonConvert.SerializeObject(product);
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange: "", routingKey: "Example", basicProperties: null, body: body);

    Console.WriteLine($"{product.Name}");
}
Console.WriteLine("Mesajınız teslim edildi..");
Console.ReadLine();

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
