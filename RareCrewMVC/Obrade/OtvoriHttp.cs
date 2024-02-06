namespace RareCrewMVC.Obrade
{
    public class OtvoriHttp
    {
        
            public static HttpClient ApiClient { get; set; }

            public static void InitializeClient()
            {
                ApiClient = new HttpClient();
                //ApiClient.BaseAddress = new Uri(""); da moze da se koristi za pristup drugim apijevim adresama

                ApiClient.DefaultRequestHeaders.Accept.Clear();
            }
        
    }
}
