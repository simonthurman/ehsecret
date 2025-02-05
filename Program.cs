﻿﻿using System.Text;
using Azure.Messaging.EventHubs.Consumer;

var connectionString = "Endpoint=sb://delmerg.servicebus.windows.net/;SharedAccessKeyName=newpolict;SharedAccessKey=vO15Hgg2T6yn5Qe6gbFFoyIwV0XO9U4eh+AEhLifpeg=";
var eventHubName = "api-usage";

string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

Console.WriteLine("Starting to read events from the Event Hub...");

await using (var consumer = new EventHubConsumerClient(consumerGroup, connectionString, eventHubName))
{
    using var cancellationSource = new CancellationTokenSource();
    cancellationSource.CancelAfter(TimeSpan.FromSeconds(4555));

    await foreach (PartitionEvent receivedEvent in consumer.ReadEventsAsync(cancellationSource.Token))
    {
        //write out even
        Console.WriteLine($"Message received on partition {receivedEvent.Partition.PartitionId}: {Encoding.UTF8.GetString(receivedEvent.Data.Body.ToArray())}");
    }
}      