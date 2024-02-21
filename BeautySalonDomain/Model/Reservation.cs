using System;
using System.Collections.Generic;

namespace BeautySalonDomain.Model;

public partial class Reservation : Entity
{
    public int ClientsId { get; set; }

    public int ServicesId { get; set; }

    public int SchedulesId { get; set; }

    public string Info { get; set; } = null!;

    public virtual Client Clients { get; set; } = null!;

    public virtual Schedule Schedules { get; set; } = null!;

    public virtual Service Services { get; set; } = null!;
}
