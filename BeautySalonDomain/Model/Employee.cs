using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonDomain.Model;

public partial class Employee : Entity
{
    private string _name;


    [Required(ErrorMessage = "Введіть Ім'я працівника")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Ім'я не може містити у собі менше двох символів")]
    [Display(Name = "Ім'я")]
    public string Name
    {
        get => _name;
        set => _name = value.Substring(0, 1).ToUpperInvariant() + value.Substring(1);
    }
    [Display(Name = "Позиція")]
    public int PositionsId { get; set; }

    public virtual ICollection<EmployeeService> EmployeeServices { get; set; } = new List<EmployeeService>();

    public virtual Position Positions { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
