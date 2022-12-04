using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AircraftSystem.Models;

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
            List<string> availableCompanies = companyRepository.GetAllCompanies().Select(x => x.Name).ToList();

            Int32 companyId = -1;
            string companyName = null;
            do
            {
                Console.WriteLine("\nEnter company name you would like to add:");
                string companyNameEntry = Console.ReadLine();

                if (String.IsNullOrEmpty(companyNameEntry))
                {
                    Console.WriteLine("\nIncorrect input! Company name should not be empty!\n");
                }
                else if (availableCompanies.Contains(companyNameEntry))
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

            companyRepository.AddCompany(new Company(companyId, companyName));
            Console.WriteLine("Company is added sucessfully!");
        }

        public void ExecuteDeleteCompanyProcedure()
        {
            List<string> availableCompanies = companyRepository.GetAllCompanies().Select(x => x.Name + x.ID).ToList();
            List<string> availableIDs = companyRepository.GetAllCompanies().Select(x => Convert.ToString(x.ID)).ToList();

            Console.WriteLine("\nCompanies in Database:");
            foreach (Company company in companyRepository.GetAllCompanies())
            {
                Console.WriteLine($"[{company.ID}] {company.Name}");
            }

            string companyId = null;
            do
            {
                Console.WriteLine("\nWhich company would you like to delete? Enter company id e.g. 1:");      
                string companyIdEntry = Console.ReadLine();

                if (String.IsNullOrEmpty(companyIdEntry))
                {
                    Console.WriteLine("\nEmpty inputs are not acceptable!\n");
                }
                else if (!availableIDs.Contains(companyIdEntry))      
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
