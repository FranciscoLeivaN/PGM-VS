using System;
using System.Collections.Generic;

namespace PGM_ORM.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public string Clave { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public int UsuariosDepartamento { get; set; }

    public virtual Departamento? IdNavigation { get; set; }
}
