using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using RareCrewMVC.Models;
using RareCrewMVC.Obrade;

namespace RareCrewMVC.Obrade
{
    public class GenerisiHtml
    {
        private async void GenerateReport()
        {
            HTMLgenerator reportGenerator = new HTMLgenerator();
            await reportGenerator.GenerateHtmlReportAsync();
        }

        private async void LoadData()
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

                //radniciListView.ItemsSource = radnici;



                //File.WriteAllText("output.html", htmlContent);
            }
        }
    }
}
