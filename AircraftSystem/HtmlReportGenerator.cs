using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AircraftSystem.Models;

namespace AircraftSystem
{
    public class HtmlReportGenerator
    {
        AircraftRepository aircraftRepository;

        public HtmlReportGenerator(AircraftRepository aircraftRepository)
        {
            this.aircraftRepository = aircraftRepository;
        }

        private List<Aircraft> FilterAircraftData(bool isEurope)
        {
            List <Aircraft> filteredAircrafts = new List<Aircraft>();
            
            foreach (Aircraft aircraft in aircraftRepository.GetAllAircraftData())
            {
                if (aircraft.Country.IsEurope == isEurope)
                {
                    filteredAircrafts.Add(aircraft);
                }
            }

            return filteredAircrafts;
        }

        private void GenerateHTMLReport(List<Aircraft> filteredAircrafts, string fileDirectory)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("<table>");
            stringBuilder.Append("<tr><th>Aircraft Tail number</th><th>Aircraft model description</th><th>Aircraft model number</th><th>Company name</th><th>Country name</th></tr>");

            var rows = from aircraft in filteredAircrafts
                       let row = "<td>" + aircraft.TailNumber + "</td><td>" + aircraft.AircraftModel.Description + "</td><td>" + aircraft.AircraftModel.Number + "</td><td>" + aircraft.Company.Name + "</td><td>" + aircraft.Country.Name + "</td>"
                       select row;

            rows.ToList().ForEach(row => stringBuilder.Append("<tr>" + row + "</tr>"));

            stringBuilder.Append("</table>");

            System.IO.File.WriteAllText(fileDirectory, stringBuilder.ToString());
        }

        private string GetFileDirectory()
        {
            Console.WriteLine("Enter file directory for savig html file (e.g. C:\\develop):");
            string fileDirectory = Console.ReadLine();
            Console.WriteLine("Enter file name (e.g. report):");
            string fileName = Console.ReadLine();

            return @$"{fileDirectory}\{fileName}.html";
        }

        public void ExecuteHTMLReportAircraftInEurope(bool value)
        {
            List<Aircraft> filteredAirrafts = FilterAircraftData(value);
            string fileDirectory = GetFileDirectory();

            GenerateHTMLReport(filteredAirrafts, fileDirectory);
            Console.WriteLine("Report was saved to your provided location sucessfully!");
        }
    }
}
