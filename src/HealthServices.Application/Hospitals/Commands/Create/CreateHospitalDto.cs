using FluentValidation;
using HealthServices.Domain.Extensions;

namespace HealthServices.Application.Hospitals.Commands.Create
{
    public class CreateHospitalDto
    {
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public string AddressZipCode { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class CreateHospitalDtoValidator : AbstractValidator<CreateHospitalDto>
    {
        public CreateHospitalDtoValidator()
        {
            RuleFor(o => o.Name)
                .MaximumLength(255)
                .NotEmpty();

            RuleFor(o => o.AddressLine1)
                .MaximumLength(255)
                .NotEmpty();

            RuleFor(o => o.AddressLine2)
                .MaximumLength(255);

            RuleFor(o => o.AddressCity)
                .MaximumLength(255)
                .NotEmpty();

            RuleFor(o => o.AddressState)
                .MaximumLength(255)
                .NotEmpty();

            RuleFor(o => o.AddressZipCode)
                .MinimumLength(5)
                .MaximumLength(10)
                .NotEmpty()
                .Must(BeValidZipCodeFormat).WithMessage("The zip code is not formatted correctly.  ##### or #####-####");

            RuleFor(o => o.PhoneNumber)
                .Length(10)
                .NotEmpty()
                .Must(BeValidPhoneNumberFormat).WithMessage("The phone number is not formatted correctly.  Numbers only please.");
        }

        private bool BeValidZipCodeFormat(string zipCode)
        {
            return zipCode.IsValidZipCodeFormat();
        }

        private bool BeValidPhoneNumberFormat(string phoneNumber)
        {
            return phoneNumber.IsValidPhoneNumberFormat();
        }
    }
}
