using Mobile_IP.Models;
using MQTTnet;
using MQTTnet.Client;
using Nancy.Json;

namespace Mobile_IP.ViewModels;

public class HomeViewModel
{
    private readonly Backend backend = new Backend();
    private readonly MqttFactory mqttFactory = new MqttFactory();

    public DateVitale dateVitale;

    public HomeViewModel()
    {
        StartMqttBroker();
    }

    public DateVitale DateVitale { get => dateVitale; }

    private async Task StartMqttBroker()
    {
        using (var mqttClient = mqttFactory.CreateMqttClient())
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithClientId(Guid.NewGuid().ToString())
                .WithTcpServer("ws://test.mosquitto.org:8080/mqtt")
                .Build();
            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                dateVitale = new JavaScriptSerializer().Deserialize<DateVitale>(e.ApplicationMessage.ContentType);
                return Task.CompletedTask;
            };

            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f =>
                    {
                        f.WithTopic("careband_iot_topic");
                    })
                .Build();
            var response = await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }
    }
}
