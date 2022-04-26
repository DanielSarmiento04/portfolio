using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; // Install Newtonsoft.Json with NuGet
using PortFolio.Keys;

namespace PortFolio;
public class MicrosoftTranslator
{
    private readonly string key = MicrsofotTranslatorKey.key;
    private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";

    // Add your location, also known as region. The default is global.
    // This is required if using a Cognitive Services resource.
    private static readonly string location = "eastus2";

    public async Task<string> Translate(string textToTranslate)
    {
        // Input and output languages are defined as parameters.
        string route = "/translate?api-version=3.0&from=en&to=de&to=es";
        object[] body = new object[] { new { Text = textToTranslate } };
        var requestBody = JsonConvert.SerializeObject(body);

        using (var client = new HttpClient())
        using (var request = new HttpRequestMessage())
        {
            // Build the request.
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(endpoint + route);
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            request.Headers.Add("Ocp-Apim-Subscription-Key", key);
            request.Headers.Add("Ocp-Apim-Subscription-Region", location);

            // Send the request and get response.
            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
            // Read response as a string.
            string result = await response.Content.ReadAsStringAsync();
            var traducciones = JsonConvert.DeserializeObject<List<Translator>>(result);
            return traducciones[0]?.translations[0].text;
        }
    }
    
    public class Translator
    {
        public List<Traduccion> translations { get; set; }
    }
    public class Traduccion
    {
        public string text { get; set; }
        public string to { get; set; }
    }
}