using System;
using System.Net.Http;
using System.Threading.Tasks;

/// <summary>
/// Interface for communication with the API
/// </summary>
public interface IApi
{
    Uri Url { get; }

    /// <summary>
    /// Post to the api
    /// </summary>
    /// <param name="extension">The extension for this request</param>
    /// <param name="body">The body of this request</param>
    /// <param name="callback">The callback for when the data was received</param>
    void Post(string extension, string body, Action<string, bool> callback);

    /// <summary>
    /// Get request to the api
    /// </summary>
    /// <param name="extension">The extension for this request</param>
    /// <param name="callback">The callback for when the data was received</param>
    void Get(string extension, Action<string, bool> callback);

    /// <summary>
    /// Get request to the api.
    /// The response will not be handled by the dispatcher. 
    /// Use this only when you want to handle this outside of the main thread. 
    /// </summary>
    /// <param name="extension">The extension for this request</param>
    Task<HttpResponseMessage> GetAsync(string extension);
}
