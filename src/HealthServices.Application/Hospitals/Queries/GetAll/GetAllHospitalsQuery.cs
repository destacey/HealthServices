using HealthServices.Application.Hospitals.Dtos;
using HealthServices.Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HealthServices.Application.Hospitals.Queries.GetAll
{
    public class GetAllHospitalsQuery : IRequest<IReadOnlyList<HospitalListDto>>
    {
    }

    public sealed class GetAllHospitalsQueryHandler : IRequestHandler<GetAllHospitalsQuery, IReadOnlyList<HospitalListDto>>
    {
        private readonly HealthServicesDbContext _context;

        public GetAllHospitalsQueryHandler(HealthServicesDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<HospitalListDto>> Handle(GetAllHospitalsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Hospitals
                .Select(h => new HospitalListDto
                {
                    Id = h.Id,
                    Name = h.Name,
                    City = h.Address.City,
                    State = h.Address.State,
                    PhoneNumber = h.PhoneNumber.Value
                })
                .ToListAsync(cancellationToken);
        }
    }
}
