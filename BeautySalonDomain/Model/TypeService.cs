using System;
using System.Collections.Generic;

namespace BeautySalonDomain.Model;

public partial class TypeService : Entity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
