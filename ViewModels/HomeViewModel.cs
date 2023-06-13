using Mobile_IP.Models;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;

namespace Mobile_IP.ViewModels;

public class HomeViewModel
{
    private readonly Backend backend = new Backend();
    private readonly MqttFactory mqttFactory = new MqttFactory();

    public HomeViewModel()
    {
        StartMqttBroker();
    }

    private async Task StartMqttBroker()
    {
        using (var mqttClient = mqttFactory.CreateMqttClient())
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithClientId(Guid.NewGuid().ToString())
                .WithTcpServer("ws://test.mosquitto.org:8080/mqtt", 8080)
                .Build();
            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                Application.Current.MainPage.DisplayAlert("Alert", e.ApplicationMessage.ToString(), "OK");
                return Task.CompletedTask;
            };

            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f =>
                    {
                        f.WithTopic("careband_iot_topic");
                    })
                .Build();
            var response = await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

            await Application.Current.MainPage.DisplayAlert("Alert", "MQTT client subscribed to topic.", "OK");
        }
    }
}
