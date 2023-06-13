using Nancy.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Mobile_IP.Models;

public class Backend
{
    private HttpClient httpClient;
    private HttpResponseMessage response;

    private string BaseUri { get => "http://34.140.195.43:80/"; }

    public Backend()
    {
        httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(BaseUri);
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public HttpResponseMessage Response { get => response; }

    public async void PostJsonAndGetResponseAsync<Key, Value>(string requestUri, Dictionary<Key, Value> values) =>
        response = await httpClient.PostAsJsonAsync(requestUri, values);

    public T DeserializeResponse<T>() =>
        new JavaScriptSerializer().Deserialize<T>(Response.Content.ReadAsStringAsync().Result);

    public void AddRequestHeader(string name, string value) =>
        httpClient.DefaultRequestHeaders.Add(name, "Bearer " + value);

    public bool IsResponseStatusCodeOk() => Response.StatusCode == HttpStatusCode.OK;
}
