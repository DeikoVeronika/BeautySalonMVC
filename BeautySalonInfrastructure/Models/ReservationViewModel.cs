using BeautySalonDomain.Model;

namespace BeautySalonInfrastructure.Models
{
    public class ReservationViewModel
    {
        public Client Client { get; set; }
        public int ServicesId { get; set; }
        public int SchedulesId { get; set; }
        public int TypeServicesId { get; set; }
        public int EmployeeServicesId { get; set; }
    }


}
