using RareCrewMVC.Models;

namespace RareCrewMVC.Obrade
{
    public class ListMerger
    {
        public static List<RadnikModel> SpojiRadnike(List<RadnikModel> Radnici)
        {
            var spojeniRadnici = Radnici
            .GroupBy(e => e.EmployeeName)
            .Select(g => new RadnikModel
            {
                EmployeeName = g.Key,
                HoursWorked = TimeSpan.FromHours(g.Sum(e => e.HoursWorked.TotalHours))
            })
            .ToList();


            return spojeniRadnici;
        }


    }
}
