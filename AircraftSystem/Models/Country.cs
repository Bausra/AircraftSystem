
namespace AircraftSystem.Models
{
    public class Country
    {
        private string shorthand;
        public  string Shorthand
        {
            get { return shorthand; }
            set { shorthand = value; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private Boolean isEurope;
        public Boolean IsEurope
        {
            get { return isEurope; }
            set { isEurope = value; }
        }


        public Country(string shorthand, string name, bool isEurope)
        {
            this.Shorthand = shorthand;
            this.Name = name;
            this.IsEurope = isEurope;
        }
    }
}
