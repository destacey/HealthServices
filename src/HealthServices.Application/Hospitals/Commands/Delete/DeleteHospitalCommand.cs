using HealthServices.Application.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace HealthServices.Application.Hospitals.Commands.Delete
{
    public class DeleteHospitalCommand : IRequest<bool>
    {
        public DeleteHospitalCommand(int hospitalId)
        {
            HospitalId = hospitalId;
        }

        public int HospitalId { get; }
    }

    public class DeleteHospitalCommandHandler : IRequestHandler<DeleteHospitalCommand, bool>
    {
        private readonly HealthServicesDbContext _context;

        public DeleteHospitalCommandHandler(HealthServicesDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteHospitalCommand request, CancellationToken cancellationToken)
        {
            var hospital = await _context.Hospitals
                .FirstOrDefaultAsync(h => h.Id == request.HospitalId, cancellationToken);

            if (hospital == null) return false;

            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
