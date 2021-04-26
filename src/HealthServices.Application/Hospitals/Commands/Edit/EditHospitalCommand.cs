using FluentValidation;
using HealthServices.Application.Hospitals.Dtos;
using HealthServices.Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HealthServices.Application.Hospitals.Commands.Edit
{
    public class EditHospitalCommand : IRequest<bool>
    {
        public EditHospitalCommand(EditHospitalDto dto)
        {
            Dto = dto;
        }

        public EditHospitalDto Dto { get; }
    }

    public class EditHospitalCommandValidator : AbstractValidator<EditHospitalCommand>
    {
        public EditHospitalCommandValidator(IValidator<EditHospitalDto> editHospitalDtoValidator)
        {
            RuleFor(o => o.Dto)
                .NotNull()
                .SetValidator(editHospitalDtoValidator);
        }
    }

    public class EditHospitalCommandHandler : IRequestHandler<EditHospitalCommand, bool>
    {
        private readonly HealthServicesDbContext _context;

        public EditHospitalCommandHandler(HealthServicesDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(EditHospitalCommand request, CancellationToken cancellationToken)
        {
            var hospital = await _context.Hospitals
                .FirstOrDefaultAsync(h => h.Id == request.Dto.Id, cancellationToken);

            if (hospital == null) return false;

            hospital.Update(
                request.Dto.Name,
                request.Dto.AddressLine1,
                request.Dto.AddressLine2,
                request.Dto.AddressCity,
                request.Dto.AddressState,
                request.Dto.AddressZipCode,
                request.Dto.PhoneNumber
                );

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
