using Azure.Messaging.ServiceBus;
using Receiver.Entities;

const string topicName =  "<Topic-Name>";
const string subscriptionName =  "<Subscription-Name>";
const string connectionString = "<Connection-String>";

var client = new ServiceBusClient(connectionString);
var receiver = client.CreateProcessor(topicName, subscriptionName, new ServiceBusProcessorOptions());

try
{
    receiver.ProcessMessageAsync += MessageHandler;
    receiver.ProcessErrorAsync += ErrorHandler;

    Console.WriteLine("Starting the receiver one...");
    await receiver.StartProcessingAsync();

    Console.ReadKey();

    Console.WriteLine("\nStopping the receiver...");
    await receiver.StopProcessingAsync();
    Console.WriteLine("Stopped receiving messages");
}
finally
{
    await receiver.DisposeAsync();
    await client.DisposeAsync();
}

Task ErrorHandler(ProcessErrorEventArgs arg)
{
    Console.WriteLine(arg.Exception.ToString());
    return Task.CompletedTask;
}

Task MessageHandler(ProcessMessageEventArgs arg)
{
    string body = arg.Message.Body.ToString();
    var checkout = Checkout.DeserializeEntity(body);

    Console.WriteLine("\nCheckout received:");
    Console.WriteLine($"Amount: ${checkout.Amount}");

    Console.WriteLine("\nItens:");

    foreach(var product in checkout.Products)
        Console.WriteLine($"{product.Name} - ${product.Value}");

    return Task.CompletedTask;
}