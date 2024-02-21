using System;
using System.Collections.Generic;

namespace BeautySalonDomain.Model;

public partial class Service : Entity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int TypeServiceId { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual TypeService TypeService { get; set; } = null!;
}
