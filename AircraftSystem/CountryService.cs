using AircraftSystem.Models;

namespace AircraftSystem
{
    public class CountryService
    {
        CountryRepository countryRepository;

        public CountryService(CountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }
        
        private string GetCountryShorthandToAdd()
        {
            List<string> availableShorthands = countryRepository.GetAllCountries().Select(x => x.Shorthand).ToList();

            string shorthand = null;
            do
            {
                Console.WriteLine("\nEnter country shorthand (e.g. LT):");
                string shorthandEntry = (Console.ReadLine()).ToUpper();


                if (shorthandEntry.Any(char.IsDigit) || shorthandEntry.Length != 2 || String.IsNullOrEmpty(shorthandEntry))
                {
                    Console.WriteLine("\nIncorrect input! Country shorthand should consist of 2 letters!\n");
                }
                else if (availableShorthands.Contains(shorthandEntry))
                {
                    Console.WriteLine("This country shorthand is already in the system! Not possible to add!");
                }
                else
                {
                    shorthand = shorthandEntry;
                }
            } while (shorthand == null);

            return shorthand;
        }


        private string GetCountryName()
        {
            string countryName = null;
            do
            {
                Console.WriteLine("\nEnter full country name (e.g. Lithuania:)");           
                string countryNameEntry = Console.ReadLine();

                if (countryNameEntry.Any(char.IsDigit) || String.IsNullOrEmpty(countryNameEntry))
                {
                    Console.WriteLine("\nIncorrect input! Country name should not be empty or consist of any digits!\n");
                }
                else
                {
                    for (int i = 0; i < countryNameEntry.Length; i++)
                    {
                        if (i == 0)
                        {
                            countryName += char.ToUpper(countryNameEntry[i]);
                        }
                        else
                        {
                            countryName += char.ToLower(countryNameEntry[i]);
                        }
                    }
                }
            } while (countryName == null);

            return countryName;
        }

        private int GetStatusIsEurope()
        {
            int isEurope = -1;
            do
            {
                Console.WriteLine("\nChoose:");
                Console.WriteLine("[1]Country is in Europe");
                Console.WriteLine("[0]Country is not in Europe\n");
                string entry = Console.ReadLine();

                if (entry != "0" && entry != "1")
                {
                    Console.WriteLine("\nIncorrect input!\n");
                }
                else
                {
                    isEurope = Int32.Parse(entry);
                }
            } while (isEurope == -1);

            return isEurope;
        }

        public void PrintAllCountries()
        {
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
        }

        public string GetCountryShorthandFromDatabase()
        {
            List<string> availableShorthands = countryRepository.GetAllCountries().Select(x => x.Shorthand).ToList();

            string shorthand = null;
            do
            {
                Console.WriteLine("\nChoose country shorthand e.g. LT:");       
                string shorthandEntry = (Console.ReadLine()).ToUpper();

                if (shorthandEntry.Any(char.IsDigit) || shorthandEntry.Length != 2 || String.IsNullOrEmpty(shorthandEntry))
                {
                    Console.WriteLine("\nIncorrect input! Country shorthand should consist of 2 letters!\n");
                }
                else if (!availableShorthands.Contains(shorthandEntry))
                {
                    Console.WriteLine("This country does not exist!");
                }
                else
                {
                    shorthand = shorthandEntry;
                }
            } while (shorthand == null);

            return shorthand;
        }

        public void ExecuteAddCountryProcedure()
        {
            string shorthand = GetCountryShorthandToAdd();
            string countryName = GetCountryName();
            int isEurope = GetStatusIsEurope();

            countryRepository.AddCountry(new Country(shorthand, countryName, isEurope == 1 ? true : false));
            Console.WriteLine("\nCountry is added sucessfully!\n");
        }

        public void ExecuteDeleteCountryProcedure()
        {
            PrintAllCountries();
            Console.WriteLine("Which country would you like to delete?");
            string shorthand = GetCountryShorthandFromDatabase();

            countryRepository.DeleteCountry(shorthand);
            Console.WriteLine("\nCountry is deleted sucessfully!\n");
        }
    }
}
