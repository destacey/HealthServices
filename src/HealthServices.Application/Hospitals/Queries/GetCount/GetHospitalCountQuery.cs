using HealthServices.Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HealthServices.Application.Hospitals.Queries.GetCount
{
    public class GetHospitalCountQuery : IRequest<int>
    {
    }

    public sealed class GetHospitalCountQueryHandler : IRequestHandler<GetHospitalCountQuery, int>
    {
        private readonly HealthServicesDbContext _context;

        public GetHospitalCountQueryHandler(HealthServicesDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(GetHospitalCountQuery request, CancellationToken cancellationToken)
        {
            return await _context.Hospitals.CountAsync(cancellationToken);
        }
    }
}
