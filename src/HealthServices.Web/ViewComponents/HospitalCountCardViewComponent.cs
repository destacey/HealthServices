using HealthServices.Application.Hospitals.Queries.GetCount;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HealthServices.Web.ViewComponents
{
    public class HospitalCountCardViewComponent : ViewComponent
    {
        private readonly ISender _sender;

        public HospitalCountCardViewComponent(ISender sender)
        {
            _sender = sender;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _sender.Send(new GetHospitalCountQuery()));
        }
    }
}
