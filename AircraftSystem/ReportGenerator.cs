using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AircraftSystem.Models;
using ConsoleTables;

namespace AircraftSystem
{
    public class ReportGenerator
    {
        AircraftRepository aircraftRepository;
        AircraftModelRepository aircraftModelRepository;
        CompanyRepository companyRepository;
        CountryRepository countryRepository;

        public ReportGenerator(AircraftRepository aircraftRepository, AircraftModelRepository aircraftModelRepository, CompanyRepository companyRepository, CountryRepository countryRepository)
        {
            this.aircraftRepository = aircraftRepository;
            this.aircraftModelRepository = aircraftModelRepository;
            this.companyRepository = companyRepository;
            this.countryRepository = countryRepository;
        }

        internal void PrintReportTableContent(bool isEurope)
        {
            //Console.WriteLine("Aircraft ID | Tail Number | Model Description | Model Number | Company Name | Country Shorthand | Country Name");
            
            foreach (Aircraft aircraft in aircraftRepository.GetAllAircraftData())
            {
                if (aircraft.Country.IsEurope == isEurope)
                {
                    Console.WriteLine($"{aircraft.ID} {aircraft.TailNumber} {aircraft.AircraftModel.Description} {aircraft.AircraftModel.Number} {aircraft.Company.Name} {aircraft.Country.Shorthand} {aircraft.Country.Name}");
                }
            }
        }

        public void GenerateReportAircraftInEurope()
        {
            Console.WriteLine("Aircrafts in Europe:");

            PrintReportTableContent(true);
        }

        public void GenerateReportAircraftNotInEurope()
        {
            Console.WriteLine("Aircrafts from outside Europe:");

            PrintReportTableContent(false);
        }
    }
}
