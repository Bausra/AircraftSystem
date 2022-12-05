using AircraftSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftSystem
{
    public class AircraftService
    {
        AircraftRepository aircraftRepository;
        AircraftModelRepository aircraftModelRepository;
        CompanyRepository companyRepository;
        CountryRepository countryRepository;

        public AircraftService(AircraftRepository aircraftRepository, AircraftModelRepository aircraftModelRepository, CompanyRepository companyRepository, CountryRepository countryRepository)
        {
            this.aircraftRepository = aircraftRepository;
            this.aircraftModelRepository = aircraftModelRepository;
            this.companyRepository = companyRepository;
            this.countryRepository = countryRepository;
        }

        public void ExecuteAddAircraftProcedure()
        {
            List<string> aircraftTailNumbers = aircraftRepository.GetAllAircraftData().Select(x => x.TailNumber).ToList();
            List<string> availableAircraftModelIDs = aircraftModelRepository.GetAllAircraftModels().Select(x => Convert.ToString(x.ID)).ToList();
            List<string> availableCompanyIDs = companyRepository.GetAllCompanies().Select(x => Convert.ToString(x.ID)).ToList();
            List<string> availableCountryShorthands = countryRepository.GetAllCountries().Select(x => x.Shorthand).ToList();

            Int32 aircraftId = -1;
            string aircraftTailNumber = null;
            do
            {
                Console.WriteLine("\nEnter aircraft tail number:");
                string aircraftTailNumberEntry = Console.ReadLine();

                if (String.IsNullOrEmpty(aircraftTailNumberEntry))
                {
                    Console.WriteLine("\nIncorrect input! Aircraft tail number should not be empty!\n");
                }
                else if (aircraftTailNumbers.Contains(aircraftTailNumberEntry))
                {
                    Console.WriteLine("\nThis aircraft tail number already exists, cannot add!\n");
                }
                else
                {
                    aircraftTailNumber = aircraftTailNumberEntry;
                }
            } while (aircraftTailNumber == null);



            Console.WriteLine("\nAircraft models in Database:");                //same as in aicraft model service - adjust later all below
            if (!aircraftModelRepository.GetAllAircraftModels().Any())
            {
                Console.WriteLine("No aircraft models exist yet, add them first! Press enter to continue to the main menu!");
                Console.ReadLine();
                return;
            }
            else
            {
                foreach (AircraftModel aircraftModel in aircraftModelRepository.GetAllAircraftModels())
                {
                    Console.WriteLine($"[{aircraftModel.ID}] {aircraftModel.Description} {aircraftModel.Number}");
                }
            }

            string aircraftModelId = null;
            do
            {
                Console.WriteLine("\nChoose aircraft model e.g. 1:");
                string aircraftModelIdEntry = Console.ReadLine();

                if (String.IsNullOrEmpty(aircraftModelIdEntry))
                {
                    Console.WriteLine("\nEmpty inputs are not acceptable!\n");
                }
                else if (!availableAircraftModelIDs.Contains(aircraftModelIdEntry))
                {
                    Console.WriteLine("\nThis aircraft model does not exist!\n");
                }
                else
                {
                    aircraftModelId = aircraftModelIdEntry;
                }
            } while (aircraftModelId == null);


            Console.WriteLine("\nCompanies in Database:");
            if (!companyRepository.GetAllCompanies().Any())
            {
                Console.WriteLine("No companies exist yet, add them first! Press enter to continue to the main menu!");
                Console.ReadLine();
                return;
            }
            else
            {
                foreach (Company company in companyRepository.GetAllCompanies())
                {
                    Console.WriteLine($"[{company.ID}] {company.Name}");
                }
            }

            string companyId = null;
            do
            {
                Console.WriteLine("\nChoose company e.g. 1:");
                string companyIdEntry = Console.ReadLine();

                if (String.IsNullOrEmpty(companyIdEntry))
                {
                    Console.WriteLine("\nEmpty inputs are not acceptable!\n");
                }
                else if (!availableCompanyIDs.Contains(companyIdEntry))
                {
                    Console.WriteLine("This company does not exist!");
                }
                else
                {
                    companyId = companyIdEntry;
                }
            } while (companyId == null);



            Console.WriteLine("\nCountries in Database:");
            if (!countryRepository.GetAllCountries().Any())
            {
                Console.WriteLine("No countries exist yet, add them first! Press enter to continue to the main menu!");
                Console.ReadLine();
                return;
            }
            else
            {
                foreach (Country country in countryRepository.GetAllCountries())
                {
                    Console.WriteLine($"[{country.Shorthand}] {country.Name}");
                }
            }

            string shorthand = null;
            do
            {
                Console.WriteLine("\nWhich country would you like to add? Enter country shorthand e.g. LT:");
                string shorthandEntry = (Console.ReadLine()).ToUpper();

                if (shorthandEntry.Any(char.IsDigit) || shorthandEntry.Length != 2 || String.IsNullOrEmpty(shorthandEntry))
                {
                    Console.WriteLine("\nIncorrect input! Country shorthand should consist of 2 letters!\n");
                }
                else if (!availableCountryShorthands.Contains(shorthandEntry))
                {
                    Console.WriteLine("This country does not exist!");
                }
                else
                {
                    shorthand = shorthandEntry;
                }
            } while (shorthand == null);


            AircraftModel chosenAircraftModel = aircraftModelRepository.GetAircraftModel(aircraftModelId);
            Company chosenCompany = companyRepository.GetCompany(companyId);
            Country chosenCountry = countryRepository.GetCountry(shorthand);

            aircraftRepository.AddAircraft(new Aircraft(aircraftId, aircraftTailNumber, chosenAircraftModel, chosenCompany, chosenCountry));
            Console.WriteLine("Aircraft added sucessfully!");
        }

        public void ExecuteDeleteAircraftProcedure()
        {
            List<string> availableAircraftIds = aircraftRepository.GetAllAircraftData().Select(x => Convert.ToString(x.ID)).ToList();

            Console.WriteLine("\nAircrafts in Database:");
            foreach (Aircraft aircraft in aircraftRepository.GetAllAircraftData())
            {
                Console.WriteLine($"[{aircraft.ID}] {aircraft.TailNumber} {aircraft.AircraftModel.Description} {aircraft.AircraftModel.Number} {aircraft.Company.Name} {aircraft.Country.Name}");
            }

            string aircraftId = null;
            do
            {
                Console.WriteLine("\nWhich aircraft would you like to delete? Enter id e.g. 1:");
                string aircraftIdEntry = Console.ReadLine();

                if (String.IsNullOrEmpty(aircraftIdEntry))
                {
                    Console.WriteLine("\nEmpty inputs are not acceptable!\n");
                }
                else if (!availableAircraftIds.Contains(aircraftIdEntry))
                {
                    Console.WriteLine("\nThis aircraft model does not exist!\n");
                }
                else
                {
                    aircraftId = aircraftIdEntry;
                }
            } while (aircraftId == null);

            aircraftRepository.DeleteAircraft(aircraftId);
            Console.WriteLine("\nAircraft is deleted!\n");
        }
    }
}
