using HealthServices.Domain;

namespace HealthServices.Application.Persistence.Seed
{
    public class HospitalImport
    {
        public string Name { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }

        public Hospital ToHospital()
        {
            return new Hospital(Name, Line1, Line2, City, State, ZipCode, PhoneNumber);
        }
    }
}
