using FluentValidation;
using HealthServices.Application.Persistence;
using HealthServices.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HealthServices.Application.Hospitals.Commands.Create
{
    public class CreateHospitalCommand : IRequest<int>
    {
        public CreateHospitalCommand(CreateHospitalDto dto)
        {
            Dto = dto;
        }

        public CreateHospitalDto Dto { get; }
    }

    public class CreateHospitalCommandValidator : AbstractValidator<CreateHospitalCommand>
    {
        public CreateHospitalCommandValidator(IValidator<CreateHospitalDto> createHospitalDtoValidator)
        {
            RuleFor(o => o.Dto)
                .NotNull()
                .SetValidator(createHospitalDtoValidator);
        }
    }

    public class CreateHospitalCommandHandler : IRequestHandler<CreateHospitalCommand, int>
    {
        private readonly HealthServicesDbContext _context;

        public CreateHospitalCommandHandler(HealthServicesDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateHospitalCommand request, CancellationToken cancellationToken)
        {
            var hospital = new Hospital(
                request.Dto.Name,
                request.Dto.AddressLine1,
                request.Dto.AddressLine2,
                request.Dto.AddressCity,
                request.Dto.AddressState,
                request.Dto.AddressZipCode,
                request.Dto.PhoneNumber
                );

            _context.Hospitals.Add(hospital);
            await _context.SaveChangesAsync(cancellationToken);

            return hospital.Id;
        }
    }
}
