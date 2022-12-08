using System.Data;
using System.Text;
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

        private void GenerateHTMLReport(List<Aircraft> filteredAircrafts, string fileDirectory, bool value)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();

                string border = $"style = 'border: solid 1px rgb(3,1,4); border-collapse: collapse'";
                string color = value ? "style = 'background-color: rgb(176,224,230)'" : "style = 'background-color: rgb(247,111,114)'";

                stringBuilder.Append(String.Format("<table {0}>", border));
                stringBuilder.Append(String.Format(@"<tr {1}>
                                                <th {0}>Aircraft Tail number</th>
                                                <th {0}>Aircraft model description</th>
                                                <th {0}>Aircraft model number</th>
                                                <th {0}>Company name</th>
                                                <th {0}>Country name</th></tr>",
                                                    border, color)
                                                    );

                var rows = from aircraft in filteredAircrafts
                           let row = @$"<td {border}> {aircraft.TailNumber} </td>
                                    <td {border}> {aircraft.AircraftModel.Description} </td>
                                    <td {border}> {aircraft.AircraftModel.Number} </td>
                                    <td {border}> {aircraft.Company.Name} </td>
                                    <td {border}> {aircraft.Country.Name}</td>"
                           select row;

                rows.ToList().ForEach(row => stringBuilder.Append("<tr>" + row + "</tr>"));

                stringBuilder.Append("</table>");

                File.WriteAllText(fileDirectory, stringBuilder.ToString());
            }
            catch
            {
                Console.WriteLine("\nIncorrect file location! Press enter to go back to the main menu.\n");
                return;
            }
        }

        private string GetFileDirectory()
        {
            Console.WriteLine("\nEnter file directory for savig html file (e.g. C:\\develop):");
            string fileDirectory = Console.ReadLine();
            Console.WriteLine("\nEnter file name (e.g. report):");
            string fileName = Console.ReadLine();

            return @$"{fileDirectory}\{fileName}.html";
        }

        public string ExecuteHTMLReportAircraftInEurope(bool value)
        {
            List<Aircraft> filteredAirrafts = FilterAircraftData(value);
            string fileDirectory = GetFileDirectory();

            GenerateHTMLReport(filteredAirrafts, fileDirectory, value);
            Console.WriteLine("\nReport was saved to your provided location sucessfully!");

            return fileDirectory;
        }
    }
}
