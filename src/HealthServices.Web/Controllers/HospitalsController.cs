using HealthServices.Application.Hospitals.Commands.Create;
using HealthServices.Application.Hospitals.Commands.Delete;
using HealthServices.Application.Hospitals.Commands.Edit;
using HealthServices.Application.Hospitals.Dtos;
using HealthServices.Application.Hospitals.Queries.GetById;
using HealthServices.Application.Hospitals.Queries.GetEditDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HealthServices.Web.Controllers
{
    public class HospitalsController : Controller
    {
        private readonly ISender _sender;
        private readonly ILogger<HospitalsController> _logger;

        public HospitalsController(ISender sender, ILogger<HospitalsController> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        // GET: HospitalsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HospitalsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var hospital = await _sender.Send(new GetHospitalByIdQuery(id));

            if (hospital == null) return NotFound();

            return View(hospital);
        }

        // GET: HospitalsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HospitalsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Name,AddressLine1,AddressLine2,AddressCity,AddressState,AddressZipCode,PhoneNumber")] CreateHospitalDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var newId = await _sender.Send(new CreateHospitalCommand(dto));

            return RedirectToAction(nameof(Details), new { id = newId });
        }

        // GET: HospitalsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var hospital = await _sender.Send(new GetHospitalEditDetailsQuery(id));

            if (hospital == null) return NotFound();

            return View(hospital);
        }

        // POST: HospitalsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Name,AddressLine1,AddressLine2,AddressCity,AddressState,AddressZipCode,PhoneNumber")] EditHospitalDto dto)
        {
            if (id != dto.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await _sender.Send(new EditHospitalCommand(dto));

            if (!result)
            {
                return View(dto);
            }

            return RedirectToAction(nameof(Details), new { id });
            
        }

        // GET: HospitalsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var hospital = await _sender.Send(new GetHospitalByIdQuery(id));

            if (hospital == null) return NotFound();

            return View(hospital);
        }

        // POST: HospitalsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {

                var result = await _sender.Send(new DeleteHospitalCommand(id));

                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Details), new { id });
            }
            catch
            {
                return View();
            }
        }
    }
}
