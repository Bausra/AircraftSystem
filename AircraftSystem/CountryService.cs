using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        

        public void ExecuteAddCountryProcedure()
        {
            List<string> availableShorthands = countryRepository.GetAllCountries().Select(x => x.Shorthand).ToList(); //kartojasi

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


            string countryName = null;
            do
            {
                Console.WriteLine("\nEnter full country name (e.g. Lithuania:)");           //kartojasi pakoreguoti veliau
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


            countryRepository.AddCountry(new Country(shorthand, countryName, isEurope == 1 ? true : false));
            Console.WriteLine("\nCountry is added!\n");
        }

        public void ExecuteDeleteCountryProcedure()
        {
            List<string> availableShorthands = countryRepository.GetAllCountries().Select(x => x.Shorthand).ToList();       //kartojasi

            Console.WriteLine("\nCountries in Database:");
            foreach (Country country in countryRepository.GetAllCountries())
            {
                Console.WriteLine($"[{country.Shorthand}] {country.Name}");
            }

            string shorthand = null;
            do
            {
                Console.WriteLine("\nWhich country would you like to delete? Enter country shorthand e.g. LT:");        //kartojasi pakoreguoti veliau
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

            countryRepository.DeleteCountry(shorthand);
            Console.WriteLine("\nCountry is deleted!\n");
        }
    }
}
