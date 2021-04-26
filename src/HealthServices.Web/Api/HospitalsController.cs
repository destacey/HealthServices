using HealthServices.Application.Hospitals.Dtos;
using HealthServices.Application.Hospitals.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthServices.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        private readonly ISender _sender;

        public HospitalsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IReadOnlyList<HospitalListDto>>> Get()
        {
            var hospitals = await _sender.Send(new GetAllHospitalsQuery());
            return Ok(hospitals.OrderBy(h => h.Name));
        }
    }
}
