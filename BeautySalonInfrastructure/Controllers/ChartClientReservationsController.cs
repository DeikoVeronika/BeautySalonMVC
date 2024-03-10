using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartClientReservationsController : ControllerBase
    {
        private readonly DbbeautySalonContext _context;

        public ChartClientReservationsController(DbbeautySalonContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var clients = _context.Clients.Include(c => c.Reservations).ToList();
            List<object> cReservation = new List<object>();
            cReservation.Add(new[] { "Клієнт", "Кількість бронювань" });
            foreach (var c in clients)
            {
                cReservation.Add(new object[] { c.FirstName + " " + c.LastName, c.Reservations.Count() });
            }

            return new JsonResult(cReservation);
        }
    }
}
