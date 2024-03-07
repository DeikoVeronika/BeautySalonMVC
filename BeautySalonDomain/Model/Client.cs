using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonDomain.Model;

public partial class Client: Entity
{

    private string _firstName;
    private string _lastName;


    [Required(ErrorMessage = "Введіть ім'я ")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Ім'я не може містити у собі менше двох символів")]
    [Display(Name = "Ім'я")]
    public string FirstName
    {
        get => _firstName;
        set => _firstName = value.Substring(0, 1).ToUpperInvariant() + value.Substring(1);
    }

    [Required(ErrorMessage = "Введіть прізвище ")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Прізвище не може містити у собі менше двох символів")]
    [Display(Name = "Прізвище")]
    public string LastName
    {
        get => _lastName;
        set => _lastName = value.Substring(0, 1).ToUpperInvariant() + value.Substring(1);
    }

    [Required(ErrorMessage = "Введіть мобільний  ")]
    [Display(Name = "Мобільний номер")]
    public string Phone { get; set; } = null!;


    [Display(Name = "Дата народження")]
    [DataType(DataType.Date)]
    [DateOfBirth]
    public DateOnly? Birthday { get; set; }


    [Required(ErrorMessage = "Введіть електронну пошту")]
    [Display(Name = "Електронна пошта")]
    [EmailAddress(ErrorMessage = "Невірний формат електронної пошти")]
    public string Email { get; set; } = null!;


    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
