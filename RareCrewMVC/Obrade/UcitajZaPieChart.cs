using Newtonsoft.Json;
using RareCrewMVC.Models;
using System.Reflection;

namespace RareCrewMVC.Obrade
{
    public class UcitajZaPieChart
    {
        public async Task<List<RadnikModel>> LoadDataAsync()
        {
            // Call the static method to load data
            return await Loader();
        }
        public async Task<List<RadnikModel>> Loader()
        {
            //ovo treba da ide drugde u apsettings/json najverovatnije al za trenutne potrebe je dovoljno dobro
            string apiKey = "vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";
            string apiUrl = $"https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(apiUrl);
                var radnici = JsonConvert.DeserializeObject<List<RadnikModel>>(response);

                radnici = ListMerger.SpojiRadnike(radnici);

                foreach (var radnik in radnici)
                {
                    double round = radnik.HoursWorked.TotalHours;
                    radnik.TotalHoursWorked = Math.Round(round, 2);
                }

                radnici.Last().EmployeeName = "Ostali";

                return radnici;

            }
        }
    }
}
