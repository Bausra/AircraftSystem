using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftSystem.Models
{
    public class AircraftModel
    {
        private Int32 id;
        public Int32 ID
        {
            get { return id; }
            set { id = value; }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
        }


        public AircraftModel(Int32 id, string description, string number)
        {
            this.ID = id;
            this.Description = description;
            this.number = number;
        }
    }
}
