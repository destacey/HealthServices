using HealthServices.Application.Hospitals.Dtos;
using HealthServices.Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HealthServices.Application.Hospitals.Queries.GetEditDetails
{
    public class GetHospitalEditDetailsQuery : IRequest<EditHospitalDto>
    {
        public GetHospitalEditDetailsQuery(int hospitalId)
        {
            HospitalId = hospitalId;
        }

        public int HospitalId { get; }
    }

    public sealed class GetHospitalEditDetailsQueryHandler : IRequestHandler<GetHospitalEditDetailsQuery, EditHospitalDto>
    {
        private readonly HealthServicesDbContext _context;

        public GetHospitalEditDetailsQueryHandler(HealthServicesDbContext context)
        {
            _context = context;
        }

        public async Task<EditHospitalDto> Handle(GetHospitalEditDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Hospitals
                .Select(h => new EditHospitalDto
                {
                    Id = h.Id,
                    Name = h.Name,
                    AddressLine1 = h.Address.Line1,
                    AddressLine2 = h.Address.Line2,
                    AddressCity = h.Address.City,
                    AddressState = h.Address.State,
                    AddressZipCode = h.Address.ZipCode.Value,
                    PhoneNumber = h.PhoneNumber.Value,
                })
                .FirstOrDefaultAsync(h => h.Id == request.HospitalId, cancellationToken);
        }
    }
}
