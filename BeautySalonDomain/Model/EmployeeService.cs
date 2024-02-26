using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonDomain.Model;

public partial class EmployeeService : Entity
{

    [Display(Name = "Послуга")]
    public int ServicesId { get; set; }

    [Display(Name = "Працівник")]
    public int EmployeesId { get; set; }


    [Display(Name = "Послуга")]

    public virtual Service Services { get; set; } = null!;

    [Display(Name = "Працівник")]

    public virtual Employee Employees { get; set; } = null!;



    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

}
