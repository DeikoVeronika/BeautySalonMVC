using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeautySalonInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartTypeServiceController : ControllerBase
    {
        private readonly DbbeautySalonContext _context;

        public ChartTypeServiceController(DbbeautySalonContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]   
        public JsonResult JsonData()
        {
            var typeServices = _context.TypeServices.Include(ts => ts.Services).ToList();
            List<object> catService = new List<object>();
            catService.Add(new[] { "Категорія", "Кількість послуг" });
            foreach (var c in typeServices)
            {
                catService.Add(new object[] { c.Name, c.Services.Count() });
            }

            return new JsonResult(catService);
        }


    }


}
