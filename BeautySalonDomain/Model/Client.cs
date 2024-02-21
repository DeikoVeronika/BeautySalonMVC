using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonDomain.Model;

public partial class Client: Entity
{
    //    public int Id { get; set; }

    [Display(Name = "Ім'я")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Прізвище")]
    public string LastName { get; set; } = null!;

    [Display(Name = "Номер телефону")]
    public string Phone { get; set; } = null!;

    [Display(Name = "Дата народження")]
    public DateOnly? Birthday { get; set; }

    [Display(Name = "Електронна пошта")]
    public string Email { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
