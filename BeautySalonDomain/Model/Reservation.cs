using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonDomain.Model;

public partial class Reservation : Entity 
{

    [Display(Name = "Клієнт")]
    public int ClientsId { get; set; }

    [Display(Name = "Послуга")]
    public int ServicesId { get; set; }

    [Display(Name = "Графік")]
    public int SchedulesId { get; set; }

    [Display(Name = "Інформація")]
    public string Info { get; set; } = null!;

    public virtual Client Clients { get; set; } = null!;

    public virtual Schedule Schedules { get; set; } = null!;

    public virtual Service Services { get; set; } = null!;
}
