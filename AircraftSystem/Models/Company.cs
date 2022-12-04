using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftSystem.Models
{
    public class Company
    {
        private Int32 id;
        public Int32 ID
        {
            get { return id; }
            set { id = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        public Company(Int32 id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}
