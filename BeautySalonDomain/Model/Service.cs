using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonDomain.Model;

public partial class Service : Entity
{
    [Display(Name = "Послуга")]
    public string Name { get; set; } = null!;

    [Display(Name = "Опис")]
    public string Description { get; set; } = null!;

    [Display(Name = "Ціна")]
    public decimal Price { get; set; }

    [Display(Name = "Категорія")]
    public int TypeServiceId { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    [Display(Name = "Категорія")]
    public virtual TypeService TypeService { get; set; } = null!;
}
