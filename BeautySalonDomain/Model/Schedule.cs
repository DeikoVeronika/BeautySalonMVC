using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonDomain.Model;

public partial class Schedule : Entity
{

    [Display(Name = "Працівник")]
    public int EmployeesId { get; set; }

    [Required(ErrorMessage = "Оберіть дату")]
    [Display(Name = "Дата")]
    public DateOnly Date { get; set; }


    [Required(ErrorMessage = "Оберіть час")]
    [Display(Name = "Початок")]
    public TimeOnly StartTime { get; set; }

    [Display(Name = "Кінець")]
    public TimeOnly EndTime { get; set; }

    public virtual Employee Employees { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
