using System;
using System.Collections.Generic;

namespace BeautySalonDomain.Model;

public partial class Schedule : Entity
{
    public int EmployeesId { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public virtual Employee Employees { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
