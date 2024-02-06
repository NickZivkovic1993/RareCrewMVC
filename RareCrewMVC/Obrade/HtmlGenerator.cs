using Newtonsoft.Json;
using RareCrewMVC.Models;
using System.Text;

namespace RareCrewMVC.Obrade
{
    public class HTMLgenerator
    {
        //znam nije DRY al ono za wpf je manje vise bilo prva verzija
        public async Task GenerateHtmlReportAsync()
        {
            string apiKey = "vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";
            string apiUrl = $"https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(apiUrl);
                var radnici = JsonConvert.DeserializeObject<List<RadnikModel>>(response);

                radnici = ListMerger.SpojiRadnike(radnici);

                foreach (var radnik in radnici)
                {
                    //extra totalhours worked??
                    //provalio sam da neka json objekti nemaju ime
                    //ne znam kako to bezimeni ljudi rade^^ al ok nazvacu ih Ostali
                    double round = radnik.HoursWorked.TotalHours;
                    radnik.TotalHoursWorked = Math.Round(round, 2);
                }

                radnici.Last().EmployeeName = "Ostali";

                string htmlContent = GenerateHtml(radnici);


                //File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "output.html"), htmlContent);
                File.WriteAllText(@"D:\\output.html", htmlContent);

            }
        }


        public string GenerateHtml(List<RadnikModel> radnici)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            htmlBuilder.AppendLine("<html>");
            htmlBuilder.AppendLine("<head>");
            htmlBuilder.AppendLine("<title>Radnici Report</title>");
            htmlBuilder.AppendLine("<style>");
            htmlBuilder.AppendLine("tr.red { background-color: red; }");
            htmlBuilder.AppendLine("</style>");
            htmlBuilder.AppendLine("</head>");
            htmlBuilder.AppendLine("<body>");
            htmlBuilder.AppendLine("<h1>Radnici Report</h1>");

            htmlBuilder.AppendLine("<table border='1'>");
            htmlBuilder.AppendLine("<tr>");
            htmlBuilder.AppendLine("<th>Name</th>");
            htmlBuilder.AppendLine("<th>Total Hours Worked</th>");
            htmlBuilder.AppendLine("</tr>");

            foreach (var radnik in radnici)
            {
                // provera za radnike
                bool isLessThan100Hours = radnik.TotalHoursWorked < 100;


                string rowStyle = isLessThan100Hours ? "class='red'" : "";

                htmlBuilder.AppendLine($"<tr {rowStyle}>");
                htmlBuilder.AppendLine($"<td>{radnik.EmployeeName}</td>");
                htmlBuilder.AppendLine($"<td>{radnik.TotalHoursWorked}</td>");
                htmlBuilder.AppendLine("</tr>");
            }

            htmlBuilder.AppendLine("</table>");

            htmlBuilder.AppendLine("</body>");
            htmlBuilder.AppendLine("</html>");

            return htmlBuilder.ToString();
        }
    }

    
}

