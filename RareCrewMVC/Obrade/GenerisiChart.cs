using RareCrewMVC.Models;
using System.Drawing;
using ScottPlot;

namespace RareCrewMVC.Obrade
{
    public class GenerisiChart
    {
        private const int defaultFontSize = 25;
        private const int ColorsRandomSeed = 1;
        public record Employee(string EmployeeName, int TotalHoursWorked);
        public record struct Resolution
        {
            public int Width { get; init; }
            public int Height { get; init; }
            public Resolution(int width, int height)
            {
                if (width <= 0 || height <= 0)
                    throw new ArgumentException("Resolution width and height must be positive numbers!");

                (Width, Height) = (width, height);
            }

            public Resolution() : this(0, 0) { }
        }

        public static void GenerateChartPng(
            List<RadnikModel> zaposleni,
            string putanja,
            int resWidth,
            int resLenght)
        {
            if (string.IsNullOrEmpty(putanja))
            {
                throw new ArgumentException($"{nameof(putanja)} has null or emty string!");
            }

            Random rnd = new(ColorsRandomSeed);

            ScottPlot.Plot myPlot = new();

            int totalTimeWorked = zaposleni
                .Select(zaposleni => (int)zaposleni.TotalHoursWorked)
                .Sum();

            var slices = zaposleni
                    .Select(likBrtKolega => new ScottPlot.PieSlice()
                    {
                        Label = $"{likBrtKolega.EmployeeName} {PrecentageWorked(totalTimeWorked, likBrtKolega.TotalHoursWorked):0.0}%",
                        Value = likBrtKolega.TotalHoursWorked,
                        FillColor = new ScottPlot.Color((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256))
                    })
                    .ToList();

            slices.ForEach(slice =>
            {
                slice.LabelStyle.FontSize = defaultFontSize;
            });

            var pie = myPlot.Add.Pie(slices);
            pie.ShowSliceLabels = true;
            pie.ExplodeFraction = .02;


            myPlot.ShowLegend();
            myPlot.HideGrid();
            var (width, height) = (resWidth, resLenght);
            myPlot.SavePng(putanja, width, height);
        }


        private static double PrecentageWorked(double TotalTime, double TimeWorked) =>
             100 * TimeWorked / TotalTime;
    }
}
