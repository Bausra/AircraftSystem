using AircraftSystem.Models;

namespace AircraftSystem
{
    public class AircraftModelService
    {
        AircraftModelRepository aircraftModelRepository;

        public AircraftModelService(AircraftModelRepository aircraftModelRepository)
        {
            this.aircraftModelRepository = aircraftModelRepository;
        }

        private string GetAircraftModelDescription()
        {
            string aircraftModelDescription = null;
            do
            {
                Console.WriteLine("\nEnter aircraft model description:");
                string aircraftModelDescriptionEntry = Console.ReadLine();

                if (String.IsNullOrEmpty(aircraftModelDescriptionEntry))
                {
                    Console.WriteLine("\nIncorrect input! Aircraft model description should not be empty!\n");
                }
                else
                {
                    for (int i = 0; i < aircraftModelDescriptionEntry.Length; i++)
                    {
                        if (i == 0)
                        {
                            aircraftModelDescription += char.ToUpper(aircraftModelDescriptionEntry[i]);
                        }
                        else
                        {
                            aircraftModelDescription += char.ToLower(aircraftModelDescriptionEntry[i]);
                        }
                    }
                }
            } while (aircraftModelDescription == null);

            return aircraftModelDescription;
        }

        private string GetAircraftModelNumber()
        {
            List<string> availableAircraftModels = aircraftModelRepository.GetAllAircraftModels().Select(x => x.Number).ToList();

            string aircraftModelNumber = null;
            do
            {
                Console.WriteLine("\nEnter aircraft model number:");
                string aircraftModelNumberEntry = Console.ReadLine();

                if (String.IsNullOrEmpty(aircraftModelNumberEntry))
                {
                    Console.WriteLine("\nIncorrect input! Aircraft model number should not be empty!\n");
                }
                else if (availableAircraftModels.Contains(aircraftModelNumberEntry))
                {
                    Console.WriteLine("\nThis aircraft model number already exists, cannot add!\n");
                }
                else
                {
                    aircraftModelNumber = aircraftModelNumberEntry;
                }
            } while (aircraftModelNumber == null);

            return aircraftModelNumber;
        }

        public void PrintAllAircraftModels()
        {
            Console.WriteLine("\nAircraft models in Database:");
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
        }

        public string GetAircraftModelId()
        {
            List<string> availableAircraftModelIDs = aircraftModelRepository.GetAllAircraftModels().Select(x => Convert.ToString(x.ID)).ToList();

            string aircraftModelId = null;
            do
            {
                Console.WriteLine("\nChoose aircraft model id e.g. 1:");
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

            return aircraftModelId;
        }

        public void ExecuteAddAircraftModelProcedure()
        {
            Int32 aircraftModelId = -1;     //value of this variable does not have any meaning, it will not be used in adding aircraft model further on, but is necessary for creation of aircraft model object
            string aircraftModelDescription = GetAircraftModelDescription();
            string aircraftModelNumber = GetAircraftModelNumber();

            aircraftModelRepository.AddAircraftModel(new AircraftModel(aircraftModelId, aircraftModelDescription, aircraftModelNumber));
            Console.WriteLine("\nAircraft model was added sucessfully!\n");    
        }

        public void ExecuteDeleteAircraftModelProcedure()
        {
            PrintAllAircraftModels();
            Console.WriteLine("Which aircraft model would you like to delete?");
            string aircraftModelId = GetAircraftModelId(); 

            aircraftModelRepository.DeleteAircraftModel(aircraftModelId);
            Console.WriteLine("\nAircraft model is deleted!\n");
        }
    }
}
