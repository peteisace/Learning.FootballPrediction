using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serialization.Json;

namespace Learning.FootballPrediction.DataFetch.Repositories
{
    public abstract class HttpRepositoryBase
    {
        private IMatchConfiguration _config;

        protected HttpRepositoryBase(IMatchConfiguration configuration)
        {
            this._config = configuration;
        }

        protected IMatchConfiguration Configuration => this._config;

        protected async Task<T> FetchOverHttp<T>(string sourceUrl, Method method = Method.GET, params object[] parameters)
        {
            // calculate the true URL.
            sourceUrl = string.Format(sourceUrl, parameters);

            // Create the client.
            var client = new RestClient(this._config.BaseUrl);
            client.ClearHandlers();
            client.AddHandler("application/json", () => new JsonDeserializer());

            // Set up the authentication.
            IRestRequest request = new RestRequest(sourceUrl, method);
            request = request.AddHeader("x-rapidapi-key", this._config.ApiKey);
            request = request.AddHeader("x-rapidapi-host", this._config.ApiHost);

            // Go get it.
            var response = await client.ExecuteAsync(request);
            T returned = System.Text.Json.JsonSerializer.Deserialize<T>(response.Content);
            return returned;
        }
    }
}