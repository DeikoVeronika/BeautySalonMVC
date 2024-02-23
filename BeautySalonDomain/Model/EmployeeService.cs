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

    public virtual Service Services { get; set; } = null!;

    public virtual Employee Employees { get; set; } = null!;
}
