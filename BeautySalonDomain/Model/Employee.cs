using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonDomain.Model;

public partial class Employee : Entity
{
    [Display(Name = "Ім'я")]
    public string Name { get; set; } = null!;

    [Display(Name = "Позиція")]
    public int PositionsId { get; set; }

    public virtual ICollection<EmployeeService> EmployeeServices { get; set; } = new List<EmployeeService>();

    public virtual Position Positions { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
