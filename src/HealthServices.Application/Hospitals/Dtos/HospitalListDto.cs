using System.Text.RegularExpressions;

namespace HealthServices.Application.Hospitals.Dtos
{
    public class HospitalListDto
    {
        private string _phoneNumber;

        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PhoneNumber 
        { 
            get => _phoneNumber; 
            set => _phoneNumber = string.IsNullOrWhiteSpace(value)
                ? ""
                : Regex.Replace(value, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
        }
    }
}
