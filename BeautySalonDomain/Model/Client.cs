using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonDomain.Model;

public partial class Client: Entity
{
    //    public int Id { get; set; }

    [Required(ErrorMessage = "Введіть ім'я ")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Ім'я не може містити у собі менше двох символів")]
    [Display(Name = "Ім'я")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Введіть прізвище ")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Прізвище не може містити у собі менше двох символів")]
    [Display(Name = "Прізвище")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Введіть мобільний номер ")]
    [Display(Name = "Мобільний номер телефону")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Введіть дату народження")]
    [Display(Name = "Дата народження")]
    [DataType(DataType.Date)]
    public DateOnly? Birthday { get; set; }


    [Required(ErrorMessage = "Введіть електронну пошту")]
    [Display(Name = "Електронна пошта")]
    [EmailAddress(ErrorMessage = "Невірний формат електронної пошти")]
    public string Email { get; set; } = null!;


    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
