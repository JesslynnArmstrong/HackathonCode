using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// A class that is used to make request to a specific API
/// </summary>
public class Api : IApi
{
    public Uri Url { get; }
    private readonly HttpClient webClient;

    /// <summary>
    /// Create an api
    /// </summary>
    /// <param name="url">The base url</param>
    public Api(string url)
    {
        Url = new Uri(url);
        webClient = new HttpClient();
    }

    /// <summary>
    /// Post to the api
    /// </summary>
    /// <param name="extension">The extension for this request</param>
    /// <param name="body">The body of this request</param>
    /// <param name="callback">The callback for when the data was received</param>
    /// <returns></returns>
    public async void Post(string extension, string body, Action<string, bool> callback)
    {
        if (extension is null)
            throw new ArgumentNullException(nameof(extension));

        if (body is null)
            throw new ArgumentNullException(nameof(body));

        string url = GetUrl(extension);
        StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
        try
        {
            HttpResponseMessage response = await webClient.PostAsync(url, content);
            CallDispatcher(response, callback);
        }
        catch (SocketException socketException)
        {
            HandleException(socketException, callback);
        }
    }

    /// <summary>
    /// Get request to the api the response will be returned as string
    /// </summary>
    /// <param name="extension">The extension for this request</param>
    /// <param name="callback">The callback for when the data was received</param>
    public async void Get(string extension, Action<string, bool> callback)
    {
        if (extension is null)
            throw new ArgumentNullException(nameof(extension));

        try
        {
            var response = await GetAsync(extension);
            CallDispatcher(response, callback);
        }
        catch (HttpRequestException socketException)
        {
            HandleException(socketException, callback);
        }
    }

    /// <summary>
    /// Get request to the api.
    /// The response will not be handled by the dispatcher.
    /// Use this only when you want to handle this outside of the main thread. 
    /// </summary>
    /// <param name="extension">The extension for this request</param>
    public async Task<HttpResponseMessage> GetAsync(string extension)
    {
        if (extension is null)
            throw new ArgumentNullException(nameof(extension));

        string url = GetUrl(extension);
        return await webClient.GetAsync(url);
    }

    /// <summary>
    /// Call the dispatcher instance
    /// </summary>
    /// <param name="response">The response we received</param>
    /// <param name="callback">The callback we want to call</param>
    private static async void CallDispatcher(HttpResponseMessage response, Action<string, bool> callback)
    {
        if (callback == null)
            return;

        string value = await response.Content.ReadAsStringAsync();
        Dispatcher.Instance.Invoke(() => callback(value, response.IsSuccessStatusCode));
    }

    /// <summary>
    /// Call the dispatcher to respond with an exception instead of a valid Response
    /// </summary>
    /// <param name="exception">The exception that was thrown</param>
    /// <param name="callback">The callback for who will receive the data from the exception</param>
    private static void HandleException(Exception exception, Action<string, bool> callback)
    {
        if (callback == null)
            return;

        Dispatcher.Instance.Invoke(() => callback(exception.Message, false));
    }

    /// <summary>
    /// Format the url with the extension
    /// </summary>
    /// <param name="extension">The extension you want to add to the base Url</param>
    /// <returns>The full url</returns>
    private string GetUrl(string extension)
    {
        return new Uri(Url, extension).ToString();
    }
}