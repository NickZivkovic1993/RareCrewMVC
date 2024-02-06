using Microsoft.AspNetCore.Http.Extensions;
using RareCrewMVC.Models;

namespace RareCrewMVC.Obrade
{
    public class RadnikObrada
    {

        public static async Task<List<RadnikModel>> ObradaRadnika()
        {
            //prvo glupo samo za test resenje
            //string url = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

            //ni ova ne valjaju al deo po deo
            string key = "vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";
            string finalUrl = $"https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code={key}";


            using (HttpResponseMessage response = await OtvoriHttp.ApiClient.GetAsync(finalUrl))
            {

                if (response.IsSuccessStatusCode)
                {
                    List<RadnikModel> radnici = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RadnikModel>>(finalUrl);

                    //RadnikModel radnik = await response.Content.ReadAsAsync<RadnikModel>();

                    return radnici;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }

    


}
