using AircraftSystem.Models;

namespace AircraftSystem
{
    public class AircraftService
    {
        AircraftRepository aircraftRepository;
        AircraftModelRepository aircraftModelRepository;
        AircraftModelService aircraftModelService;
        CompanyRepository companyRepository;
        CompanyService companyService;
        CountryRepository countryRepository;
        CountryService countryService;

        public AircraftService(AircraftRepository aircraftRepository, AircraftModelRepository aircraftModelRepository, AircraftModelService aircraftModelService, CompanyRepository companyRepository, CompanyService companyService, CountryRepository countryRepository, CountryService countryService)
        {
            this.aircraftRepository = aircraftRepository;
            this.aircraftModelRepository = aircraftModelRepository;
            this.aircraftModelService = aircraftModelService;
            this.companyRepository = companyRepository;
            this.companyService = companyService;
            this.countryRepository = countryRepository;
            this.countryService = countryService;
        }

        private string GetAircraftTailNumber()
        {
            List<string> aircraftTailNumbers = aircraftRepository.GetAllAircraftData().Select(x => x.TailNumber).ToList();

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

            return aircraftTailNumber;
        }

        private void PrintAllAircrafts()
        {
            Console.WriteLine("\nAircrafts in Database:");
            foreach (Aircraft aircraft in aircraftRepository.GetAllAircraftData())
            {
                Console.WriteLine($"[{aircraft.ID}] {aircraft.TailNumber} {aircraft.AircraftModel.Description} {aircraft.AircraftModel.Number} {aircraft.Company.Name} {aircraft.Country.Name}");
            }
        }

        private string GetAircraftId()
        {
            string aircraftId = null;
            do
            {
                List<string> availableAircraftIds = aircraftRepository.GetAllAircraftData().Select(x => Convert.ToString(x.ID)).ToList();

                Console.WriteLine("\nChoose aircraft id e.g. 1:");
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

            return aircraftId;
        }

        public void ExecuteAddAircraftProcedure()
        {
            Int32 aircraftId = -1;      //value of this variable does not have any meaning, it will not be used in adding aircraft further on, but is necessary for creation of aircraft object
            string aircraftTailNumber = GetAircraftTailNumber();
            aircraftModelService.PrintAllAircraftModels();
            string aircraftModelId = aircraftModelService.GetAircraftModelId();
            companyService.PrintAllCompanies();
            string companyId = companyService.GetCompanyID();
            countryService.PrintAllCountries();
            string shorthand = countryService.GetCountryShorthandFromDatabase();

            AircraftModel chosenAircraftModel = aircraftModelRepository.GetAircraftModel(aircraftModelId);
            Company chosenCompany = companyRepository.GetCompany(companyId);
            Country chosenCountry = countryRepository.GetCountry(shorthand);

            aircraftRepository.AddAircraft(new Aircraft(aircraftId, aircraftTailNumber, chosenAircraftModel, chosenCompany, chosenCountry));
            Console.WriteLine("Aircraft added sucessfully!");
        }

        public void ExecuteDeleteAircraftProcedure()
        {
            PrintAllAircrafts();
            Console.WriteLine("Which aircraft would you like to delete?");
            string aircraftId = GetAircraftId();

            aircraftRepository.DeleteAircraft(aircraftId);
            Console.WriteLine("\nAircraft is deleted!\n");
        }
    }
}
