using Azure.Messaging.ServiceBus;
using Sender.Entities;

const string topicName =  "<Topic-Name>";
const string connectionString = "<Connection-String>";

var client = new ServiceBusClient(connectionString);
var sender = client.CreateSender(topicName);

IList<Product> products = new List<Product>
{
    new Product(Guid.NewGuid(), "Chocolate Cake", 29.99),
    new Product(Guid.NewGuid(), "Soda", 10.00),
    new Product(Guid.NewGuid(), "Banana", 2.99),    
};

var checkout = new Checkout(products);

try
{
    await sender.SendMessageAsync(new ServiceBusMessage(checkout.SerializeEntity()));
    Console.WriteLine($"Checkout Sent");
}
finally
{
    await sender.DisposeAsync();
    await client.DisposeAsync();
}
