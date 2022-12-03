using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftSystem
{
    public class AircraftModelService
    {
        AircraftModelRepository aircraftModelRepository;

        public AircraftModelService(AircraftModelRepository aircraftModelRepository)
        {
            this.aircraftModelRepository = aircraftModelRepository;
        }

        public void ExecuteAddAircraftModelProcedure()
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


            string aircraftModelNumber = null;
            do
            {
                Console.WriteLine("\nEnter aircraft model number:");
                string aircraftModelNumberEntry = Console.ReadLine();

                if (String.IsNullOrEmpty(aircraftModelNumberEntry))
                {
                    Console.WriteLine("\nIncorrect input! Aircraft model number should not be empty!\n");
                }
                else if (aircraftModelRepository.GetAircraftModelNumbers().Contains(aircraftModelNumberEntry))
                {
                    Console.WriteLine("\nThis aircraft model number already exists, cannot add!\n");
                }
                else
                {
                    aircraftModelNumber = aircraftModelNumberEntry;
                }
            } while (aircraftModelNumber == null);

            aircraftModelRepository.AddAircraftModel(aircraftModelDescription, aircraftModelNumber);
            Console.WriteLine("\nAircraft model was added sucessfully!\n");    
        }

        public void ExecuteDeleteAircraftModelProcedure()
        {
            Console.WriteLine("\nAircraft models in Database:");
            foreach (string aircraftModel in aircraftModelRepository.GetAllAircraftModelsData())
            {
                Console.WriteLine(aircraftModel);
            }

            string aircraftModelId = null;
            do
            {
                Console.WriteLine("\nWhich aircraft model would you like to delete? Enter company id e.g. 1:");
                string aircraftModelIdEntry = Console.ReadLine();

                if (String.IsNullOrEmpty(aircraftModelIdEntry))
                {
                    Console.WriteLine("\nEmpty inputs are not acceptable!\n");
                }
                else if (!aircraftModelRepository.GetAircraftModelIds().Contains(aircraftModelIdEntry))
                {
                    Console.WriteLine("\nThis aircraft model does not exist!\n");
                }
                else
                {
                    aircraftModelId = aircraftModelIdEntry;
                }
            } while (aircraftModelId == null);

            aircraftModelRepository.DeleteAircraftModel(aircraftModelId);
            Console.WriteLine("\nAircraft model is deleted!\n");
        }
    }
}
