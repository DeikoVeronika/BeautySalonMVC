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

    [Display(Name = "Бронювання створено")]
    public string Info { get; set; } = null!;

    [Display(Name = "Категорія  ")]
    public int TypeServicesId { get; set; }


    public int EmployeeServicesId { get; set; }


    [Display(Name = "Клієнт")]
    public virtual Client Clients { get; set; } = null!;

    [Display(Name = "Графік")]
    public virtual Schedule Schedules { get; set; } = null!;

    [Display(Name = "Послуга")]
    public virtual Service Services { get; set; } = null!;

    [Display(Name = "Категорія")]
    public virtual TypeService TypeServices { get; set; } = null!; 
    
    [Display(Name = "Працівник")]
    public virtual EmployeeService EmployeeServices { get; set; } = null!;

}
