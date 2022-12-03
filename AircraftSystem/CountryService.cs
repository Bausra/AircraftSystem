using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftSystem
{
    public class CountryService
    {
        CountryRepository countryRepository;

        public CountryService(CountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public void ExecuteAddCountryProcedure()
        {
            string shorthand = null;
            do
            {
                Console.WriteLine("\nEnter country shorthand (e.g. LT):"); 
                string shorthandEntry = (Console.ReadLine()).ToUpper();
               
                if (shorthandEntry.Any(char.IsDigit) || shorthandEntry.Length != 2 || String.IsNullOrEmpty(shorthandEntry))
                {
                    Console.WriteLine("\nIncorrect input! Country shorthand should consist of 2 letters!\n");
                }
                else if (countryRepository.GetCountryShorthands().Contains(shorthandEntry))
                {
                    Console.WriteLine("This country shorthand is already in the system! Not possible to add!");
                }
                else
                {
                    shorthand = shorthandEntry;
                }              
            } while (shorthand == null);


            string countryName = null;
            do
            {
                Console.WriteLine("\nEnter full country name (e.g. Lithuania:)");           //kartojasi
                string countryNameEntry = Console.ReadLine();

                if (countryNameEntry.Any(char.IsDigit) || String.IsNullOrEmpty(countryNameEntry))
                {
                    Console.WriteLine("\nIncorrect input! Country name should not be empty of consist of any digits!\n");
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


            int isEurope = -1;
            do
            {
                Console.WriteLine("\nChoose:");
                Console.WriteLine("[1]Country is in Europe");
                Console.WriteLine("[0]Country is in not in Europe\n");
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


            countryRepository.AddCountry(shorthand, countryName, isEurope); 
            Console.WriteLine("\nCountry is added!\n");
        }

        public void ExecuteDeleteCountryProcedure()
        {
            Console.WriteLine("\nCountries in Database:");
            foreach (string country in countryRepository.countriesShorthandsAndNames()) 
            {
                Console.WriteLine(country);
            }

            string shorthand = null;
            do
            {
                Console.WriteLine("\nWhich country would you like to delete? Enter country shorthand e.g. LT:");        //kartojasi
                string shorthandEntry = (Console.ReadLine()).ToUpper();

                if (shorthandEntry.Any(char.IsDigit) || shorthandEntry.Length != 2 || String.IsNullOrEmpty(shorthandEntry))
                {
                    Console.WriteLine("\nIncorrect input! Country shorthand should consist of 2 letters!\n");
                }
                else if (!countryRepository.GetCountryShorthands().Contains(shorthandEntry))
                {
                    Console.WriteLine("This country does not exist!");
                }
                else
                {
                    shorthand = shorthandEntry;
                }
            } while (shorthand == null);

            countryRepository.DeleteCountry(shorthand);
            Console.WriteLine("\nCountry is deleted!\n");
        }
    }
}
