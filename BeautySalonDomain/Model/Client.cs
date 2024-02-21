using System;
using System.Collections.Generic;

namespace BeautySalonDomain.Model;

public partial class Client: Entity
{
//    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateOnly? Birthday { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
