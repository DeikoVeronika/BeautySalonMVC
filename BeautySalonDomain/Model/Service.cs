using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonDomain.Model;

public partial class Service : Entity
{
    private string _name;

    [Required(ErrorMessage = "Введіть назву послуги")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Назва послуги не може містити у собі менше двох символів")]
    [RegularExpression(@"^.*[а-яА-Яa-zA-Z].*[а-яА-Яa-zA-Z].*$", ErrorMessage = "Назва послуги повинна містити принаймні дві букви")]
    [Display(Name = "Назва послуги")]
    public string Name
    {
        get => _name;
        set => _name = value.Substring(0, 1).ToUpperInvariant() + value.Substring(1);
    }


    [Display(Name = "Опис")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Укажіть ціну послуги")]
    [Display(Name = "Ціна")]
    public int Price { get; set; }

    [Required(ErrorMessage = "Оберіть категорію")]
    [Display(Name = "Категорія")]
    public int TypeServiceId { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    [Display(Name = "Категорія")]
    public virtual TypeService TypeService { get; set; } = null!;
}
