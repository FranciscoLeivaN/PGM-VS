using System;
using System.Collections.Generic;

namespace PGM_ORM.Models;

public partial class Departamento
{
    public int IdDepartamento { get; set; }

    public string NombreDepartamento { get; set; } = null!;

    public string JefeDepartamento { get; set; } = null!;

    public virtual Usuario? Usuario { get; set; }
}
