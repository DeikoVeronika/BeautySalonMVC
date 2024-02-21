using System;
using System.Collections.Generic;

namespace BeautySalonDomain.Model;

public partial class EmployeeService : Entity
{
    public int ServicesId { get; set; }

    public int EmployeesId { get; set; }

    public virtual Employee Employees { get; set; } = null!;
}
