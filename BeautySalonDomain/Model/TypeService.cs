using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonDomain.Model;

public partial class TypeService : Entity
{
    public TypeService()
    {
        Services = new HashSet<Service>();
    }

    [Required(ErrorMessage = "Введіть категорію")]
    [Display(Name = "Категорія")]
    public string Name { get; set; }

    public virtual ICollection<Service> Services { get; set; }
}
