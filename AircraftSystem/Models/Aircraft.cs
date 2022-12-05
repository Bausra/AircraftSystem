using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftSystem.Models
{
    public class Aircraft
    {

        private Int32 id;
        public Int32 ID
        {
            get { return id; }
            set { id = value; }
        }
        private string tailNumber;
        public string TailNumber
        {
            get { return tailNumber; }
            set { tailNumber = value; }
        }
        private AircraftModel aircraftModel;
        public AircraftModel AircraftModel
        {
            get { return aircraftModel; }
            set { aircraftModel = value; }
        }
        private Company company;
        public Company Company
        {
            get { return company; }
            set { company = value; }
        }
        private Country country;
        public Country Country
        {
            get { return country; }
            set { country = value; }
        }


        public Aircraft(Int32 id, string tailNumber, AircraftModel aircraftModel, Company company, Country country)
        {
            this.id = id;
            this.tailNumber = tailNumber;
            this.aircraftModel = aircraftModel;
            this.company = company;
            this.country = country;
        }
    }
}
