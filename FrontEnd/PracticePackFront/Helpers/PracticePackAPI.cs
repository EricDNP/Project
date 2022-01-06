namespace PracticePackFront.Helpers
{
    public class PracticePackAPI
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://practicepackapi.azurewebsites.net/api/");
            return Client;
        }
    }
}
