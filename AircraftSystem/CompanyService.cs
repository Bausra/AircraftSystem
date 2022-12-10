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

        private string GetCompanyName()
        {
            List<string> availableCompanies = companyRepository.GetAllCompanies().Select(x => x.Name).ToList();
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

            return companyName;
        }

        public void PrintAllCompanies()
        {
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
        }

        public string GetCompanyID()
        {
            List<string> availableIDs = companyRepository.GetAllCompanies().Select(x => Convert.ToString(x.ID)).ToList();

            string companyId = null;
            do
            {
                Console.WriteLine("\nChoose company id e.g. 1:");
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

            return companyId;
        }

        public void ExecuteAddCompanyProcedure()
        {
            Int32 companyId = -1;   //value of this variable does not have any meaning, it will not be used in adding company further on, but is necessary for creation of company object
            string companyName = GetCompanyName();

            companyRepository.AddCompany(new Company(companyId, companyName));
            Console.WriteLine("Company is added sucessfully!");
        }

        public void ExecuteDeleteCompanyProcedure()
        {
            PrintAllCompanies();
            Console.WriteLine("Which company would you like to delete?");
            string companyId = GetCompanyID();

            companyRepository.DeleteCompany(companyId);
            Console.WriteLine("\nCompany is deleted sucessfully!\n");
        }
    }
}
