using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonDomain.Model;

public partial class Client: Entity
{

    private string _firstName;
    private string _lastName;


    [Required(ErrorMessage = "Введіть ім'я ")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Ім'я повинно містити у собі принаймні дві літери")]
    [RegularExpression("^[А-Яа-яІіЇїЄє]+$", ErrorMessage = "Ім'я може містити лише українські літери")]
    [Display(Name = "Ім'я")]
    public string FirstName
    {
        get => _firstName;
        set => _firstName = value.Substring(0, 1).ToUpperInvariant() + value.Substring(1);
    }

    [Required(ErrorMessage = "Введіть прізвище ")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Прізвище повинно містити у собі принаймні дві літери")]
    [RegularExpression("^[А-Яа-яІіЇїЄє]+$", ErrorMessage = "Прізвище може містити лише українські літери")]
    [Display(Name = "Прізвище")]

    public string LastName
    {
        get => _lastName;
        set => _lastName = value.Substring(0, 1).ToUpperInvariant() + value.Substring(1);
    }

    [Required(ErrorMessage = "Введіть номер телефону ")]
    [RegularExpression(@"^\+380 \(\d{2}\) \d{3}-\d{2}-\d{2}$", ErrorMessage = "Введіть коректний номер телефону")]
    [Display(Name = "Мобільний номер")]
    public string Phone { get; set; } = null!;




    [Display(Name = "Дата народження (необов'язково)")]
    [DataType(DataType.Date)]
    [DateOfBirth]
    public DateOnly? Birthday { get; set; }


    [Required(ErrorMessage = "Введіть електронну пошту")]
    [Display(Name = "Електронна пошта")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Невірний формат електронної пошти")]
    public string Email { get; set; } = null!;



    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
