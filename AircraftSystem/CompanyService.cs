using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftSystem
{
    public class CompanyService
    {
        CompanyRepository companyRepository;

        public CompanyService(CompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public void ExecuteAddCompanyProcedure()
        {
            string companyName = null;
            do
            {
                Console.WriteLine("\nEnter country name you would like to add:");
                string companyNameEntry = Console.ReadLine();

                if (String.IsNullOrEmpty(companyNameEntry))
                {
                    Console.WriteLine("\nIncorrect input! Company name should not be empty!\n");
                }
                else if (companyRepository.GetCompanyNames().Contains(companyNameEntry))
                {
                    Console.WriteLine("This company already exists! Not possible to add!");
                }
                else
                {
                    for (int i = 0; i < companyNameEntry.Length; i++)
                    {
                        if (i == 0)
                        {
                            companyName += char.ToUpper(companyNameEntry[i]);
                        }
                        else
                        {
                            companyName += char.ToLower(companyNameEntry[i]);
                        }
                    }
                }
            } while (companyName == null);

            companyRepository.AddCompany(companyName);
            Console.WriteLine("Company is added sucessfully!");
        }

        public void ExecuteDeleteCompanyProcedure()
        {
            Console.WriteLine("\nCompanies in Database:");
            foreach (string company in companyRepository.companyIdsAndNames())
            {
                Console.WriteLine(company);
            }

            string companyId = null;
            do
            {
                Console.WriteLine("\nWhich company would you like to delete? Enter company id e.g. 1:");      
                string companyIdEntry = (Console.ReadLine()).ToUpper();

                if (String.IsNullOrEmpty(companyIdEntry))
                {
                    Console.WriteLine("\nEmpty inputs are not acceptable!\n");
                }
                else if (!companyRepository.GetCompanyIds().Contains(companyIdEntry))
                {
                    Console.WriteLine("This company does not exist!");
                }
                else
                {
                    companyId = companyIdEntry;
                }
            } while (companyId == null);

            companyRepository.DeleteCompany(companyId);
            Console.WriteLine("\nCompany is deleted!\n");
        }
    }
}
